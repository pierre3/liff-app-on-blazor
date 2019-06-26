using System;
using System.Threading.Tasks;
using LiffSdk.Blazor.Data;
using Microsoft.JSInterop;

namespace LiffSdk.Blazor
{
    public interface ILiff
    {
        bool Initialized { get; }
        LiffData Data { get; }
        Profile Profile { get; }

        Task CloseWindowAsync();
        Task<string> GetAccessTokenAsync();
        Task InitializeAsync(IJSRuntime jSRuntime);
        Task LoadProfileAsync();
        Task OpenWindowAsync(string url, bool external);
        void Reset();
        Task SendMessagesAsync(object messages);
    }
}