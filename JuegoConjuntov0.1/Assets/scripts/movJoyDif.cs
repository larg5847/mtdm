using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movJoyDif : Personajes
{
    public joystick1 joy;
    public Camera thisC;
    //private float xmin, xmax, ymin, ymax;
    Vector3 direction;


    void Awake()
    {
        posicionP = this.transform.position;
        vidaP = 3;
        velocidadP = 5.0f;
        Debug.Log("Personaje-> Vida: " + vidaP + " Vel: " + velocidadP);
    }


    // Update is called once per frame
    void Update()
    {

        direction = joy.InputDirection;

        Debug.Log(direction);

        if (direction.magnitude != 0)
        {
            posicionP += direction * velocidadP* Time.deltaTime;
            //posicionP = new Vector3(Mathf.Clamp(transform.position.x, xmin, xmax), Mathf.Clamp(transform.position.y, ymin, ymax), 0f);//to restric movement of player
            //Debug.Log("se esta sumando?: " + posicionP);
            this.transform.position = posicionP;

            
        }
    }
    /// <summary>
    /// funcion start ayuda a cargatr datos al inicio del nivel, no al inicio del juego como awakw
    /// /// </summary>
    void Start()
    {
        Debug.Log(thisC.orthographicSize);

        /*Initialization of boundaries
        xmax = Screen.height / 9; // I used 50 because the size of player is 100*100

        Debug.Log("tam de lugar maximo = " + xmax);

        xmin = 0;
        ymax = Screen.width / 16;

        Debug.Log("tam de lugar maximo = " + ymax);

        ymin = 0*/
    }
}
