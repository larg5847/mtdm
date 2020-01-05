using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public Camera mainCam;

    public BoxCollider2D colVertNave;
    public BoxCollider2D colHorAbajo;
    public BoxCollider2D colHor;

    private void Start()
    {
        //Define posición y tamaño de colisionadores
        colHor.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        colHor.offset = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y + 0.5f);

        colHorAbajo.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        colHorAbajo.offset = new Vector2(0f, -(mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y)/2 - 0.5f);

        colVertNave.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        colVertNave.offset = new Vector2(-(mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x)/2 - 0.5f, 0f);
    }
}
