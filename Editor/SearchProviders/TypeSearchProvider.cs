using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Oneiromancer.SearchProviders
{
    public class TypeSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        private readonly Type _type;
        private readonly Action<Type> _callback;
        private readonly bool _simplifyNamespace;

        public TypeSearchProvider(Type type, Action<Type> callback = null)
        {
            _type = type;
            _callback = callback;
            _simplifyNamespace = true;
        }

        public TypeSearchProvider(Type type, bool simplifyNamespace, Action<Type> callback = null)
        {
            _type = type;
            _callback = callback;
            _simplifyNamespace = simplifyNamespace;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> list = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Types"), 0)
            };
            List<string> groups = new List<string>();
            
            var types = TypeCache.GetTypesDerivedFrom<ScriptableObject>();
            foreach (var type in types)
            {
                if (type.IsAbstract || !_type.IsAssignableFrom(type)) continue;

                //TODO can check for attribute and structure tree according to it.
                
                int indentLevel = 1;
                if (!_simplifyNamespace && !string.IsNullOrEmpty(type.FullName))
                {
                    string[] entries = type.FullName.Split('.');
                    indentLevel = entries.Length;
                    string groupName = "";
                    for (int i = 0; i < entries.Length - 1; i++)
                    {
                        groupName += entries[i];
                        if (!groups.Contains(groupName))
                        {
                            list.Add(new SearchTreeGroupEntry(new GUIContent(entries[i]), i + 1));
                            groups.Add(groupName);
                        }

                        groupName += "/";
                    }
                }

                var entry = new SearchTreeEntry(new GUIContent(type.Name))
                {
                    level = indentLevel,
                    userData = type,
                };
                list.Add(entry);
            }

            return list;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            Type type = (Type) searchTreeEntry.userData;
            _callback?.Invoke(type);
            return true;
        }
    }
}