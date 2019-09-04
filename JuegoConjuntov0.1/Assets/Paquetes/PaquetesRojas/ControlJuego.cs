using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJuego : MonoBehaviour
{
    public static ControlJuego instance;

    // Control de estado actual del juego
    [Header("Estado Juego")]
    public bool pararEnemigos = false;  // Forza a parar todos los enemigos del juego

    [Header("Elementos UI")]
    public GameObject gameUI;           // Objeto UI durante el juego
    public GameObject pauseUI;          // Objeto UI cuando esta pausado
    public GameObject endUI;            // Objeto UI cuando perdio el jugador

    private void Awake() {
        instance = this;
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

    // Metodo que se llama cuando el jugador pierde
    public void EndGame() {
        // Primero para a todos los enemigos
        pararEnemigos = true;
        // Activa el UI de fin del juego
        endUI.SetActive(true);
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
