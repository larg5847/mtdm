using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlCharacter : MonoBehaviour
{
    public Rigidbody2D personaje;
    public int velocidad = 5;
    public int fuerzaSalto = 40;

    // Start is called before the first frame update
    void Start()
    {
        personaje = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        muevePersonaje();
    }

    void muevePersonaje()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        else if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);

        else if (Input.GetKey(KeyCode.W))
            personaje.AddForce(new Vector2(0, fuerzaSalto));
    }
}
