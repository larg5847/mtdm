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

    //Método que desactiva el objeto
    private void destruye()
    {
        this.gameObject.SetActive(false);
    }
}
