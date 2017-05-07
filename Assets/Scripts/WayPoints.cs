using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {

    public Transform[] waypoints;


    void OnDrawGizmos() {
        iTween.DrawPath(waypoints);
    }

}
