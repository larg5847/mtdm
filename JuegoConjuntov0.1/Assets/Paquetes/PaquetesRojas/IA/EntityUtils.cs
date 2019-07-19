using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityUtils {
    public static void DrawRadiusGizmos(Vector2 origin, float radius) {
        float theta = 0;
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);
        Vector2 pos = origin + new Vector2(x, y);
        Vector2 newPos = pos;
        Vector2 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = radius * Mathf.Cos(theta);
            y = radius * Mathf.Sin(theta);
            newPos = origin + new Vector2(x, y);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        
        Gizmos.DrawLine(pos, lastPos);
    }
}
