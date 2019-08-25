using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJuego : MonoBehaviour
{
    public static ControlJuego instance;

    [Header("Elementos UI")]
    public GameObject gameUI;
    public GameObject pauseUI;

    private void Awake() {
        //instance = this;
    }

    private void Start() {
        // Asegura que las interfaces sean las correctas
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    void Update()
    {
        // Esto es solo para pruebas dentro del editor
        #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.Escape)) 
            {
                if (!pauseUI.activeInHierarchy) 
                {
                    PausarJuego();
                } else if (pauseUI.activeInHierarchy) 
                {
                    DesPausarJuego();   
                }
            }
        #endif
    }

    // Metodo que pausa el juego
    public void PausarJuego() {
        Time.timeScale = 0;

        gameUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    // Metodo que despausa el juego
    public void DesPausarJuego() {
        Time.timeScale = 1;

        pauseUI.SetActive(false);
        gameUI.SetActive(true);
    }

    // Condicionales para que se pause cuando se sale de la aplicacion
    // dependiendo de la plataforma es como se utiliza
    #if UNITY_ANDROID
        void OnApplicationFocus ( bool focus )
        {
            if (focus)
                DesPausarJuego();
            else            
                PausarJuego();
        }
    #endif
    
    #if UNITY_EDITOR || UNITY_IOS
        void OnApplicationPause ( bool pause )
        {
            if (pause)    
                PausarJuego();
            else          
                DesPausarJuego();
        }
    #endif

}
