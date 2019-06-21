﻿using LiffSdk.Blazor.Data;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LiffSdk.Blazor
{
    public class Liff
    {
        protected bool Initialized;
        protected IJSRuntime JSRuntime { get; set; }

        public LiffData Data { get; protected set; }

        public Profile Profile { get; set; }

        public string Error { get; protected set; }

        public event EventHandler<InitSuccessEventArgs> InitSuccess;
        public event EventHandler<InitErrorEventArgs> InitError;

        public Liff()
        { }

        public void Reset()
        {
            Data = null;
            Profile = null;
            Error = null;
            Initialized = false;
        }

        public async Task InitializeAsync(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
            if (Initialized)
            {
                return;
            }
            await JSRuntime.InvokeAsync<object>("liffInterop.init", DotNetObjectRef.Create(this));
        }

        public async Task LoadProfileAsync() => Profile = await JSRuntime.InvokeAsync<Profile>("liff.getProfile");

        public async Task SendMessagesAsync(object messages) 
            => await JSRuntime.InvokeAsync<object>("liff.sendMessages", messages);

        public async Task OpenWindowAsync(string url, bool external) 
            => await JSRuntime.InvokeAsync<object>("liff.openWindow", new { url, external });

        public async Task CloseWindowAsync() => await JSRuntime.InvokeAsync<object>("liff.closeWindow");

        public Task<string> GetAccessTokenAsync() => JSRuntime.InvokeAsync<string>("liff.getAccessToken");

        [JSInvokable]
        public void OnInitSuccess(string data)
        {
            try
            {
                Data = JsonConvert.DeserializeObject<LiffData>(data);
                Initialized = true;
                InitSuccess?.Invoke(this, new InitSuccessEventArgs(Data));
            }
            catch (Exception e)
            {
                Error = e.ToString();
            }
        }

        [JSInvokable]
        public void OnInitError(string error)
        {
            Error = error;
            InitError?.Invoke(this, new InitErrorEventArgs(Error));
        }

    }
}