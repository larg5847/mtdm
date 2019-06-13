using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemigo;
    public int enemigoPoolSize = 5;
    public float spawnRate = 3f;
    
    //Colección de enemigos
    private GameObject[] enemigos;
    //Índice de enemigo actual
    private int j = 0;

    //Posición para enemigo que no aparece en pantalla
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float spawnXPosition = 1f;

    //Tiempo desde la última aparición
    private float tiempo;


    void Start()
    {
        tiempo = 0f;

        //Inicializa colleción de enemigos
        enemigos = new GameObject[enemigoPoolSize];
         
        for (int i = 0; i < enemigoPoolSize; i++)
        {
            enemigos[i] = (GameObject)Instantiate(enemigo, objectPoolPosition, Quaternion.identity);
        }
    }

    //Aparición de enemigos hasta que el juego no se haya terminado
    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= spawnRate)
        {
            tiempo = 0f;

            float spawnYPosition = Random.Range(-5, 5);

            enemigos[j].transform.position = new Vector2(spawnXPosition, spawnYPosition);

            j++;

            if (j >= enemigoPoolSize)
            {
                j = 0;
            }
        }
    }
}
