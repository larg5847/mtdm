using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMaxRecord : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        
        if(PlayerPrefs.HasKey("max_puntos")) {
            text.text = "RECORD: " + PlayerPrefs.GetInt("max_puntos");
        } else {
            text.text = "RECORD: 0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
