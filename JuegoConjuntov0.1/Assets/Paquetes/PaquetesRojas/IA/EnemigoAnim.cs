using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAnim : MonoBehaviour
{
    public float speed = 1.0f;                  // Velocidad del jugador
    public float followRadius = 5.0f;           // Radio en el que empieza a seguir al jugador
    public float attackRadius = 1.0f;           // Radio en el que empieza a atacar al jugador
    public int vida = 100;

    private EntityController controller;        // EntityController para controlar el movimiento mediante IA
    public Animator animator;                   // Controlador de las animaciones
    public GameObject player;                   // GameObject del jugador

    private Vector2 targetPos;                  // La posicion objetivo actual
    private bool followingPlayer;               // Booleano para checar si se esta siguiendo al juador

    // Control de la maquina de estado
    private Task task;                          // Task con informacion de la courutina que se esta corriendo
    private string state;                       // String con el estado actual del Entity
    private float playerDist;                   // La distacia hacia el juador

    void Awake() {
        // Si no tiene el comoponente de EntityController se lo agrega 
        if( (controller = this.GetComponent<EntityController>()) == null ) {
            controller = this.gameObject.AddComponent<EntityController>();
        }

        // Cambia la velocidad del controlador
        controller.speed = speed;
    }

    void Start() {
        // Setup inicial
        playerDist = float.PositiveInfinity;
        state = "GoToObjective";

        // Verifica que exista el objeto de control de IA
        EntityUtils.CheckForIAManger();
    }

    public void RecibirMadrazo(Collider2D col) {
        if(col.tag == "hitboxJugador") {
            vida -= 10;
            if(vida == 0) {
                Morir();
            }
        }
    }

    private void Morir() {

    }

    // Courutina que sigue al jugador
    IEnumerator FollowPlayer() {
        // Se aseguara que ya paso un tiempo desde que se cargo la escena
        if(Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(0.3f);
        
        // Entra en un update constante
        while(true) {
            // El target es el juador
            targetPos = player.transform.position;

            // Dependiendo si esta alfrente o atras el jugador es su posicion objetivo
            if(this.transform.position.x >= player.transform.position.x)
                targetPos.x += attackRadius;    // le suma el radio de ataque para que se ponga alfrenete del jugador
            else 
                targetPos.x -= attackRadius;    // le suma el radio de ataque para que se ponga alfrenete del jugador

            // Ordena al controlador a ir al target pos
            controller.FollowObject(targetPos);
            followingPlayer = true;

            // Si la disntancia es mayor a la de seguir mejor ir a objetivo
            if(playerDist > followRadius) {
                state = "GoToObjective";
                yield break;
            }

            // Si esta suficientemente cerca puede atacarlo
            if(playerDist <= attackRadius * 2.5f) {
                state = "AttackPlayer";
                yield break;
            }

            // Espera 0.2 segundos antes de volver a actualizar
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Courutina que se mueve hasta el objetivo
    IEnumerator GoToObjective() {
        // Se aseguara que ya paso un tiempo desde que se cargo la escena
        if(Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(0.3f);
        
        // Entra en un update constante
        while(true) {
            // Mueve el enemigo en direccion a la izquierda
            controller.MoverEnDireccion(new Vector2(-1, 0));
            followingPlayer = false;

            // Si esta suficientemente cerca al jugador, lo ataca
            if(playerDist <= followRadius) {
                state = "FollowPlayer";
                yield break;
            }

            // Espera 0.2 segundos antes de volver a actualizar
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Courutina que ataca al jugador
    IEnumerator AttackPlayer() {
        // Entra en un update constante
        while(true) {
            // Activa el bool del animator para atacar
            animator.SetBool("atack", true);
            // Se espera tantito en lo que termina la animacion
            yield return new WaitForSeconds(0.25f);
            // Desactiva el bool del animator para atacar
            animator.SetBool("atack", false);

            // Dependiendo de la posicion del jugador se asegura que este mirando en la direccion correcta
            if(player.transform.position.x > this.transform.position.x) {
                this.transform.localScale = new Vector3(-1, 1, 1);
                //sprRenderer.flipX = false;
            } else if (player.transform.position.x < this.transform.position.x) {
                this.transform.localScale = new Vector3(1, 1, 1);
                //sprRenderer.flipX = true;
            }

            // Si el jugador ya se alejo lo suficiente lo empieza a seguir
            if(playerDist > attackRadius * 2.5f) {
                state = "FollowPlayer";
                yield break;
            }

            // Espera 0.5 segundos antes de volver a atacar
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update() {
        // Calcula la distancia
        playerDist = Vector2.Distance(transform.position, player.transform.position);
        
        // Switch dependiendo del estado
        switch(state) {
            // En caso de que la courutina no esta corriendo la inicia

            case "GoToObjective":
                if(task == null || !task.Running)
                    task = new Task(GoToObjective());
                break;

            case "FollowPlayer":
                if(task == null || !task.Running)
                    task = new Task(FollowPlayer());
                break;

            case "AttackPlayer":
                if(task == null || !task.Running)
                    task = new Task(AttackPlayer());
                break;
        }

        // Redondea la velociada a entero
        int vel = Mathf.RoundToInt(controller.rb.velocity.x);

        // Se asegura que este mirando en la direccion correcta
        if(vel > 0) {
            this.transform.localScale = new Vector3(-1, 1, 1);
            //sprRenderer.flipX = false;
        } else if (vel < 0) {
            this.transform.localScale = new Vector3(1, 1, 1);
            //sprRenderer.flipX = true;
        }

        // cambia la velocidad del animator
        animator.SetFloat("speed", Mathf.RoundToInt(controller.rb.velocity.magnitude));
    }

    // Este metodo es solo para debugging
    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        //Gizmos.DrawWireSphere(transform.position, attackRadius);

        if (player != null) {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }

        Gizmos.color = followingPlayer ? Color.red : Color.white;

        EntityUtils.DrawRadiusGizmos(transform.position, followRadius);

        Gizmos.color = Color.red;
        EntityUtils.DrawRadiusGizmos(transform.position, attackRadius);
    }
}
