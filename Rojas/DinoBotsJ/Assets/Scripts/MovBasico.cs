using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovBasico : MonoBehaviour{
    
    public Animator animator;
    public Rigidbody2D rb;
    public Joystick joystick;
    public float velocidad;
    private Vector3 movement;

    private void Update() {
        //movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
        movement *= velocidad;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        //transform.position = transform.position + movement * Time.deltaTime;
        rb.velocity = new Vector2(movement.x, movement.y);
    }
}
