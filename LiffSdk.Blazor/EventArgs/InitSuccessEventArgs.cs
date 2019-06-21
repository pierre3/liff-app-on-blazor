using LiffSdk.Blazor.Data;
using System;

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