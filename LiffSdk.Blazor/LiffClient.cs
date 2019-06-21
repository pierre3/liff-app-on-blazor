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
    public class InitSuccessEventArgs : EventArgs
    {
        public LiffData Data{get;set;}
        public InitSuccessEventArgs(LiffData data)
        {
            Data = data;
        }
    }

    public class LiffClientErrorEventArgs : EventArgs
    {
        public string Error { get; set; }
        public LiffClientErrorEventArgs(string error)
        {
            Error = error;
        }
    }

    public class GetProfileSuccessEventArgs : EventArgs
    {
        public Profile Profile { get; set; }
        public GetProfileSuccessEventArgs(Profile profile)
        {
            Profile = profile;
        }
    }
    
    public class LiffClient
    {
        protected bool Initialized;
        protected IJSRuntime JSRuntime { get; set; }

        public LiffData Data { get; protected set; }

        public Profile Profile { get; set; }

        public string Error { get; protected set; }

        public event EventHandler<InitSuccessEventArgs> InitSuccess;
        public event EventHandler<LiffClientErrorEventArgs> InitError;

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
            Profile = await JSRuntime.InvokeAsync<Profile>("liff.getProfile");
        }

        public async Task SendMessagesAsync(string messages)
        {
            await JSRuntime.InvokeAsync<object>("liff.sendMessages", messages);
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
            InitSuccess?.Invoke(this, new InitSuccessEventArgs(Data));
        }

        [JSInvokable]
        public void OnInitError(string error)
        {
            Error = error;
            InitError?.Invoke(this, new LiffClientErrorEventArgs(Error));
        }

    }
}
