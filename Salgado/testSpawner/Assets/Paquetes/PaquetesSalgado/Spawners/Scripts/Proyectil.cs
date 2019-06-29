using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    //private GameObject[] gameObjects;
    //private float timeLeft = 5;
    public float velocidad = 10f;
    public float velocidadGiro = -100f;
    public Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        rB.velocity = transform.right * velocidad;
    }

    void FixedUpdate()
    {
        rB.MoveRotation(rB.rotation + velocidadGiro * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    timeLeft -= Time.deltaTime;
    //    if (timeLeft <= 0)
    //    {
    //        gameObjects = GameObject.FindGameObjectsWithTag("hitBoxJugador");

    //        foreach (GameObject powerUps in gameObjects)
    //            Destroy(powerUps);

    //        timeLeft = 20;
    //    }
    //}
}
