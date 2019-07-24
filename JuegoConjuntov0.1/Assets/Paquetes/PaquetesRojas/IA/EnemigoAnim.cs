using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAnim : MonoBehaviour
{
    public float speed = 1.0f;
    public float followRadius = 5.0f;
    public float attackRadius = 1.0f;

    private EntityController controller;
    public Animator animator;
    public SpriteRenderer sprRenderer;
    public GameObject player;

    private Vector2 targetPos;
    private Node targetNode;
    private bool followingPlayer;

    // Control de la maquina de estado
    private Task task;
    private string state;
    private float playerDist;

    void Awake() {
        if( (controller = this.GetComponent<EntityController>()) == null ) {
            controller = this.gameObject.AddComponent<EntityController>();
        }

        controller.speed = speed;
    }

    void Start() {
        playerDist = float.PositiveInfinity;
        state = "GoToObjective";
        //player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(UpdateTarget());
    }

    IEnumerator FollowPlayer() {
        if(Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(0.3f);
        

        while(true) {
            targetPos = player.transform.position;

            if(this.transform.position.x >= player.transform.position.x)
                targetPos.x += attackRadius;
            else 
                targetPos.x -= attackRadius;

            controller.FollowObject(targetPos);
            followingPlayer = true;

            if(playerDist > followRadius) {
                state = "GoToObjective";
                yield break;
            }

            if(playerDist <= attackRadius * 2.5f) {
                state = "AttackPlayer";
                yield break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator GoToObjective() {
        if(Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(0.3f);
        

        while(true) {
            controller.MoverEnDireccion(new Vector2(-1, 0));
            followingPlayer = false;

            if(playerDist <= followRadius) {
                state = "FollowPlayer";
                yield break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator AttackPlayer() {
        if(Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(0.3f);
        

        while(true) {
            //Debug.Log("Attacking player");
            animator.SetBool("atack", true);
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("atack", false);

            if(player.transform.position.x > this.transform.position.x) {
                this.transform.localScale = new Vector3(1, 1, 1);
                //sprRenderer.flipX = false;
            } else if (player.transform.position.x < this.transform.position.x) {
                this.transform.localScale = new Vector3(-1, 1, 1);
                //sprRenderer.flipX = true;
            }

            if(playerDist > attackRadius * 2.5f) {
                state = "FollowPlayer";
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update() {
        playerDist = Vector2.Distance(transform.position, player.transform.position);
        
        switch(state) {
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

        int vel = Mathf.RoundToInt(controller.rb.velocity.x);

        if(vel > 0) {
            this.transform.localScale = new Vector3(1, 1, 1);
            //sprRenderer.flipX = false;
        } else if (vel < 0) {
            this.transform.localScale = new Vector3(-1, 1, 1);
            //sprRenderer.flipX = true;
        }
        animator.SetFloat("speed", Mathf.RoundToInt(controller.rb.velocity.magnitude));
    }

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
