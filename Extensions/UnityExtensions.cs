using UnityEngine;

namespace Oneiromancer.Extensions
{
    public static class UnityExtensions
    {
        /// Check if LayerMask contains layer
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        /// Returns perpendicular vector in 2d space, rotated clockwise/counter-clocwise
        public static Vector2 Perpendicular(this Vector2 vector, bool clockwise=true)
        {
            int sign = clockwise ? 1 : -1;
            return new Vector2(sign * vector.y,-sign * vector.x);
        }
    
        /// Returns perpendicular of a vector projected to in XZ plane, rotated clockwise/counter-clocwise
        public static Vector3 PerpendicularInGroundPlane(this Vector3 vector, bool clockwise=true)
        {
            int sign = clockwise ? 1 : -1;
            return new Vector3(sign * vector.z, 0, -sign * vector.x);
        }
        
        public static void LerpRotateTowards(this Transform transform, Vector2 direction, float rotationSpeed, bool instant = false)
        {
            float angle = Vector2.SignedAngle(transform.right, direction);
            if (!instant) angle = Mathf.LerpAngle(0, angle, Time.deltaTime * rotationSpeed);
            transform.Rotate(Vector3.forward, angle, Space.Self);
        }

        public static void RotateTowards(this Transform transform, Vector2 direction, float rotationSpeed, bool upward = true)
        {
            if (!upward) direction = Vector2.Perpendicular(direction);
            var rot = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.transform.rotation, rot,
                rotationSpeed * Mathf.Rad2Deg * Time.deltaTime);    //TODO: Slerp?
        }
    }
}
