using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 
// Este script tiene funciones auxiliares para los enemigos,
// estas no afectan su comportamiento solo son para Debugging
public class EntityUtils {

    // Dibuja mediante los gizmos un circulo alrededor del origin y cierto tadio
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

    // Metodo que verifica que en la escena exista el objeto de "IA"
    public static void CheckForIAManger() {
        if (GameObject.FindGameObjectWithTag("IA Manager") == null) {
            throw new Exception("A la escena le falta el objeto de control de IA, y este objeto lo nececita para su funcionamiento.");
        } 
    }
}
