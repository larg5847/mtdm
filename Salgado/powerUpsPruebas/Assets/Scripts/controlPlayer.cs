using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPlayer : MonoBehaviour
{
    public Rigidbody2D personaje;
    public Rigidbody2D powerUp;
    public Vector3 movimiento;
    public int velocidad = 5;

    // Start is called before the first frame update
    void Start()
    {
        personaje = this.gameObject.GetComponent<Rigidbody2D>();
        InvokeRepeating("powerUpSpawn", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        obtenerInputs();
        muevePersonaje();
    }

    private void muevePersonaje()
    {
        // transform.position = transform.position + movimiento * Time.deltaTime;
        personaje.velocity = new Vector2(movimiento.x, movimiento.y) * velocidad;
    }

    private void obtenerInputs()
    {
        movimiento = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    }

    private void powerUpSpawn()
    {
        Rigidbody2D powerUpInstance;

        powerUpInstance = Instantiate(powerUp, new Vector3(Random.Range(-10, 10), Random.Range(5, 10), -5),
            Quaternion.Euler(new Vector3(0, 0, 0)));
        powerUpInstance.tag = "powerUp";
    }
}
