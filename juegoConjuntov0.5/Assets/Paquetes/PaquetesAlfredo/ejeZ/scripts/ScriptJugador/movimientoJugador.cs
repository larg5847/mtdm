using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoJugador : MonoBehaviour
{

    private float velocidad = 4.0f;  //4 - 6 -8f

    public int intentos = 3;
    public Animator controlAnim;
    public Joystick joystick;
    public Rigidbody2D rb;
    public Vector3 inputJoystick;
    public Transform firePoint;

    //Antes privado, ahora se utiliza cuando es atacado el dinosaurio
    public SpriteRenderer tSprite;
    private Transform posicion;
    private Vector3 mov = new Vector3(0.0f,0.0f,0.0f);

    private bool attacking = false;

    private bool facinLeft = true;
    private bool facinRight = false;

    private Vector2 posInicial;


    //agregarle vida al jugador 
    int vida = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        posicion = this.transform;
        //Para parámetro privado
        //tSprite = GetComponent<SpriteRenderer>();
        tSprite.color = Color.HSVToRGB(0f, 0f, 1f);
        posInicial = transform.position;
        firePoint = firePoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        inputJoystick = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
        inputJoystick *= velocidad;

        rb.velocity = new Vector2(inputJoystick.x, inputJoystick.y);
        controlAnim.SetFloat("speed", Mathf.Abs(rb.velocity.magnitude));

        if (rb.velocity.x > 0) {
            this.transform.localScale = new Vector3(.3f, .3f, 1);

            if (!facinLeft)
            {
                facinRight = false;
                facinLeft = true;
                firePoint.transform.Rotate(0.0f, 180.0f, 0.0f);
            }

            //controlAnim.SetFloat("speed", inputJoystick.x);
        } else if(rb.velocity.x < 0)
        {
            this.transform.localScale = new Vector3(-.3f, .3f, 1);

            if (!facinRight)
            {
                facinLeft = false;
                facinRight = true;
                firePoint.transform.Rotate(0.0f, 180.0f, 0.0f);
            }
            //controlAnim.SetFloat("speed", inputJoystick.x);
        }

        /*el ataque se activa desde el boton del canvas por medio de un ONCLICK() haciendo que se active la funcion para poner el positivo 
        el ataque, al finalizarlo, se pasa a negativo y al volver al siguiente fotograma ya no entra mas que a acabar la animacion*/
        if (attacking == true)
        {
            //attacking = true;
            controlAnim.SetBool("attack", attacking);
            attacking = false;
        }
        else
        {
            controlAnim.SetBool("attack", attacking);
        }

        /*
        if(Input.GetKey(KeyCode.W))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Vertical") * velocidad);
            posicion.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + velocidad * Time.deltaTime), 0);
            controlAnim.SetFloat("speed", movement);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Vertical") * velocidad);
            posicion.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - velocidad * Time.deltaTime), 0);
            controlAnim.SetFloat("speed", movement);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Horizontal") * velocidad);

            //se necesita esta linea a negativo paara que se conserven los hitboxes en animacion
            posicion.transform.localScale = new Vector3(-1, 1, 1);
            //movimiento
            posicion.transform.position = new Vector3(this.transform.position.x - velocidad * Time.deltaTime , this.transform.position.y , 0);
            controlAnim.SetFloat("speed", movement);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = Mathf.Abs(Input.GetAxisRaw("Horizontal") * velocidad);
            posicion.transform.localScale = new Vector3(1, 1, 1);
            posicion.transform.position = new Vector3(this.transform.position.x + velocidad * Time.deltaTime, this.transform.position.y , 0);
            controlAnim.SetFloat("speed", movement);
        }
        else
        {
            controlAnim.SetFloat("speed", 0.0f);
        }
        */
    }

    


    public void activarAtt()
    {
        attacking = true;
        //controlAnim.SetBool("atack", attacking);
    }



    public void bajarVida(Collider2D collider)
    {
        if (collider.gameObject.tag == "hitboxEnemigo")
        {
            //vida -= 10;
            //Debug.Log(vida);

            intentos --;
            Debug.Log(intentos);
            if (intentos == 0)//(vida <= 0)
            {
                ControlJuego.instance.EndGame();
                this.gameObject.SetActive(false);
                //parentMyself.SetActive(false);
            }

            //se agregan las siguientes condiciones para el cambio de color del sprite debido a
            //que el personaje es atacado
            else if (intentos == 2) //solo else
            {
                tSprite.color = Color.HSVToRGB(0f,0.36f,1f);
                //transform.position = posInicial;
            }

            else if (intentos == 1)
            {
                tSprite.color = Color.HSVToRGB(0f,0.65f,0.95f);
                //transform.position = posInicial;
            }
        }
    }
}
