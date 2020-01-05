using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personajes : MonoBehaviour
{

    int vida;
    float velocidad;
    Vector3 Posicion;
    Rigidbody2D rb;
    SpriteRenderer sprite;

    public Personajes(int vidaP, float velMov /*, tipoDino*/)
    {
        vida = vidaP;
        velocidad = velMov;
    }

    public Personajes()
    {

    }


    public int vidaP
    {
        get => vida;
        set
        {
            vida = value;
        }
    }

    public float velocidadP
    {
        get => velocidad;
        set
        {
            velocidad = value;
        }
    }

    public Vector3 posicionP
    {
        get => Posicion;
        set
        {
            Posicion = value;
        }
    }
    
}
