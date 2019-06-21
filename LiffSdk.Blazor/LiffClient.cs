using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using LiffSdk.Blazor.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace LiffSdk.Blazor
{
    public class LiffClient
    {
        protected bool Initialized;
        protected IJSRuntime JSRuntime { get; set; }

        public LiffData Data { get; protected set; }

        public Profile Profile { get; set; }

        public string Error { get; protected set; }

        public event EventHandler<string> InitSuccess;
        public event EventHandler<string> InitError;
        public event EventHandler<string> GetProfileSuccess;
        public event EventHandler<string> GetProfileError;

        public LiffClient()
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
            if (Initialized) { return; }
            await JSRuntime.InvokeAsync<object>("liffInterop.init", DotNetObjectRef.Create(this));
        }

        public async Task LoadProfileAsync()
        {
            Profile = await JSRuntime.InvokeAsync<Profile>("liffInterop.getProfile", DotNetObjectRef.Create(this));
        }

        public async Task SendMessagesAsync(string messages)
        {
            await JSRuntime.InvokeAsync<object>("liffExt.sendMessages", messages);
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
        protected void OnInitSuccess(string data)
        {
            try
            {
                Data = JsonConvert.DeserializeObject<LiffData>(data);
            }
            catch (Exception e)
            {
                Error = e.ToString();
            }
            Initialized = true;
            InitSuccess?.Invoke(this, data);
        }

        [JSInvokable]
        protected void OnInitError(string error)
        {
            Error = error;
            InitError?.Invoke(this, error);
        }

        [JSInvokable]
        protected void OnGetProfileSuccess(string data)
        {
            try
            {
                Profile = JsonConvert.DeserializeObject<Profile>(data);
            }
            catch (Exception e)
            {
                Error = e.ToString();
            }
            GetProfileSuccess?.Invoke(this, data);
        }

        [JSInvokable]
        protected void OnGetProfileError(string error)
        {
            Error = error;
            GetProfileError?.Invoke(this, error);
        }
    }
}
