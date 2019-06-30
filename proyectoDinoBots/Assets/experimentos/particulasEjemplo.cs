using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulasEjemplo : MonoBehaviour
{

    bool activado = false;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ps.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "hitboxJugador")
        {
            ps.Play();
        }
    }
}
