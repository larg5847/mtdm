using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxPlayer : MonoBehaviour
{
    public EventCollider2D OnTriggerEnter;

    void OnTriggerEnter2D(Collider2D col)
    {
        // Manda a llamar el evento
        if (OnTriggerEnter != null)
            OnTriggerEnter.Invoke(col);
    }
}
