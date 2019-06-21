using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using LiffSdk.Blazor.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Line.Messaging;
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

        public LiffClient()
        { }

        public async Task InitializeAsync(IJSRuntime jSRuntime)
        {
            if (!Initialized)
            {
                JSRuntime = jSRuntime;
                await JSRuntime.InvokeAsync<object>("liffExt.init", DotNetObjectRef.Create(this));
                Initialized = Data != null;
            }
        }

        public async Task LoadProfileAsync()
        {
            Profile = await JSRuntime.InvokeAsync<Profile>("liff.getProfile");
        }

        public async Task SendMessagesAsync(IList<ISendMessage> messages)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() }
            };
            settings.Converters.Add(new StringEnumConverter() { NamingStrategy = new CamelCaseNamingStrategy() });
            var json = JsonConvert.SerializeObject(messages, settings);
            await JSRuntime.InvokeAsync<object>("liffExt.sendMessages", json);
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
            InitSuccess?.Invoke(this, data);
        }

        [JSInvokable]
        protected void OnInitError(string error)
        {
            Error = error;
            InitError?.Invoke(this, error);
        }
    }
}
