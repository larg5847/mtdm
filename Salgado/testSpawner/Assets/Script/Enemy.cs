using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] gameObjects;
    public float timeLeft = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            gameObjects = GameObject.FindGameObjectsWithTag("Enemigo");

            foreach (GameObject powerUps in gameObjects)
                Destroy(powerUps);

            timeLeft = 20;
        }
    }
}
