using UnityEngine;
using System.Collections;

public class TeamColider : MonoBehaviour {


    private int targetLaneIndex;
    private int nowLaneIndex;
    private int targetLaneIndex1;
    private int nowLaneIndex1;
    private float speed;
    private float speed1;
	void Start () 
    {
        
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == Tags.Player1)
        {
            targetLaneIndex = GameController.Instance.player.GetComponent<PlayerMove>().targetLaneIndex;
            nowLaneIndex = GameController.Instance.player.GetComponent<PlayerMove>().nowLaneIndex;
            targetLaneIndex1= GameObject.FindGameObjectWithTag(Tags.Player1).GetComponent<PlayerMove>().targetLaneIndex;
            nowLaneIndex1= GameObject.FindGameObjectWithTag(Tags.Player1).GetComponent<PlayerMove>().nowLaneIndex;
            //player碰player1
            if(targetLaneIndex!=nowLaneIndex&&targetLaneIndex1==nowLaneIndex1)
            {
                GameObject.FindGameObjectWithTag(Tags.Player1).GetComponent<PlayerMove>().speed -= 1;
                //同步
            }
            //player1碰player
            if (targetLaneIndex == nowLaneIndex && targetLaneIndex1 != nowLaneIndex1)
            {
                GameController.Instance.player.GetComponent<PlayerMove>().speed -= 1;
                //同步
            }
            if (targetLaneIndex != nowLaneIndex && targetLaneIndex1 != nowLaneIndex1) 
            {
                /*if (0 < targetLaneIndex&&targetLaneIndex < 2)
                {
                    targetLaneIndex--;
                }*/
                Debug.Log("同时碰撞");
            }
        }
    }
}
