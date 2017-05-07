using UnityEngine;
using System.Collections;
using TaidouCommon.Model;

public class Initialization : MonoBehaviour 
{
    private int[] xOffsets = new int[2] { GameController.xOffsets[0], GameController.xOffsets[2] };
    private int[] index = new int[2] { 0, 2 };
    void Awake()
    {
        
        //在场景中加载游戏物体
        GameObject playerPrefab;
        GameObject go;
        if (GameController.isTeam)
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.Log("初始化时角色个数：" + GameController.teamRoles.Count);
                Role role = GameController.teamRoles[i];
                if (role.ID == PhotonEngine.Instance.role.ID)
                {
                    Debug.Log("创建成功");
                    //当前创建的角色是当前客户端控制的
                    playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
                    Vector3 pos = new Vector3(playerPrefab.transform.position.x + xOffsets[i], playerPrefab.transform.position.y, playerPrefab.transform.position.z);
                    go = GameObject.Instantiate(playerPrefab, pos, Quaternion.identity) as GameObject;
                    go.GetComponent<PlayerMove>().targetLaneIndex = index[i];
                    go.GetComponent<PlayerMove>().nowLaneIndex = index[i];
                    go.GetComponent<Player>().roleID = role.ID;
                    GameController.Instance.AddPlayer(role.ID, go);
                    GameController.Instance.player = go;
                    //GameController.Instance.isMaster = false;
                    go.GetComponent<PlayerMove>().isCanControl = true;
                    
                }
                else
                {
                    //这个角色是其他客户端的  修改移动为不可控
                    playerPrefab = Resources.Load("Prefabs/Player1") as GameObject;
                    Vector3 pos = new Vector3(playerPrefab.transform.position.x + xOffsets[i], playerPrefab.transform.position.y, playerPrefab.transform.position.z);
                    go = GameObject.Instantiate(playerPrefab, pos, Quaternion.identity) as GameObject;
                    go.GetComponent<Player>().roleID = role.ID;
                    go.GetComponent<PlayerMove1>().targetLaneIndex = index[i];
                    go.GetComponent<PlayerMove1>().nowLaneIndex = index[i];
                    GameController.Instance.AddPlayer(role.ID, go);
                    go.GetComponent<PlayerMove1>().isCanControl = false;
                    
                }
            }
        }
        if (GameController.gameState == GameState.Person)
        {
            
            playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
            go = GameObject.Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity) as GameObject;
            GameController.Instance.player = go;
            go.GetComponent<PlayerMove>().isCanControl = true;
            
        }
        /*playerPrefab = Resources.Load("Prefabs/forest_1") as GameObject;
        go = GameObject.Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity) as GameObject;
        GameController.Instance.forest1 = go;
        playerPrefab = Resources.Load("Prefabs/forest_2") as GameObject;
        go = GameObject.Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity) as GameObject;
        GameController.Instance.forest2 = go; */
    }
}
