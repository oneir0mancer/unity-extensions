using UnityEngine;

namespace Oneiromancer.NestedScriptableObjects
{
    /// This isn't really a functional implementation of a nested SO, and exists only to showcase custom editor for generic classes
    public class NestedScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        [HideInInspector] public T NestedObject;
    }
}