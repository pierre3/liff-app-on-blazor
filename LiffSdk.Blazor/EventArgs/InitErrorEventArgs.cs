using LiffSdk.Blazor.Data;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LiffSdk.Blazor
{
    public class InitErrorEventArgs : EventArgs
    {
        public string Error { get; set; }
        public InitErrorEventArgs(string error)
        {
            Error = error;
        }
    }

}