using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float velocidadProy = -5.0f;
    public Rigidbody2D proyectil;
    public Animator animacion;

    // Start is called before the first frame update
    void Start()
    {
        animacion = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        muevePersonaje();
        lanzaProyectil();
    }

    void muevePersonaje()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
            animacion.SetBool("condWin", false);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
            animacion.SetBool("condWin", false);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
            animacion.SetBool("condWin", false);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
            animacion.SetBool("condWin", false);
        }


    }

    void lanzaProyectil()
    {
        Rigidbody2D proyectilInstance;

        if (Input.GetButtonDown("Fire1"))
        {
            animacion.SetBool("condWin", true);
            proyectilInstance = Instantiate(proyectil, transform.position, Quaternion.Euler(new Vector3(-1, 0, 0)));
            proyectilInstance.velocity = new Vector2(velocidadProy, 0);
            proyectilInstance.name = "Proyectil";
        }

    }
}
