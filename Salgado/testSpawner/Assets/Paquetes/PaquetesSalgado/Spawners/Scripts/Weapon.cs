using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject proyectilPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            dispara();
        }
    }

    private void dispara()
    {
        Instantiate(proyectilPrefab, firePoint.position, firePoint.rotation);
    }
}
