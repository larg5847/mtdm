using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reinicioNivel : MonoBehaviour
{
    int numEscena;
    // Start is called before the first frame update
    void Start()
    {
        numEscena = SceneManager.GetActiveScene().buildIndex;
    }
    public void cargarEscena()
    {
        Debug.Log("es la escena..." + numEscena);
        SceneManager.LoadScene(numEscena);
    }
}
