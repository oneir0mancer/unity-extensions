using System.Collections.Generic;
using UnityEngine;

namespace Oneiromancer.Utils
{
    [System.Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [System.Serializable]
        public struct Pair
        {
            public TKey Key;
            public TValue Value;

            public static implicit operator KeyValuePair<TKey, TValue>(Pair pair)
            {
                return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
            }

            public static implicit operator Pair(KeyValuePair<TKey, TValue> pair)
            {
                return new Pair
                {
                    Key = pair.Key, 
                    Value = pair.Value
                };
            }
        }
        
        [SerializeField] private List<Pair> _entries = new List<Pair>();
        
        public SerializedDictionary()
        { }

        public SerializedDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        { }
        
        public void OnBeforeSerialize()
        {
            _entries.Clear();
            foreach (var pair in this)
            {
                _entries.Add(pair);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();
            foreach (var entry in _entries)
            {
                this[entry.Key] = entry.Value;
            }
        }
    }
}