using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    //Variables para control de movimiento//
    private float velocidad = 2.0f;
    Transform posicion;
    Vector3 mov = new Vector3(0.0f, 0.0f, 0.0f);
    private float movement = 0.0f;
    public Transform firePoint;

    //Vida del jugador
    public int vida;

    private bool facinRight = true;
    private bool facinLeft = false;

    void Start()
    {
        posicion = this.transform;

        firePoint = firePoint.transform;

        vida = 100;
    }


    void Update()
    {
        movimiento();
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
            posicion.transform.localScale = new Vector3(-1, 1, 1);

            //movimiento
            posicion.transform.position = new Vector3(this.transform.position.x - velocidad * Time.deltaTime, this.transform.position.y, 0);

            if (!facinLeft)
            {
                facinRight = false;
                facinLeft = true;
                firePoint.transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Horizontal") * velocidad);
            posicion.transform.localScale = new Vector3(1, 1, 1);
            posicion.transform.position = new Vector3(this.transform.position.x + velocidad * Time.deltaTime, this.transform.position.y, 0);

            if (!facinRight)
            {
                facinLeft = false;
                facinRight = true;
                firePoint.transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
    }

    public void powerUp(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            velocidad += 1.0f;
            Debug.Log("La velocidad aumentó a " + velocidad);

            if (vida < 100)
            {
                vida += 10;
                Debug.Log("La vida aumentó a " + vida);
            }
        }
    }
}
