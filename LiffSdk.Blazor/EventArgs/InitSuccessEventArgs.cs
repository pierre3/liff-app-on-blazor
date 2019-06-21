using LiffSdk.Blazor.Data;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LiffSdk.Blazor
{
    public class InitSuccessEventArgs : EventArgs
    {
        public LiffData Data { get; set; }
        public InitSuccessEventArgs(LiffData data)
        {
            Data = data;
        }
    }
}