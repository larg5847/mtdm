using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour {

    const float minUpdateTime = .2f;
    const float pathUpdateMoveThreshold = 0.3f;

    public float speed = 0.5f;
    public float turnSpeed = 1.0f;
    public float turnDistance = 0.5f;

    public Transform target;       // Posicion objetivo para calcular el camino
    private Rigidbody2D rb;         // Referencia al rigidbody para el control del movimiento
    private Path path;              // Referencia al camino calculado

    Vector2[] waypointsF;

    int targetIndex;

    void Awake() {
        // Se obtiene el path
        if((rb = this.GetComponent<Rigidbody2D>()) == null) {
            rb = this.gameObject.AddComponent<Rigidbody2D>();
        }
    }

    void Start() {
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector2[] waypoints, bool pathSuccessful) {
        if(pathSuccessful) {
            rb.velocity = Vector2.zero;
            waypointsF = waypoints;
            path = new Path(waypoints, transform.position, turnDistance);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath() {
        if(Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(0.3f);
        
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

        float squareMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector2 targetPosOld = target.position;

        while(true) {
            yield return new WaitForSeconds(minUpdateTime);
            if(((Vector2)target.position - targetPosOld).sqrMagnitude > squareMoveThreshold) {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath() {
        if(waypointsF.Length > 0) {
            Vector3 currentWaypoint = waypointsF[0];

            while(true) {
                if(transform.position == currentWaypoint) {
                    targetIndex ++;
                    if(targetIndex >= waypointsF.Length) {
                        yield break;
                    }
                    currentWaypoint = waypointsF[targetIndex];
                }

                //Vector3 pos = transform.position;
                //rb.velocity = (pos - currentWaypoint).normalized * speed;
                Vector2 direction = target.position - transform.position;
                rb.velocity = direction.normalized * speed;
                //rb.AddRelativeForce(direction.normalized * speed, ForceMode2D.Force);
                yield return null;
                
            }
        }

        /* 
        bool followingPath = true;
        int pathIndex = 0;

        while(followingPath) {
            Vector2 pos = transform.position;
            while(path.turnBoundaries[pathIndex].HasCrossedLine(pos)) {
                if(pathIndex == path.finishLineIndex) {
                    followingPath = false;
                    break;
                }else
                    pathIndex ++;
            }

            if(followingPath) {
                Vector2 diff = pos - path.lookPoints[pathIndex];
                
                rb.velocity = diff.normalized * speed;
                
            }

            yield return null;
        }
        */
    }
}
