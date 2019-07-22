using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxPlayer : MonoBehaviour
{
    int vida;

    private void Awake()
    {
        vida = 50;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hurtboxPowerUp")
        {
            if (vida < 100)
            {
                vida += 10;
                Debug.Log(vida);
            }
        }
    }
}
