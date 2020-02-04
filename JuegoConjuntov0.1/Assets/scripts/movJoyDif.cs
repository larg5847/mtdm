using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movJoyDif : Personajes
{
    public joystick1 joy;
    public Camera thisC;
    //private float xmin, xmax, ymin, ymax;
    Vector3 direction;
    public SpriteRenderer tSprite;


    void Awake()
    {
        tSprite.color = Color.HSVToRGB(0f, 0.65f, 0.95f);
        posicionP = this.transform.position;
        vidaP = 1;
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

    //Fución que es llamada por la clase EventoColision
    public void controlVida(Collider2D collider)
    {
        if (collider.gameObject.tag == "hitboxEnemigo" || collider.gameObject.tag == "PowerUp")
        {
            if (collider.gameObject.tag == "hitboxEnemigo")
                vidaP--;

            else
            {
                if (vidaP < 3)
                    vidaP++;
            }

            Debug.Log(vidaP);
            if (vidaP == 0)
            {
                //ControlJuego.instance.EndGame();
                this.gameObject.SetActive(false);
                //parentMyself.SetActive(false);
            }

            //se agregan las siguientes condiciones para el cambio de color del sprite debido a
            //que el personaje es atacado
            else if (vidaP == 2) //solo else
            {
                tSprite.color = Color.HSVToRGB(0f, 0.36f, 1f);
            }

            else if (vidaP == 1)
            {
                tSprite.color = Color.HSVToRGB(0f, 0.65f, 0.95f);
            }

            else
            {
                tSprite.color = Color.HSVToRGB(0f, 0f, 1f);
            }
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
