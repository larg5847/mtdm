using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cargarEscenas : MonoBehaviour
{
   public void cargarEscena(int numEscena)
    {
        SceneManager.LoadScene(numEscena);
    }
}
