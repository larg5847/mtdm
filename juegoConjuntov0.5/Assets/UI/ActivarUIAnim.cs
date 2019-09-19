using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarUIAnim : MonoBehaviour
{
    // Script que pone una animacion cuando se activa el objeto
    public Animation anim;              // Controlador de animaciones
    public AnimationClip animClip;      // El clip de la animacion

    // Cuando se active el objeto
    public void OnEnable() {
        // Checa que si este asignado el controlador de animaciones
        if(anim != null) {
            // Pone la naimacion
            anim.clip = animClip;
            anim.Play();
        }
    }
}
