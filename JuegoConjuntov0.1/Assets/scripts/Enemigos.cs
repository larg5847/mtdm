using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : Personajes
{
    string tipoDino;

    private void Awake()
    {
        tipoDino = "bronto";
        switch (tipoDino)
        {
            case "bronto":
                vidaP = 4;
                velocidadP = 3.0f;
                break;
        }

        Debug.Log("Enemigo-> Vida: " + vidaP + " Velocidad : " + velocidadP);
    }

    public Enemigos(int vida, float velocidad) : base(vida, velocidad)
    { 

    }

    public string _tipoDino
    {
        get => tipoDino;
        set => tipoDino = value;
    }
}
