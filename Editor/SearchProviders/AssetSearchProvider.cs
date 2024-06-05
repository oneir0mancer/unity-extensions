using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Oneiromancer.SearchProviders
{
    public class AssetSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        private readonly Type _assetType;
        private readonly SerializedProperty _property;
        
        public AssetSearchProvider(Type assetType, SerializedProperty property)
        {
            _assetType = assetType;
            _property = property;
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> list = new List<SearchTreeEntry>();

            string[] guids = AssetDatabase.FindAssets($"t:{_assetType.Name}");
            List<string> paths = guids.Select(AssetDatabase.GUIDToAssetPath).OrderBy(x => x).ToList();
            List<string> groups = new List<string>();

            foreach (var item in paths)
            {
                string[] entries = item.Split('/');    //System.IO.Path.DirectorySeparatorChar
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

                var obj = AssetDatabase.LoadAssetAtPath<Object>(item);
                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entries.Last(),
                    EditorGUIUtility.ObjectContent(obj, obj.GetType()).image))
                {
                    level = entries.Length, 
                    userData = obj
                };
                list.Add(entry);
            }

            return list;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            _property.objectReferenceValue = (Object) searchTreeEntry.userData;
            _property.serializedObject.ApplyModifiedProperties();
            return true;
        }
    }
}