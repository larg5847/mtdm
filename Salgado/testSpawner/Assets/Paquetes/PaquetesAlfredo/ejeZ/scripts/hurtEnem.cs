using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtEnem : MonoBehaviour
{
    // Start is called before the first frame update
    int vida;
    public GameObject parentMyself;
    ParticleSystem ps;

    void Awake()
    {
        vida = 100;
        ps = GetComponent<ParticleSystem>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(vida);
        //t.position = new Vector3(t.position.x + (3.0f * Time.deltaTime), t.position.y, 0.0f);
    }



    //como el hurtbox es trigger por ser accionado por el hitbox, el hitbox tiene rigidbody fixed por no necesitar 
    //gravedad
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hitboxJugador")
        {
            vida -= 10;
            Debug.Log(vida);
            ps.Play();
            if (vida <= 0)
            {
                parentMyself.SetActive(false);
            }
        }
    }
}
