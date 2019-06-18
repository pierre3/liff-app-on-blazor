﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using LiffSdk.Blazor.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LiffSdk.Blazor
{
    public class LiffClient
    {
        protected IJSRuntime JSRuntime { get; set; }

        public LiffData Data { get; protected set; }

        public Profile Profile { get; set; }

        public string Error { get; protected set; }

        public LiffClient()
        { }

        public async Task InitializeAsync(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
            await JSRuntime.InvokeAsync<object>("liffExt.init", DotNetObjectRef.Create(this));
        }

        public async Task LoadProfileAsync()
        {
            Profile = await JSRuntime.InvokeAsync<Profile>("liff.getProfile");
        }

        public async Task SendMessagesAsync(string text)
        {
            await JSRuntime.InvokeAsync<object>("liff.sendMessages", new List<object>() { new { type = "text", text } });
        }

        public async Task OpenWindow(string url, bool external)
        {
            await JSRuntime.InvokeAsync<object>("liff.openWindow", new { url, external });
        }

        public async Task CloseWindow()
        {
            await JSRuntime.InvokeAsync<object>("liff.closeWindow");
        }

        [JSInvokable]
        public void LiffInitSuccess(string data)
        {
            try
            {
                Data = JsonConvert.DeserializeObject<LiffData>(data);
            }
            catch (Exception e)
            {
                Error = e.ToString();
            }
        }

        [JSInvokable]
        public void LiffInitError(string error)
        {
            Error = $"Error: {error}";
        }
    }
}
