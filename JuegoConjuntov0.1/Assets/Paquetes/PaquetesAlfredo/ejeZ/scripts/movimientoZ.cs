using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoZ : MonoBehaviour
{

    float velocidad = 2.0f;


    public Animator controlAnim;
    public Joystick joystick;
    public Rigidbody2D rb;
    public Vector3 inputJoystick;

    SpriteRenderer tSprite;
    Transform posicion;
    Vector3 mov = new Vector3(0.0f,0.0f,0.0f);

    public bool attacking = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        posicion = this.transform;
        tSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inputJoystick = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
        inputJoystick *= velocidad;

        rb.velocity = new Vector2(inputJoystick.x, inputJoystick.y);
        controlAnim.SetFloat("speed", Mathf.Abs(rb.velocity.magnitude));

        if (rb.velocity.x > 0) {
            transform.localScale = new Vector3(1, 1, 1);
            //controlAnim.SetFloat("speed", inputJoystick.x);
        } else if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //controlAnim.SetFloat("speed", inputJoystick.x);
        }

        /*el ataque se activa desde el boton del canvas por medio de un ONCLICK() haciendo que se active la funcion para poner el positivo 
        el ataque, al finalizarlo, se pasa a negativo y al volver al siguiente fotograma ya no entra mas que a acabar la animacion*/
        if (attacking == true)
        {
            //attacking = true;
            controlAnim.SetBool("atack", attacking);
            attacking = false;
        }
        else
        {
            controlAnim.SetBool("atack", attacking);
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
}
