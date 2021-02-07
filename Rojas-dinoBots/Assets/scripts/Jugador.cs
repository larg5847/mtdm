using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 movimiento;

    private void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        ObtenerInputs();
        Mover();
    }

    private void FixedUpdate() {
        //Mover();
    }

    private void Mover() {
        // transform.position = transform.position + movimiento * Time.deltaTime;
        rb.velocity = new Vector2(movimiento.x, movimiento.y) * 3.0f;
    }

    private void ObtenerInputs() {
        movimiento = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    }
}
