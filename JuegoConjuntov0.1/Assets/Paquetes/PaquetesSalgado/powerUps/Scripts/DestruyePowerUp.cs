using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruyePowerUp : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("destruye", 3.0f);
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

        else if (collision.gameObject.tag == "hurtboxPlayer")
        {
            Debug.Log("Se colisionó con " + collision.gameObject.tag);
            destruye();
        }

        else if (collision.gameObject.tag == "Proyectil")
        {
            Debug.Log("Se colisionó con " + collision.gameObject.tag);
            destruye();
        }
    }

    //Método que desactiva el objeto
    private void destruye()
    {
        this.gameObject.SetActive(false);
    }
}
