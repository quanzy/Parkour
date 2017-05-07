using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TaidouCommon.Model;
using System.Collections.Generic;

public class GetOpponent : MonoBehaviour {

    public static GetOpponent _instance;
    private BattleController battleController;
    private Canvas battleCanvas;
    private Canvas searchCanvas;
    private Canvas createRoomCanvas;
    
    void Awake()
    {
        _instance = this;
        battleCanvas = this.GetComponent<Canvas>();
        searchCanvas = GameObject.Find("waitingSearch").transform.Find("searchCanvas").GetComponent<Canvas>();
        createRoomCanvas = transform.Find("createRoomCanvas").GetComponent<Canvas>();
    }
    
    void Start()
    {
        battleController = GameController.battleController;
        battleController.OnWaitingTeam += this.OnWaitingTeam;
        battleController.OnCancelTeam += this.OnCancelTeamSuccess;
        battleController.OnGetTeam += this.OnGetTeam;
        battleController.OnGetRoomNumber += this.OnGetRoomNumber;
    }
    void hideBattleCanvas()
    {
        battleCanvas.gameObject.SetActive(false);
    }
    void showSearchCanvas()
    {
        searchCanvas.gameObject.SetActive(true);
    }
    //团队战斗
    public void OnClickEnterTeam()
    {
        hideBattleCanvas();
        showSearchCanvas();
        battleController.SendTeam(888);//发起组队的请求
    }
    public void OnClickEnterTeamRoom() 
    {
        int roomNumber = int.Parse(transform.Find("searchRoomCanvas/InputField").GetComponent<InputField>().text);
        battleController.SendTeam(roomNumber);
    }
    public void OnClickCreateRoom() 
    {
        createRoomCanvas.gameObject.SetActive(true);
        battleController.GetRoomNumber();
    }
    public void OnClickSearchRoom()
    {
        transform.Find("searchRoomCanvas").GetComponent<Canvas>().gameObject.SetActive(true);
    }
    public void OnClickCancelTeam()
    {
        battleController.CancelTeam();//向服务器端发起取消组队的请求
    }

    //响应服务器端组队成功
    public void OnGetTeam(List<Role> roles,int masterRoleID)
    {
        Debug.Log("组队成功");
        /*foreach (var role1 in roles)
        {
            Debug.Log(role1.Level);
        }*/
        GameController.isTeam = true;//游戏进入双人模式
        GameController.teamRoles = roles;
       
        if (PhotonEngine.Instance.role.ID == masterRoleID)
        {
            GameController.Instance.isMaster = true;//当前客户端是否是主机
        }
         //Application.LoadLevel(1);
        Application.LoadLevelAsync(1);
    }

    public void OnGetRoomNumber(int roomNumber)
    {
        createRoomCanvas.gameObject.SetActive(true);
        createRoomCanvas.transform.Find("roomNumber").GetComponent<Text>().text = roomNumber.ToString();
    }
    //响应服务器端正在等待组队
    public void OnWaitingTeam()
    {
        //AsyncOperation operation = Application.LoadLevelAsync(1);
        print("OnWaitingTeam");
    }
    //响应服务器端取消组队成功
    public void OnCancelTeamSuccess()
    {
        print("OnCancelTeamSuccess");
    }

    void OnDestroy()
    {
        if (battleController != null)
        {
            battleController.OnWaitingTeam -= this.OnWaitingTeam;
            battleController.OnCancelTeam -= this.OnCancelTeamSuccess;
            battleController.OnGetTeam -= this.OnGetTeam;
        }
    }
}
