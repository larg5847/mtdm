using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtJug : MonoBehaviour
{

    //int vida = 100;

    //public GameObject player;
    //Transform jugador;
    //public GameObject parentMyself;

    private void Start()
    {
        //obtenerPadre();
        //SetParent(player);
    }

    public void obtenerPadre()
    {
        //esta linea obtiene al padre de todo, ese es el que desaparecera cuando la vida sea 0
        //jugador = this.transform.root;

        //Debug.Log(jugador.name);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //son colisiones con espada o con el gameobject que da vida
        /*if(collision.gameObject.tag == "hitboxEnemigo")
        {
            vida -= 10;
            Debug.Log(vida);
            if (vida <= 0)
            {

                parentMyself.SetActive(false);
            }
        }*/
    }
    
}
