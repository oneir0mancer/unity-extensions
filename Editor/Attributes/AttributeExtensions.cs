﻿using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Oneiromancer.Attributes
{
    public static class AttributeExtensions
    {
        public static T GetPropertyAttribute<T>(this SerializedProperty prop, bool inherit, out Type fieldType)
            where T : PropertyAttribute
        {
            Type t = prop.serializedObject.targetObject.GetType();

            fieldType = null;
            MemberInfo affectedField = null;
            foreach (var name in prop.propertyPath.Split('.'))
            {
                var field = t.GetField(name, (BindingFlags) (-1));    //TODO: what does it mean ?
                if (field != null)
                {
                    affectedField = field;
                    fieldType = field.FieldType;
                    break;
                }

                var property = t.GetProperty(name, (BindingFlags) (-1));
                if (property != null)
                {
                    affectedField = property;
                    fieldType = property.PropertyType;
                    break;
                }

                return null;
            }

            if (affectedField == null) return null;

            T[] attributes = affectedField.GetCustomAttributes(typeof(T), inherit) as T[];
            return attributes.Length > 0 ? attributes[0] : null;
        }
    }
}