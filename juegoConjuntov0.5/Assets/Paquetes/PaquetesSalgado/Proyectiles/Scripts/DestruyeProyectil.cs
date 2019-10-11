using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruyeProyectil : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("destruye", 2.0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hurtboxJugador")
        {
            Debug.Log("se colisiono con " + collision.gameObject.tag);
            destruye();
        }

        else if (collision.gameObject.tag == "PowerUp")
        {
            Debug.Log("Se colisionó con " + collision.gameObject.tag);
            destruye();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "colisionNivel")
        {
            destruye();
        }
    }

    //Método que desactiva el objeto
    private void destruye()
    {
        this.gameObject.SetActive(false);
    }
}
