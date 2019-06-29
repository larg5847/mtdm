using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //Variables para control de movimiento//
    private float velocidad = 2.0f;
    Transform posicion;
    Vector3 mov = new Vector3(0.0f, 0.0f, 0.0f);
    private float movement = 0.0f;

    private bool facinRight = true;
    private bool facinLeft = false;

    //Variables para hacer pool de objetos//
    public GameObject enemigo;
    public int enemigoPoolSize = 5;
    public float spawnRate = 10f;

    //Colección de enemigos
    private GameObject[] enemigos;
    //Índice de enemigo actual
    private int j = 0;

    //Posición para enemigo que no aparece en pantalla
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float spawnXPosition = 1.0f;

    //Tiempo desde la última aparición
    public float tiempo;


    void Start()
    {
        posicion = this.transform;

        tiempo = 0f;

        //Inicializa colección de enemigos
        enemigos = new GameObject[enemigoPoolSize];
         
        for (int i = 0; i < enemigoPoolSize; i++)
        {
            Debug.Log("Counting");
            enemigos[i] = (GameObject)Instantiate(enemigo, objectPoolPosition, Quaternion.identity);
        }
    }

    
    void Update()
    {
        movimiento();
        enemyPooling();
    }

    //Aparición de enemigos
    private void enemyPooling()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= spawnRate && j < enemigoPoolSize)
        {
            tiempo = 0f;

            float spawnYPosition = Random.Range(0, 5);  //original(-5,5)

            enemigos[j].transform.position = new Vector2(this.posicion.transform.position.x, spawnYPosition);

            j++;
        }

        if (tiempo >= 10f)
        {
            for (int i = 0; i < enemigoPoolSize; i++)
            {
                Debug.Log("Destroying");
                Destroy(enemigos[i]);
            }

            for (int i = 0; i < enemigoPoolSize; i++)
            {
                Debug.Log("Counting");
                enemigos[i] = (GameObject)Instantiate(enemigo, objectPoolPosition, Quaternion.identity);
            }

            j = 0;
        }
    }

    private void movimiento()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Vertical") * velocidad);
            posicion.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + velocidad * Time.deltaTime), 0);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Vertical") * velocidad);
            posicion.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - velocidad * Time.deltaTime), 0);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Horizontal") * velocidad);

            //se necesita esta linea a negativo paara que se conserven los hitboxes en animacion
            //posicion.transform.localScale = new Vector3(-1, 1, 1);
            
            //movimiento
            posicion.transform.position = new Vector3(this.transform.position.x - velocidad * Time.deltaTime, this.transform.position.y, 0);

            if (!facinLeft)
            {
                facinRight = false;
                facinLeft = true;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Horizontal") * velocidad);
            posicion.transform.localScale = new Vector3(1, 1, 1);
            posicion.transform.position = new Vector3(this.transform.position.x + velocidad * Time.deltaTime, this.transform.position.y, 0);

            if(!facinRight)
            {
                facinLeft = false;
                facinRight = true;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
    }
 }
