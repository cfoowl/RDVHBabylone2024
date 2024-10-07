using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBoat : MonoBehaviour
{
    public GameObject[] waypoints;
    float speed;
    bool isWaiting = true;
    int currentWaypointIndex = -1;
    int currentTrajectoryNodeIndex = 0;
    List<Vector3> trajectory = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        updateTrajectory(0);
        Vector3 startingPos = trajectory[0];
        gameObject.GetComponent<RectTransform> ().position = startingPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWaiting) {
            MoveTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint(){
        if (currentWaypointIndex >= waypoints.Length) return;
        if (speed < 50) {
            speed += 0.5f;
        }

        Vector3 direction = trajectory[currentTrajectoryNodeIndex] - transform.position;
        float distance = direction.magnitude;
        Vector3 moveDirection = direction.normalized;
        if(distance < 2f) {
            if (currentTrajectoryNodeIndex < trajectory.Count -1) {
                currentTrajectoryNodeIndex++;
            } else {
                StartWaitingAtWaypoint();
            }
        } else {
            gameObject.GetComponent<RectTransform> ().position += moveDirection * speed * Time.deltaTime;
        }
    } 
       private void StartWaitingAtWaypoint()
    {
        isWaiting = true;

        Debug.Log("Bateau arrêté au waypoint " + currentWaypointIndex);
    }
    public void ContinueMoving() {
        if (isWaiting) {
            isWaiting = false;
            currentWaypointIndex++;
            currentTrajectoryNodeIndex = 0;
            speed = 0;
            updateTrajectory(currentWaypointIndex);

            Debug.Log("Reprise du mouvement du bateau vers le waypoint " + currentWaypointIndex);
        }
    }
    void updateTrajectory(int index) {
        trajectory.Clear();
        foreach (Transform child in waypoints[index].transform) {
            trajectory.Add(child.GetComponent<RectTransform>().position);
        }
    }
}
