using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoZ : MonoBehaviour
{

    float velocidad = 2.0f;

    Transform posicion;
    Vector3 mov = new Vector3(0.0f,0.0f,0.0f);
    // Start is called before the first frame update
    void Start()
    {
        posicion = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            posicion.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + velocidad * Time.deltaTime), (this.transform.position.z + velocidad * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            posicion.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - velocidad * Time.deltaTime), (this.transform.position.z - velocidad * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            posicion.transform.position = new Vector3(this.transform.position.x - velocidad * Time.deltaTime , this.transform.position.y , this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            posicion.transform.position = new Vector3(this.transform.position.x + velocidad * Time.deltaTime, this.transform.position.y , this.transform.position.z);
        }
    }
}
