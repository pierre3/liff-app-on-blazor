using System;

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