using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaProyectil : MonoBehaviour
{
    //Tiempo de aparición de objeto
    public float fireBullet = 0.05f;
    //Objeto para hacer el pool
    public GameObject bullet;
    //Lista de objetos
    List<GameObject> bullets;
    //Tamaño de lista de pool
    public int poolTam = 2;
    public Transform firePoint;

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
        if (Input.GetButtonDown("Fire1"))
        {
            Invoke("dispara", fireBullet);
        }
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
