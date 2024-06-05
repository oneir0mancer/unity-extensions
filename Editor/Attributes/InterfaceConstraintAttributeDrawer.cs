﻿using UnityEditor;
using UnityEngine;

namespace Oneiromancer.Attributes
{
    [CustomPropertyDrawer(typeof(InterfaceConstraintAttribute))]
    public class InterfaceConstraintAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label, true);
            if (EditorGUI.EndChangeCheck())
            {
                if (property.objectReferenceValue == null) return;
                
                //TODO if property is collection, check each element,
                //TODO if property is SerializedReference, check inner object, also somehow pass constraint to type search
                
                var att = (InterfaceConstraintAttribute)this.attribute;
                bool result = att.Check(property.objectReferenceValue.GetType());
                if (!result)
                {
                    Debug.LogWarning($"Can't apply {property.objectReferenceValue} to {att.ConstrainedType} constraint");
                    property.objectReferenceValue = null;    //TODO cache and undo change ?
                }
            }
        }
    }
}