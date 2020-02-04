using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Evento personalizado con parametro de Collider2D
[System.Serializable]
public class EventCollider2D : UnityEvent<Collider2D> { }

public class EventoColision : MonoBehaviour
{
    public EventCollider2D OnTriggerEnter;

    void OnTriggerEnter2D(Collider2D col) {

        //Se manda a llamar el evento cuando el collider del hurtboxJugador colisionó
        //con el del PowerUp
        if(this.tag == "hurtboxJugador")
        {
            if (col.gameObject.tag == "PowerUp")
            {
                OnTriggerEnter.Invoke(col);
            }
                
        }
    }
}
