using UnityEngine;

namespace FogFormer
{
    public static class BoundsUtility
    {
        public static Vector2 CenterLeft(this Bounds bounds) =>
            (Vector2) bounds.center + (Vector2.left * bounds.extents.x);

        public static Vector2 CenterRight(this Bounds bounds) =>
            (Vector2) bounds.center + (Vector2.right * bounds.extents.x);

        public static Vector2 CenterTop(this Bounds bounds) =>
            (Vector2) bounds.center + (Vector2.up * bounds.extents.x);
        
        public static Vector2 CenterBottom(this Bounds bounds) =>
            (Vector2) bounds.center + (Vector2.down * bounds.extents.x);
    }
}