using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilFuego : MonoBehaviour
{
    public float velocidad = 13f;

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
    }
}
