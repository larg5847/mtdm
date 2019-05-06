using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public GameObject[] gameObjects;
    public float timeLeft = 20;
    public float multiplicador = 0.5f;

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
            gameObjects = GameObject.FindGameObjectsWithTag("powerUp");

            foreach (GameObject powerUps in gameObjects)
                Destroy(powerUps);

            timeLeft = 20;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "player")
        {
            pickUp(other);
        }
    }

    private void pickUp(Collision2D player)
    {
        player.transform.localScale *= multiplicador;

        Destroy(gameObject);
    }
}
