<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour {

    const float minUpdateTime = .2f;                    // Cada cuanto tiempo se recalcula
    const float pathUpdateMoveThreshold = 0.3f;         // Cantidad de mov para recalcular el camino

    [HideInInspector]
    public float speed = 0.0f;
    private float turnDistance = 0.5f;

    private bool calculatePath;         // Si se puede calcular el camino
    public Vector2 target;             // Posicion objetivo para calcular el camino
    private Rigidbody2D rb;             // Referencia al rigidbody para el control del movimiento
    private Path path;                  // Referencia al camino calculado
    private Vector2[] waypointsF;       // Arreglo con los puntos del camino encontrado
    private int targetIndex;            // Indice del nodo objetivo al cual moverse


    void Awake() {
        // Se asegura de los componentes basicos 
        if((rb = this.GetComponent<Rigidbody2D>()) == null) {
            rb = this.gameObject.AddComponent<Rigidbody2D>();
        }
    }

    void Start() {
        calculatePath = false;
        StartCoroutine(UpdatePath());
    }

    public void FollowObject(Transform transform) {
        if(!calculatePath) calculatePath = true;

        target = transform.position;
    }

    public void MoverEnDireccion(Vector2 dir) {
        Vector2 newDir = dir * pathUpdateMoveThreshold * 10;
        newDir += (Vector2)transform.position;

        if(!calculatePath) calculatePath = true;

        target = newDir;
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
        
        if(calculatePath)
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);

        float squareMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector2 targetPosOld = target;

        while(true) {
            yield return new WaitForSeconds(minUpdateTime);
            if(calculatePath && ((Vector2)target - targetPosOld).sqrMagnitude > squareMoveThreshold) {
                PathRequestManager.RequestPath(transform.position, target, OnPathFound);
                targetPosOld = target;
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
                Vector2 direction = currentWaypoint - transform.position;
                rb.velocity = direction.normalized * speed;
                //rb.AddRelativeForce(direction.normalized * speed, ForceMode2D.Force);
                yield return null;
                
            }
        }
    }

    void OnDrawGizmos() {
        if (path != null) {
            path.DrawWithGizmos();
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour {

    const float minUpdateTime = .2f;                    // Cada cuanto tiempo se recalcula
    const float pathUpdateMoveThreshold = 0.3f;         // Cantidad de mov para recalcular el camino

    [HideInInspector]
    public float speed = 0.0f;
    private float turnDistance = 0.5f;

    private bool calculatePath;         // Si se puede calcular el camino
    public Vector2 target;             // Posicion objetivo para calcular el camino
    private Rigidbody2D rb;             // Referencia al rigidbody para el control del movimiento
    private Path path;                  // Referencia al camino calculado
    private Vector2[] waypointsF;       // Arreglo con los puntos del camino encontrado
    private int targetIndex;            // Indice del nodo objetivo al cual moverse


    void Awake() {
        // Se asegura de los componentes basicos 
        if((rb = this.GetComponent<Rigidbody2D>()) == null) {
            rb = this.gameObject.AddComponent<Rigidbody2D>();
        }
    }

    void Start() {
        calculatePath = false;
        StartCoroutine(UpdatePath());
    }

    public void FollowObject(Transform transform) {
        if(!calculatePath) calculatePath = true;

        target = transform.position;
    }

    public void MoverEnDireccion(Vector2 dir) {
        Vector2 newDir = dir * pathUpdateMoveThreshold * 10;
        newDir += (Vector2)transform.position;

        if(!calculatePath) calculatePath = true;

        target = newDir;
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
        
        if(calculatePath)
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);

        float squareMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector2 targetPosOld = target;

        while(true) {
            yield return new WaitForSeconds(minUpdateTime);
            if(calculatePath && ((Vector2)target - targetPosOld).sqrMagnitude > squareMoveThreshold) {
                PathRequestManager.RequestPath(transform.position, target, OnPathFound);
                targetPosOld = target;
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
                Vector2 direction = currentWaypoint - transform.position;
                rb.velocity = direction.normalized * speed;
                //rb.AddRelativeForce(direction.normalized * speed, ForceMode2D.Force);
                yield return null;
                
            }
        }
    }

    void OnDrawGizmos() {
        if (path != null) {
            path.DrawWithGizmos();
        }
    }
}
>>>>>>> Stashed changes
