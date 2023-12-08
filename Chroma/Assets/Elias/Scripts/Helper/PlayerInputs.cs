using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public static class PlayerInputs
    {
        private static readonly KeyCode[] Keys = { KeyCode.R, KeyCode.G, KeyCode.B, KeyCode.C };
        public static readonly List<KeyCode> InputList = new List<KeyCode>(Keys);
    }
}