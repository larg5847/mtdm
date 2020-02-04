using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtPowerUp : MonoBehaviour
{
    public GameObject parentMyself;

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
        if (collision.gameObject.tag == "hurtboxPlayer")
        {
            parentMyself.SetActive(false);
        }
    }
}
