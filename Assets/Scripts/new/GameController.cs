using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using TaidouCommon;
using Assets.Scripts.Photon.controller;

public enum GameState {
    Menu,
    Playing,
    Team,
    Person,
    End
}
//游戏进行状态的自定义及开场结束UI
public class GameController : MonoBehaviour {


    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }
    //三个跑道x坐标位置差值
    public static int[] xOffsets = new int[3]{ -10,0,10 };
    public static GameState gameState = GameState.Menu;
    public static bool isStartGame = false;
    public static bool isTeam=false;

    //存储当前的player
    private Dictionary<int, GameObject> playerDict = new Dictionary<int, GameObject>();
    public static BattleController battleController;
    public static DataController dataController;
    public bool isMaster = false;
    public static List<Role> teamRoles = new List<Role>();

    
    public GameObject player;
        
    private bool returnSpeed=false;
    private bool Collider = false;
    private float time = 2;
    private int userId;
    private level1Data level1Data;
    private level2Data level2Data;
    public static User user;
    public static Role role;
    //获得同步的方向
    
    void Awake() 
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            battleController = GetComponent<BattleController>();
            dataController = GetComponent<DataController>();
            battleController.OnSyncElements += this.OnSyncElements;
            //battleController.OnGameStateChange += OnGameStateChange;

            isStartGame = true;
        }
        else
        {
            Destroy(gameObject);
        }

    }
   
    void Update() {
        /*if (GameController.gameState == GameState.Menu) {
            if (isStartGame) {
                if (Input.GetMouseButtonDown(0))
                {
                    gameState = GameState.Playing;
                    startUI.SetActive(false);
                }
            }
        }*/
        if (GameController.gameState == GameState.End) {
            //gameoverUI.SetActive(true);
        }
        if (Collider)
        {
            
            time = time - Time.deltaTime;
            if (time <= 0)
            {
                player.GetComponent<PlayerMove>().speed += 10;
                //battleController.SyncElements<float>(player.GetComponent<PlayerMove>().speed, SyncType.Speed);
                Debug.Log("速度同步增加回来40");
                Collider = false;
                time = 2;
            }
        }
        
    }

    public void setUserId(int id)
    {
        userId = id;
    }
    public int getUserId()
    {
        return userId;
    }
    public void setLevel1Data(level1Data bd)
    {
        level1Data = bd;
    }
    public level1Data getLevel1Data()
    {
        return level1Data;
    }
    public void setLevel2Data(level2Data bd)
    {
        level2Data = bd;
    }
    public level2Data getLevel2Data()
    {
        return level2Data;
    }
    public void AddPlayer(int roleID,GameObject playerGameObject )
    {
        playerDict.Add(roleID,playerGameObject);
    }
 
    public void OnSyncElements(int roleID, Dictionary<byte, object> dic) 
    {
        GameObject go = null;
        bool isHave = playerDict.TryGetValue(roleID, out go);
        if (isHave)
        {
            SyncType type = ParameterTool.GetParameter<SyncType>(dic, ParameterCode.SyncTypes, false);
            switch (type)
            {
                case SyncType.ChangeWay:
                    Debug.Log("同步换道");
                    bool istrue = ParameterTool.GetParameter<bool>(dic, ParameterCode.SyncElements, false);
                    go.GetComponent<PlayerMove>().twoWay = istrue;
                    break;
                case SyncType.Speed:
                    float speed=ParameterTool.GetParameter<float>(dic,ParameterCode.SyncElements,false);
                    if (roleID == role.ID) 
                    {
                        GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMove>().speed = speed;
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag(Tags.Player1).GetComponent<PlayerMove1>().speed = speed;
                    }
                    
                    //go.GetComponent<PlayerMove>().speed = speed;
                    break;
                case SyncType.Direction:
                    TouchDir dir = ParameterTool.GetParameter<TouchDir>(dic,ParameterCode.SyncElements,false);
                    //go.GetComponent<PlayerMove>().SetTouchDir(dir);
                    GameObject.FindGameObjectWithTag(Tags.Player1).GetComponent<PlayerMove1>().SetTouchDir(dir);
                    break;
                case SyncType.Collider:
                    Debug.Log("收到同步碰撞");
                    Collider = ParameterTool.GetParameter<bool>(dic, ParameterCode.SyncElements, false);
                    if (Collider)
                    {
                        Debug.Log("速度减少40");
                        player.GetComponent<PlayerMove>().speed -= 10;
                    }
                    break;
            }
        }
        else 
        {
            Debug.LogWarning("未找到对应的角色游戏物体进行更新移动动画状态");
        }
    }
}
