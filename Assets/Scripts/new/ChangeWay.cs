using UnityEngine;
using System.Collections;

public class ChangeWay : MonoBehaviour 
{

	private PlayerMove playerMove;
    private PlayerMove1 playerMove1;

    void Start() 
    {
        playerMove = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMove>();
        playerMove1 = GameObject.FindGameObjectWithTag(Tags.Player1).GetComponent<PlayerMove1>();
    }

    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("撞到");
        if (other.tag == Tags.player) 
        {
            if (playerMove.targetLaneIndex == 1)
            {
                playerMove.targetLaneIndex = 2;
            }
            playerMove.twoWay = true;
            
        }
        if (other.tag == Tags.Player1)
        {
            if (playerMove1.targetLaneIndex == 1)
            {
                playerMove1.targetLaneIndex = 2;
                //GameController.battleController.SyncElements<TouchDir>(TouchDir.Right, SyncType.Direction);
            }
            playerMove1.twoWay = true;
            //GameController.battleController.SyncElements<bool>(playerMove.twoWay, SyncType.ChangeWay); 
        }
    }
}
