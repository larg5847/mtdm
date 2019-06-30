using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoZ : MonoBehaviour
{

    float velocidad = 2.0f;
    public Animator controlAnim;
    SpriteRenderer tSprite;
    Transform posicion;
    Vector3 mov = new Vector3(0.0f,0.0f,0.0f);


    bool attacking = false;
    float movement = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        posicion = this.transform;
        tSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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

       if (Input.GetKey(KeyCode.Space))
        {
            attacking = true;
            controlAnim.SetBool("atack", attacking);
        }
        else
        {
            attacking = false;
            controlAnim.SetBool("atack", attacking);
        }

    }
}
