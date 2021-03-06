﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //Tiempo de aparición entre enemigos
    public float spawnRate = 2f;    //ANTERIOR == 3
    //Objeto para hacer el pool
    public GameObject enemy;
    //Lista de objetos
    List<GameObject> enemies;
    //Tamaño de lista de pool
    int poolTam = 8;  //ANTERIOR == 4
    //Posición para enemigo que no aparece en pantalla
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float spawnXPosition = 19.0f;
    //Tiempo desde la última aparición del enemigo
    public float tiempo = 0f;
    //Índice de enemigo
    public int j = 0;
    //ïndice de oleadas
    int k = 50;
    //Total de objetos en escena
    public GameObject[] tEnemigos;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();

        //Creación de la lista de objetos
        for (int i = 0; i < poolTam; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemy);
            obj.SetActive(false);
            enemies.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= spawnRate && j < poolTam && k > 0)
        {
            tiempo = 0f;

            //modifique este valor de 9 a 5
            float spawnYPosition = Random.Range(0, 4.5f);

            
            enemies[j].transform.position = new Vector2(spawnXPosition, spawnYPosition); ;
            enemies[j].transform.rotation = transform.rotation;
            enemies[j].SetActive(true);
                       
            j++;
        }

        else if (tiempo > spawnRate)
        {
            tiempo = 0f;

            tEnemigos = GameObject.FindGameObjectsWithTag("Enemigo");

            if (tEnemigos.Length == 0)
            {
                k--;
                j = 0;
            }
        }
    }
}
