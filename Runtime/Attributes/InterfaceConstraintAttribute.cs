using System;
using UnityEngine;

namespace Oneiromancer.Attributes
{
    public class InterfaceConstraintAttribute : PropertyAttribute
    {
        public Type ConstrainedType => _type;
        
        private readonly Type _type;

        public InterfaceConstraintAttribute(Type type)
        {
            _type = type;
        }
        
        public bool Check(Type typeToCheck)
        {
            return _type.IsAssignableFrom(typeToCheck);
        }
    }
}