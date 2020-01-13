using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : Personajes
{
    //Tiempo de aparición entre enemigos
    public float spawnRate = 3f;
    //Objeto para hacer el pool
    public GameObject enemy;
    //Lista de objetos
    List<GameObject> enemies;
    //Tamaño de lista de pool
    public int poolTam = 5;
    //Posición para enemigo que no aparece en pantalla
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float spawnXPosition = 19.0f;
    //Tiempo desde la última aparición del enemigo
    public float tiempo = 0f;
    //Índice de enemigo
    public int j = 0;
    //ïndice de oleadas
    public int k = 2;
    //Selector de posición de spawn en escena
    public int posSel = 1;
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

            //Se selecciona en que posición se muestra en escena
            
            switch (posSel)
            {
                case 1:
                    posicionP = new Vector3(19, -8, 0);
                    break;
                case 2:
                    posicionP = new Vector3(23, -4, 0);
                    break;
                case 3:
                    posicionP = new Vector3(19, 0, 0);
                    break;
                default:
                    posicionP = new Vector3(19, 0, 0);
                    break;
                    
            }

            float spawnYPosition = posicionP.y;
            spawnXPosition = posicionP.x;

            Debug.Log("Posicion: " + posicionP);
            //float spawnYPosition = Random.Range(0, 9);

            
            enemies[j].transform.position = new Vector2(spawnXPosition, spawnYPosition); ;
            enemies[j].transform.rotation = transform.rotation;
            enemies[j].SetActive(true);
                       
            j++;
            posSel++;
            if (posSel > 3)
                posSel = 1;
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
