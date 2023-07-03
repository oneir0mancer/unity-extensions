using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Oneiromancer.Utils
{
    //See: https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs
    public static class EditorHelper
    {
        /// get target object is it is a part of a nested class inside SerializedObject. But ignores arrays!
        public static object GetTargetObjectOfProperty(SerializedProperty prop, Object serializedObject)
        {
            var elements = prop.propertyPath.Split('.');
            object targetObj = serializedObject;
            foreach (var element in elements)
            {
                targetObj = GetValue_Imp(targetObj, element);
            }
            return targetObj;
        }
        
        private static object GetValue_Imp(object source, string name)
        {
            if (source == null) return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null) return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null) return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }
    }
}