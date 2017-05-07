using UnityEngine;
using System.Collections;

public class PlayerSmallCollider : MonoBehaviour {

    private PlayerMove playerMove;
    void Start() {
        playerMove = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMove>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.obstacles && playerMove.isSliding) {
            if (GameController.isTeam)
            {
                playerMove.speed -= 1;
                Destroy(other.gameObject);
                //同步
            }
            //GameController.gameState = GameState.End;
            Destroy(other.gameObject);
        }
    }

}
