using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comportEnem : MonoBehaviour
{

    //definicion de los componentes de movimiento del enemigo
    //este script esta alejado del hitbox que es aparte
    Animator animEnem;
    Transform posicionEste;

    float velMov = 1.0f;
    Vector3 mov = new Vector3(0.0f, 0.0f, 0.0f);

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        animEnem = GetComponent<Animator>();
        posicionEste = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        movimientoEnem();
    }


    void movimientoEnem()
    {
        //caminar, si se encuentra al objetivo acercarse y atacar, sino, caminar hasta el fondo
        posicionEste.transform.position = new Vector3((this.transform.position.x - velMov * Time.deltaTime), this.transform.position.y, 0);
        animEnem.SetFloat("speed", 1.0f);

    }
}
