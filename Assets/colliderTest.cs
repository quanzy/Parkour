using UnityEngine;
using System.Collections;

public class colliderTest : MonoBehaviour {

    private PlayerMove playerMove;
    private BattleController bc;
    void Start()
    {
        playerMove = GameController.Instance.player.GetComponent<PlayerMove>();
        bc = GameController.battleController;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player) 
        {
            playerMove.speed = 0;
            Debug.Log("发起同步速度请求");
            bc.SyncElements<float>(playerMove.speed, SyncType.Speed);
            
        }
    }
}
