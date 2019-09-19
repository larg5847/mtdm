using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilBomba : MonoBehaviour
{
    public float velocidad = 10f;
    public float velocidadGiro = -100f;

    Transform t;
    Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        t.transform.Translate(Vector2.right * velocidad * Time.fixedDeltaTime);
        t.transform.Rotate(Vector3.forward, velocidadGiro * Time.fixedDeltaTime);
    }
}