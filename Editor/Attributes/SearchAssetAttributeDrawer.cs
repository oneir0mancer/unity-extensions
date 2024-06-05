﻿using Oneiromancer.SearchProviders;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Oneiromancer.Attributes
{
    [CustomPropertyDrawer(typeof(SearchAssetAttribute))]
    public class SearchAssetAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            position.width -= 60;
            EditorGUI.ObjectField(position, property, label);

            position.x += position.width;
            position.width = 60;
            if (GUI.Button(position, new GUIContent("Find"), EditorStyles.popup))
            {
                //var attr = property.GetPropertyAttribute<SearchAssetAttribute>(true, out var affectedFieldType);
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
                    new AssetSearchProvider(fieldInfo.FieldType, property));    //TODO cache tree?
                
                //TODO: can you open already on some selected node?
            }
        
        }
    }
}