using UnityEngine;

namespace FogFormer
{
    public static class WallGrabUtility
    {
        public static bool DetectLedge(Collider2D collider, float height, int directionSign, LayerMask mask, out Vector2 foundFloor)
        {
            var bounds = collider.bounds;
            Vector2 origin = bounds.CenterTop();
            var ceilingCast = Physics2D.Raycast(origin, Vector2.up, height, mask);
            if (ceilingCast)
            {
                foundFloor = Vector2.zero;
                return false;
            }

            origin += Vector2.up * height;
            Vector2 direction = Vector2.right * directionSign;
            //1.5f because it needs to check from collider center to wall, then from wall edge to collider center, then from collider center to collider edge
            var wallCast = Physics2D.Raycast(origin, direction, bounds.size.x * 1.5f, mask);
            if (wallCast)
            {
                foundFloor = Vector2.zero;
                return false;
            }

            origin += direction * bounds.size.x;
            var floorCast = Physics2D.Raycast(origin, Vector2.down, height + bounds.extents.y, mask);
            foundFloor = floorCast.point;
            return floorCast;
        }
    }
}