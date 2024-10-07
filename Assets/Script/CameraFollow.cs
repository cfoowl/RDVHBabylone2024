using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;
    float z_offset;
    int currentWaypointIndex = -1;
    bool isWaiting = true;
    Vector2[] waypoints = {
        new Vector2(1176, 893),
        new Vector2(1176, 893),
        new Vector2(1176, 860),
        new Vector2(1080, 780),
        new Vector2(975, 780),
        new Vector2(780, 710),
        new Vector2(620, 780),
        new Vector2(560, 760),
        new Vector2(400, 760),
        new Vector2(350, 700),
    };
    void Start()
    {
        z_offset = -150;
        speed = 0;

        Vector2 firstWaypoint = waypoints[0];
        Vector3 startingPos = new Vector3(firstWaypoint.x, firstWaypoint.y, z_offset);
        transform.position = startingPos;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting) {
            MoveTowardsWaypoint();
        }
    }

    private void MoveTowardsWaypoint() {
        if (currentWaypointIndex >= waypoints.Length) return;
        Vector3 targetWaypoint =  new Vector3(waypoints[currentWaypointIndex].x, waypoints[currentWaypointIndex].y, z_offset);

        if (speed < 50) {
            speed += 0.5f;
        }
        
        Vector3 direction = targetWaypoint - transform.position;
        float distance = direction.magnitude;
        Vector3 moveDirection = direction.normalized;

        if (distance < 5f) {
            StartWaitingAtWaypoint();
        }
        else {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    private void StartWaitingAtWaypoint()
    {
        isWaiting = true;

    }

    public void ContinueMoving()
    {
        if (isWaiting)
        {
            isWaiting = false;
            currentWaypointIndex++;
            speed = 0;

        }
    }

}
