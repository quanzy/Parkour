using UnityEngine;
using System.Collections;

public class ObstaclesBarbWireMultiLane : Obstacles {

    public override void InitSelf(Vector3 position,Transform parent  = null) {
        transform.position = new Vector3(position.x, fixedY + position.y, position.z);
        this.transform.parent = parent;
    }
}
