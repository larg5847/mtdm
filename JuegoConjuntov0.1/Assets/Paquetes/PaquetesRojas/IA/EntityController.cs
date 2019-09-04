using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour {

    const float minUpdateTime = .2f;                    // Cada cuanto tiempo se recalcula
    const float pathUpdateMoveThreshold = 0.3f;         // Cantidad de mov para recalcular el camino
    const float minWaypointOffset = 0.3f;               // Cantidad minima de distancia del Entity al waypoint del path

    [HideInInspector]
    public float speed = 0.0f;
    private float turnDistance = 0.5f;

    private bool calculatePath;         // Si se puede calcular el camino
    private Vector2 target;             // Posicion objetivo para calcular el camino
    [HideInInspector]
    public Rigidbody2D rb;             // Referencia al rigidbody para el control del movimiento
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

    // Metodo que permite que el Entity siga a un objeto
    // Args:
    //      transform: el transform del objeto a seguir
    public void FollowObject(Vector2 position) {
        if(!calculatePath) calculatePath = true;

        target = position;
    }

    // Metodo que permite que el Entity se mueva en una direaccion
    // Args:
    //      dir: vector de la direccion que tomara el entity. ejem: Vector2.right
    public void MoverEnDireccion(Vector2 dir) {
        Vector2 newDir = dir * pathUpdateMoveThreshold * 10;
        newDir += (Vector2)transform.position;

        if(!calculatePath) calculatePath = true;

        target = newDir;
    }

    // Metodo que permite parar por completo el control de movimiento del Entity
    public void StopEntity() {
        calculatePath = false;
        rb.velocity = Vector2.zero;
    }

    // Metodo que se llama cuando se encontro un path que pueda seguir el entity
    private void OnPathFound(Vector2[] waypoints, bool pathSuccessful) {
        // Se verifica primero que el camino sea valida
        if(pathSuccessful) {
            // Se reinicia indices, velocidad, path y waypoints
            targetIndex = 0;
            rb.velocity = Vector2.zero;
            waypointsF = waypoints;
            path = new Path(waypoints, transform.position, turnDistance);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    // Courutina que esta constantemente viendo si hay que recalcular el path
    IEnumerator UpdatePath() {
        // Inicialmente espera unos segundos a que carge la escena
        if(Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(0.3f);

        // Si el entity puede calcular
        if(calculatePath)
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);

        // Obtiene la magnitud a partir del cual se vuele a recalcular el camino
        float squareMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector2 targetPosOld = target;

        while(true) {
            yield return new WaitForSeconds(minUpdateTime);
            // si se pude mover y el target se movio la magnitud anteriormente calculada, se vuelve a calcular el path
            if(calculatePath && ((Vector2)target - targetPosOld).sqrMagnitude > squareMoveThreshold) {
                // Realiza peticion al manger con callback a OnPathFound
                PathRequestManager.RequestPath(transform.position, target, OnPathFound);
                targetPosOld = target;
            }
        }
    }

    // Routina que sigue el camino
    IEnumerator FollowPath() {
        // Mientras existan waypoints
        if(waypointsF.Length > 0) {
            Vector3 currentWaypoint = waypointsF[0];

            while(true && calculatePath) {
                // Si las distancia entre el Entity y el waypoint es la minima para cambiar al siguiente waypoint
                if(Vector2.Distance(transform.position, currentWaypoint) <= 0.3f) {
                    targetIndex ++;
                    // Checa si se llego al final del path
                    if(targetIndex >= waypointsF.Length) {
                        rb.velocity = Vector2.zero;
                        yield break;            // se sale del ciclo
                    }
                    currentWaypoint = waypointsF[targetIndex];
                }

                // Mueve el entity mediante el rigidbody
                Vector2 direction = currentWaypoint - transform.position;
                rb.velocity = direction.normalized * speed;

                yield return null;      // repite ciclo

            }
        }
    }

    void OnDrawGizmos() {
        if (path != null) {
            // Dibuja los puntos del path
            path.DrawWithGizmos();

            if(targetIndex < waypointsF.Length) {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(waypointsF[targetIndex], new Vector2(0.08f, 0.08f));
            }
        }
    }
}
