using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    //Tiempo de aparición entre objetos
    private float spawnRate = 3.0f;
    //Tiempo desde la última aparición del objeto
    private float tiempo = 0f;
    //Objeto para hacer el pool
    public GameObject powerUp;
    //Lista de objetos
    List<GameObject> powerUps;
    //Tamaño de lista de pool
    private int poolTam = 2;
    //Índice de objeto
    private int i = 0;
    //Posición de aparición de objeto sobre el eje Y
    private float spawnYPosition = 6.0f;
    //Total de objetos en escena
    private GameObject[] tPowerUps;

    // Start is called before the first frame update
    void Start()
    {
        powerUps = new List<GameObject>();

        //Creación de la lista de objetos
        for (int i = 0; i < poolTam; i++)
        {
            GameObject obj = (GameObject)Instantiate(powerUp);
            obj.SetActive(false);
            powerUps.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= spawnRate && i < poolTam)
        {
            tiempo = 0.0f;

            //Posición random para el objeto sobre el eje X
            float spawnXPosition = Random.Range(-6.0f, 5.0f);

            powerUps[i].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            powerUps[i].transform.rotation = transform.rotation;
            powerUps[i].SetActive(true);
            i++;
        }

        else if (tiempo > spawnRate)
        {
            tiempo = 0f;

            tPowerUps = GameObject.FindGameObjectsWithTag("PowerUp");

            if (tPowerUps.Length == 0)
            {
                i = 0;
            }
        }
    }

}
