using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystick1 : MonoBehaviour
{

    //prefab del jugador
    public Transform jugador;
    float velMov = 3.0f;
    bool toqueInicial = false;
    Vector2 puntoA;
    Vector2 puntoB;

    public Transform palanca;
    public Transform baseJ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            puntoA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            palanca.transform.position = puntoA * -1;
            baseJ.transform.position = puntoA * -1;
            //palanca.GetComponent<SpriteRenderer>().enabled=true;
            //baseJ.GetComponent<SpriteRenderer>().enabled = true;
        }
        if(Input.GetMouseButton(0))
        {
            toqueInicial = true;
            puntoB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            toqueInicial = false;
        }
    }

    private void FixedUpdate()
    {
        if(toqueInicial)
        {
            Vector2 separacion = puntoB - puntoB;
            Vector2 direccion = Vector2.ClampMagnitude(separacion, 1.0f);
            moverJugador(direccion);
            palanca.transform.position = new Vector2(puntoA.x + direccion.x, puntoA.y + direccion.y) * -1;
        }
        else
        {
            //palanca.GetComponent<SpriteRenderer>().enabled = false;
            //baseJ.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void moverJugador(Vector2 direccion)
    {
        jugador.Translate(direccion * velMov * Time.deltaTime);
    }
}
