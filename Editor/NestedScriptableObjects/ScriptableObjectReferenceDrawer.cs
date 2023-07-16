using System.IO;
using System.Reflection;
using Oneiromancer.Utils;
using UnityEditor;
using UnityEngine;

namespace Oneiromancer.NestedScriptableObjects
{
    [CustomPropertyDrawer(typeof(ScriptableObjectReference<>), true)]
    public class ScriptableObjectReferenceDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int rows = 2;
            return 20f * rows + 5;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueProperty = property.FindPropertyRelative("Value");
            var rect = new Rect(position.x, position.y, position.width, 20);
            EditorGUI.PropertyField(rect, valueProperty, new GUIContent(property.displayName));
            rect.y += rect.height;

            if (valueProperty.objectReferenceValue != null)
            {
                if (AssetDatabase.IsSubAsset(valueProperty.objectReferenceValue))
                {
                    if (GUI.Button(rect, "Clear Nested"))
                    {
                        AssetDatabase.RemoveObjectFromAsset(valueProperty.objectReferenceValue);
                        Object.DestroyImmediate(valueProperty.objectReferenceValue, true);
                        SaveAssets();
                        valueProperty.objectReferenceValue = null;
                    }
                }
                else
                {
                    if (GUI.Button(rect, "Copy As Nested"))
                    {
                        var instance = Object.Instantiate(valueProperty.objectReferenceValue);
                        instance.name = instance.name.Replace("(Clone)", "");
                        AssetDatabase.AddObjectToAsset(instance, property.serializedObject.targetObject);
                        valueProperty.objectReferenceValue = instance;
                        SaveAssets();
                    }
                }
            }
            else
            {
                if (EditorGUI.DropdownButton(rect, new GUIContent("Add Nested"), FocusType.Passive))
                {
                    GenericMenu menu = new GenericMenu();
                    //TODO can try to use generic static class as a cache for this
                    foreach (var t in EditorHelper.GetTypeOfProperty(property).GetGenericArguments())
                    {
                        PopulateMenu(property, menu, t);
                    }
                    menu.ShowAsContext();
                }
            }

            EditorGUI.EndProperty ();
        }

        private void PopulateMenu(SerializedProperty property, GenericMenu menu, System.Type T)
        {
            Assembly assembly = T.Assembly;
            var guids = AssetDatabase.FindAssets("t:script");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var type = AssetDatabase.LoadAssetAtPath(path, typeof(Object));

                // look through all classes to find the fullname of the asset
                bool typeFound = false;

                // make sure that incoming guid type inherits
                foreach(System.Type t in assembly.GetTypes())
                {
                    //Debug.Log($"{t.Name} {T.Name} ? {type.name}| {t.IsSubclassOf(T)}");
                    if (T.IsAssignableFrom(t) && t.Name == type.name)
                    {
                        typeFound = true;
                        break;
                    }
                }
                
                // if the type isn't found then skip
                if(!typeFound) { continue; }
                
                menu.AddItem(
                    new GUIContent(Path.GetFileNameWithoutExtension(path)),
                    false,
                    (userData) =>
                    {
                        SetNestedObject(property, (string) userData);
                    },
                    path);
            }
        }

        private void SetNestedObject(SerializedProperty property, string assetPath)
        {
            var type = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));
            var newProperty = ScriptableObject.CreateInstance(type.name);
            newProperty.name = type.name;
                        
            AssetDatabase.AddObjectToAsset(newProperty, property.serializedObject.targetObject);
            SaveAssets();
                        
            var valueProperty = property.FindPropertyRelative("Value");
            valueProperty.objectReferenceValue = newProperty;
            property.serializedObject.ApplyModifiedProperties();
        }
        
        private void SaveAssets()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}