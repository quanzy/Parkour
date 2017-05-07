using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour {

    public float fixedY = 0;

    public virtual void InitSelf(Vector3 position,Transform parent=null) {
        position = new Vector3(position.x, fixedY+position.y, position.z);
        this.transform.parent = parent;

        int count = Random.Range(1, 4);
        int xOffset = Random.Range(0, 3);// 0 1 2
        switch (count) {
            case 1:
                this.transform.position = new Vector3(position.x + GameController.xOffsets[xOffset], position.y, position.z);
                break;
            case 2:
                bool isfirst = true;
                for (int i = 0; i < 3; i++) {
                    if (i == xOffset) continue;
                    if (isfirst) {
                        isfirst = false;
                        this.transform.position = new Vector3(position.x + GameController.xOffsets[i], position.y, position.z);
                    } else {
                        Vector3 tempPosition = new Vector3(position.x + GameController.xOffsets[i], position.y, position.z);
                        GameObject go = GameObject.Instantiate(this.gameObject, tempPosition, Quaternion.identity) as GameObject;
                        go.transform.parent = parent;
                    }
                }
                break;
            case 3:
                this.transform.position = new Vector3(position.x + GameController.xOffsets[0], position.y, position.z);
                Vector3 tempPosition1 = new Vector3(position.x + GameController.xOffsets[1], position.y, position.z);
                GameObject go1 = GameObject.Instantiate(this.gameObject, tempPosition1, Quaternion.identity) as GameObject;
                go1.transform.parent = parent;
                Vector3 tempPosition2 = new Vector3(position.x + GameController.xOffsets[2], position.y, position.z);
                GameObject go2 = GameObject.Instantiate(this.gameObject, tempPosition2, Quaternion.identity) as GameObject;
                go2.transform.parent = parent;
                break;
        }
    }

}
