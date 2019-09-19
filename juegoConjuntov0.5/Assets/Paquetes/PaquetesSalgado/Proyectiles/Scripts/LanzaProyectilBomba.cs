using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaProyectilBomba : MonoBehaviour
{
    //Tiempo de aparición de objeto
    public float fireBullet = 0.2f;
    //float fireBullet = 1.0f;
    //Objeto para hacer el pool
    public GameObject bullet;
    //Lista de objetos
    List<GameObject> bullets;
    //Tamaño de lista de pool
    public int poolTam = 2;
    public Transform firePoint;
    //Activa/desactiva los disparos
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        firePoint = firePoint.transform;

        bullets = new List<GameObject>();

        //Creación de la lista de objetos
        for (int i = 0; i < poolTam; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(fireBullet);

        //verificar con un tag de jugador o enemigo y meter un if

        //Debug.Log("Activado");
        //InvokeRepeating("dispara", fireBullet, fireBullet);



        //se manda llamar si el objeto se usa por el jugador
        if (attacking == true)
        {
            Debug.Log("Activado");
            Invoke("dispara", fireBullet);
            attacking = false;
        }

    }




    //funcion para cuando se necesite por parte del jugador (no automatica)
    //Método que activa los diaparos
    public void activarDisparo()
    {
        attacking = true;
    }



    //Método que dispara proyectiles
    private void dispara()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.position = firePoint.position;
                bullets[i].transform.rotation = firePoint.rotation;
                bullets[i].SetActive(true);
                break;
            }
        }
    }
}
