using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
     private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        
        text.text = "score: " + ControlJuego.instance.puntaje;
    }

    void OnEnable() {
         text.text = "score: " + ControlJuego.instance.puntaje;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
