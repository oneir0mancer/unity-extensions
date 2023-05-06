﻿﻿using UnityEditor;
using UnityEngine;

namespace Oneiromancer.NestedScriptableObjects
{
    [CustomEditor(typeof(NestedScriptableObject<>))]
    public class NestedScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            OnInspectorGUI((dynamic) target);
        }
        
        private void OnInspectorGUI<T>(NestedScriptableObject<T> myTarget) where T : ScriptableObject
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Nested Object:", EditorStyles.boldLabel);
            if (myTarget.NestedObject == null)
            {
                var original = EditorGUILayout.ObjectField("Add", myTarget.NestedObject, typeof(T), false) as T;
                if (original != null)
                {
                    AddNestedObject(myTarget, Instantiate(original));
                    //Object.DestroyImmediate(original, true);
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(original));
                    SaveAssets();
                }
                else if (GUILayout.Button("Create"))
                {
                    AddNestedObject(myTarget, CreateInstance<T>());
                    SaveAssets();
                }
            }
            else if (GUILayout.Button("Clear Nested"))
            {
                AssetDatabase.RemoveObjectFromAsset(myTarget.NestedObject);
                Object.DestroyImmediate(myTarget.NestedObject, true);
                SaveAssets();
                
                myTarget.NestedObject = null;
            }
        }

        private void AddNestedObject<T>(NestedScriptableObject<T> myTarget, T obj) where T : ScriptableObject
        {
            obj.name = "Nested";
            AssetDatabase.AddObjectToAsset(obj, myTarget);
            myTarget.NestedObject = obj;
        }

        private void SaveAssets()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}