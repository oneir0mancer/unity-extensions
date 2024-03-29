﻿using UnityEngine;

namespace Oneiromancer.NestedScriptableObjects
{
    /// A reference to SO, that can exist as a separate asset, of be "consumed" as a nested SO in parent asset.
    [System.Serializable]
    public class ScriptableObjectReference<T> where T : ScriptableObject
    {
        public T Value;
    }
}