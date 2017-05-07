using UnityEngine;
using System.Collections;

public class tlWayPoints : MonoBehaviour
{

    private Transform player;
    private Transform player1;
    private int targetWayPointIndex = 0;
    private int targetWayPointIndex1 = 0;
    public Transform[] tlWaypoints;
    private tlWayPoints waypoints;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        player1 = GameObject.FindGameObjectWithTag(Tags.Player1).transform;
        waypoints = GameObject.FindGameObjectWithTag(Tags.tlWayPoints).GetComponent<tlWayPoints>();
        targetWayPointIndex = 40;
        targetWayPointIndex1 = 40;
    }
    void OnDrawGizmos()
    {
        iTween.DrawPath(tlWaypoints);
    }

    public Vector3 GetNextWayPoint()
    {
        while (true)
        {

            if (waypoints.tlWaypoints[targetWayPointIndex].position.z - player.position.z < 0.3f)
            {
                targetWayPointIndex--;
                if (targetWayPointIndex < 0)
                {
                    targetWayPointIndex = 0;
                    if (PlayerBigCollider.ontop == true)
                    {
                        return waypoints.tlWaypoints[0].position + new Vector3(0, 21, 0);
                    }
                    else
                    {
                        return waypoints.tlWaypoints[0].position;
                    }
                }
            }
            else
            {
                if (PlayerBigCollider.ontop == true)
                {
                    return waypoints.tlWaypoints[targetWayPointIndex].position + new Vector3(0, 21, 0);
                }
                else
                {
                    return waypoints.tlWaypoints[targetWayPointIndex].position;
                }
            }
        }
    }
    public Vector3 GetNextWayPoint1()
    {
        while (true)
        {

            if (waypoints.tlWaypoints[targetWayPointIndex1].position.z - player1.position.z < 0.3f)
            {
                targetWayPointIndex1--;
                if (targetWayPointIndex1 < 0)
                {
                    targetWayPointIndex1 = 0;
                    if (PlayerBigCollider.ontop == true)
                    {
                        return waypoints.tlWaypoints[0].position + new Vector3(0, 21, 0);
                    }
                    else
                    {
                        return waypoints.tlWaypoints[0].position;
                    }
                }
            }
            else
            {
                if (PlayerBigCollider.ontop == true)
                {
                    return waypoints.tlWaypoints[targetWayPointIndex1].position + new Vector3(0, 21, 0);
                }
                else
                {
                    return waypoints.tlWaypoints[targetWayPointIndex1].position;
                }
            }
        }
    }
}
