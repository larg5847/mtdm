using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject[] gameObjects;
    static bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Removal()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Proyectil");

        for (int i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Proyectil")
        {
            Removal();
            Destroy(gameObject);
            hit = true;
        }
    }
}
