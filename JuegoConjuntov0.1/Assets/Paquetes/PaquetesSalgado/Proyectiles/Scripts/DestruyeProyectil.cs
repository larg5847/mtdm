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

    //Método que desactiva el objeto
    private void destruye()
    {
        this.gameObject.SetActive(false);
    }
}
