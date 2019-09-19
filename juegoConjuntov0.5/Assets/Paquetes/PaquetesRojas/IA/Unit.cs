using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    const float minUpdateTime = .2f;
    const float pathUpdateMoveThreshold = 0.1f;

    public Transform target;
    public float speed = 0.5f;
    public float turnSpeed = 1.0f;
    public float turnDistance = 0.5f;

    //private Vector2[] path;
    //private int targetIndex;

    private Path path;

    void Start() {
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector2[] waypoints, bool pathSuccessful) {
        if(pathSuccessful) {
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
        //Vector2 currentWaypoint = path[0];

        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

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
                
                Vector3 diff = path.lookPoints[pathIndex] - pos;
                transform.up= Vector3.Lerp(transform.up, diff, turnSpeed * Time.deltaTime);
                transform.Translate(Vector3.up * Time.deltaTime * speed, Space.Self); // space.self para mover dependiendo de su rotacion
            }

            yield return null;
        }
    }

    public void OnDrawGizmos() {
        if(path != null) {
            path.DrawWithGizmos();
        }
    }
}
