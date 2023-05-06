//modified from: https://forum.unity.com/threads/finally-a-serializable-dictionary-for-unity-extracted-from-system-collections-generic.335797/#post-2173836

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Oneiromancer.Utils
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializedDictionaryDrawer : PropertyDrawer
    {
        private const float _itemsButtonWidth = 18f;
        private const float _mainButtonWidth = 40f;
        private const float _itemHeight = 20f;
        
        private IDictionary _dictionary;
        private bool _foldout;
 
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            CheckInitialize(property, label);
            if (_foldout)
                return (_dictionary.Count + 1) * _itemHeight;
            return _itemHeight;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            CheckInitialize(property, label);
            
            position.height = _itemHeight;
 
            var foldoutRect = position;
            foldoutRect.width -= 2 * _mainButtonWidth;
            EditorGUI.BeginChangeCheck();
            if (!_foldout) label.text += $" ({_dictionary.Count} items)";
            _foldout = EditorGUI.Foldout(foldoutRect, _foldout, label, true);
            
            //if (EditorGUI.EndChangeCheck())
            //    EditorPrefs.SetBool(label.text, _foldout);
            
            if (!_foldout) return;
            
            var buttonRect = position;
            buttonRect.x = position.width - _mainButtonWidth + position.x;
            buttonRect.width = _mainButtonWidth;
            
            if (GUI.Button(buttonRect, new GUIContent("Clear", "Clear dictionary"), EditorStyles.miniButtonRight))
            {
                ClearDictionary();
            }
            
            buttonRect.x -= _mainButtonWidth;
            
            //TODO need to create a separate temporary field for adding items
            
            if (GUI.Button(buttonRect, new GUIContent("Add", "Add item"), EditorStyles.miniButtonLeft))
            {
                AddNewItem((dynamic)_dictionary);
            }

            OnGUI(position, property, label, (dynamic) _dictionary);
        }
        
        public void OnGUI<TK, TV>(Rect position, SerializedProperty property,
            GUIContent label, SerializedDictionary<TK, TV> dictionary)
        {
            foreach (var item in dictionary)
            {
                var key = item.Key;
                var value = item.Value;
 
                position.y += _itemHeight;
 
                var keyRect = position;
                keyRect.width /= 2;
                keyRect.width -= 4;
                EditorGUI.BeginChangeCheck();
                var newKey = DoField(keyRect, typeof(TK), key);
                if (EditorGUI.EndChangeCheck())
                {
                    try
                    {
                        dictionary.Remove(key);
                        dictionary.Add(newKey, value);
                    }
                    catch(Exception e)
                    {
                        Debug.Log(e.Message);
                    }
                    break;
                }
 
                var valueRect = position;
                valueRect.x = position.width / 2 + 15;
                valueRect.width = keyRect.width - _itemsButtonWidth;
                EditorGUI.BeginChangeCheck();
                value = DoField(valueRect, typeof(TV), value);
                if (EditorGUI.EndChangeCheck())
                {
                    dictionary[key] = value;
                    break;
                }
 
                var removeRect = valueRect;
                removeRect.x = valueRect.xMax + 2;
                removeRect.width = _itemsButtonWidth;
                if (GUI.Button(removeRect, new GUIContent("x", "Remove item"), EditorStyles.miniButtonRight))
                {
                    dictionary.Remove(key);
                    break;
                }
            }
        }
        
        private void CheckInitialize(SerializedProperty property, GUIContent label)
        {
            if (_dictionary == null)
            {
                var target = property.serializedObject.targetObject;
                _dictionary = fieldInfo.GetValue(target) as IDictionary;
                _foldout = EditorPrefs.GetBool(label.text);
            }
        }
        
        private void ClearDictionary()
        {
            _dictionary.Clear();
        }
        
        private void AddNewItem<TK, TV>(SerializedDictionary<TK, TV> dictionary)
        {
            TK key;
            if (typeof(TK) == typeof(string))
                key = (TK)(object)"";
            else key = default;
 
            var value = default(TV);
            try
            {
                dictionary.Add(key, value);
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        
        private static readonly Dictionary<Type, Func<Rect, object, object>> FieldMapping =
            new Dictionary<Type,Func<Rect,object,object>>
            {
                { typeof(int), (rect, value) => EditorGUI.IntField(rect, (int)value) },
                { typeof(float), (rect, value) => EditorGUI.FloatField(rect, (float)value) },
                { typeof(string), (rect, value) => EditorGUI.TextField(rect, (string)value) },
                { typeof(bool), (rect, value) => EditorGUI.Toggle(rect, (bool)value) },
                { typeof(Vector2), (rect, value) => EditorGUI.Vector2Field(rect, GUIContent.none, (Vector2)value) },
                { typeof(Vector3), (rect, value) => EditorGUI.Vector3Field(rect, GUIContent.none, (Vector3)value) },
                { typeof(Bounds), (rect, value) => EditorGUI.BoundsField(rect, (Bounds)value) },
                { typeof(Rect), (rect, value) => EditorGUI.RectField(rect, (Rect)value) },
            };
 
        private static T DoField<T>(Rect rect, Type type, T value)
        {
            if (FieldMapping.TryGetValue(type, out var field))
                return (T)field(rect, value);
 
            if (type.IsEnum)
                return (T)(object)EditorGUI.EnumPopup(rect, (Enum)(object)value);
 
            if (typeof(UnityEngine.Object).IsAssignableFrom(type))
                return (T)(object)EditorGUI.ObjectField(rect, (UnityEngine.Object)(object)value, type, true);
 
            Debug.LogError("Type is not supported: " + type);
            return value;
        }
    }
}