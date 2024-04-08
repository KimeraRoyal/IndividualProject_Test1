using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IP1
{
    public abstract class MicrogameSelector : ScriptableObject
    {
        public abstract string Select(GameState _state);
    }
}
