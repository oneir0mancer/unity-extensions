using UnityEngine;

namespace Oneiromancer.Extensions
{
    public static class RandomExtensions
    {
        public static Vector3 OnUnitCircle()
        {
            Vector3 offset = Random.insideUnitSphere;
            offset.y = 0;
            return offset.normalized;
        }
        
        public static char RandomLetter()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            System.Random rand = new System.Random();
            int num = rand.Next(0, chars.Length);
            return chars[num];
        }
    }
}