using System;

namespace DesktopClient.Platform
{
    [Serializable]
    public sealed class VerbNotRecognizedException : Exception
    {
        public VerbNotRecognizedException(string verb) : base($"Cannot determine appropriate matching HTTP Verb for string '{verb}'")
        {            
        }
    }
}