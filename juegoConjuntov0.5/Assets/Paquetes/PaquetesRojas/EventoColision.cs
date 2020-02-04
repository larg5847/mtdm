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
        // Manda a llamar el evento

        if(this.tag == "hurtboxJugador")
        {
            if(col.gameObject.tag == "hitboxEnemigo")
            OnTriggerEnter.Invoke(col);
        }

        if (this.tag == "hurtboxEnemigo")
        {
            if (col.gameObject.tag == "hitboxJugador")
                OnTriggerEnter.Invoke(col);
        }

        if (this.tag == "hurtboxJugador")
        {
            if (col.gameObject.tag == "PowerUp")
                OnTriggerEnter.Invoke(col);
        }

        /*if(OnTriggerEnter != null && col.gameObject.tag != "colisionProyectil")
            OnTriggerEnter.Invoke(col);*/
    }
}
