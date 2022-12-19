using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private static LayerMask lm = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rb, Vector2 dir)
    {
        if (rb.isKinematic)
        {
            return false;
        }

        float rad = 0.25f;
        float dist = 0.375f;

        RaycastHit2D collision = Physics2D.CircleCast(rb.position, rad, dir.normalized , dist, lm);
        return collision.collider != null && collision.rigidbody != rb;
    }

    public static bool DotTest(this Transform t, Transform collisionT, Vector2 dir)
    {
        Vector2 dirT = collisionT.position - t.position;
        return Vector2.Dot(dirT.normalized, dir) > 0.25f;
    }

}
