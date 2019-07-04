using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBaisco : MonoBehaviour
{
    public float speed = 1.0f;
    public float attackRadius = 5.0f;

    private EntityController controller;
    public GameObject player;
    private bool followingPlayer;

    void Awake() {
        if( (controller = this.GetComponent<EntityController>()) == null ) {
            controller = this.gameObject.AddComponent<EntityController>();
        }

        controller.speed = speed;
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if(dist <= attackRadius) {
            controller.FollowObject(player.transform);
            followingPlayer = true;
        } else {
            controller.MoverEnDireccion(new Vector2(-1, 0));
            followingPlayer = false;
        }
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

        float theta = 0;
        float x = attackRadius * Mathf.Cos(theta);
        float y = attackRadius * Mathf.Sin(theta);
        Vector3 pos = transform.position + new Vector3(x, y);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = attackRadius * Mathf.Cos(theta);
            y = attackRadius * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, y);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos, lastPos);
    }
}
