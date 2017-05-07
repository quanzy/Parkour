using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Photon.controller;
using System.Collections.Generic;
using TaidouCommon.Model;


public class StartMenu : MonoBehaviour {
    private Canvas menuCanvas;
    private Canvas loginCanvas;
    private Canvas registerCanvas;
    private Canvas battleCanvas;
    private Canvas gameLevelCanvas;
    //private Canvas searchCanvas;

    private LoginController loginController;
    private RegisterController registerController;
    public  RoleController roleController;
    
    private string username;
    private string password;
    private string repassword;

    public Text tip;
    public Text registerLabel;
    public static StartMenu _instance;

    
    void Awake() 
    {
        _instance = this;
        loginController = this.GetComponent<LoginController>();
        registerController=this.GetComponent<RegisterController>();
        roleController = this.GetComponent<RoleController>();
        menuCanvas = this.transform.Find("menuCanvas").GetComponent<Canvas>();
        loginCanvas = this.transform.Find("loginCanvas").GetComponent<Canvas>();
        registerCanvas = this.transform.Find("registerCanvas").GetComponent<Canvas>();
        battleCanvas = this.transform.Find("battleCanvas").GetComponent<Canvas>();
        tip = this.transform.Find("registerCanvas/IPtip").GetComponent<Text>();
        //searchCanvas = this.transform.Find("searchCanvas").GetComponent<Canvas>();
        roleController.OnAddRole += OnAddRole;
        roleController.OnGetRole += OnGetRole;

    }
    void Start()
    {
        
    }
    //单机试玩无限奔跑模式
    public void onClickRun() 
    {
        hideLoginCanvas();
        GameController.gameState = GameState.Person;
        Application.LoadLevel(1);
        
    }
    public void onClickMenuRegister()
    {
        hideLoginCanvas();
        showRegisterCanvas();
    }

    
    //登陆
    public void onClickLogin()
    {
        username = GameObject.Find("loginUsername").GetComponent<InputField>().text;
        password = GameObject.Find("loginPassword").GetComponent<InputField>().text;
        
        
        loginController.Login(username, password);
        //PhotonEngine.Instance.connectServer();
       
    }
    //注册
    public void onClickRegister() 
    {
        bool isRegister = false;
        username = GameObject.Find("registerUsername").GetComponent<InputField>().text;
        char[] c = username.ToCharArray();
        foreach (char e in c)
        {
            if ((e >= '0' && e <= '9') || (e >= 'A' && e <= 'Z') ||
              (e >= 'a' && e <= 'z') || (e == '_'))
            {
                isRegister = true;
            }

            else
            {
                tip.text = "用户名只能为字母、数字和下划线！";
                break;
            }
        }
        password = GameObject.Find("registerPassword").GetComponent<InputField>().text;
        repassword = GameObject.Find("repassword").GetComponent<InputField>().text;
        if (!password.Equals(repassword))
        {
            isRegister = false;
            tip.text = "两次密码不等，请重新输入！";
        }
        if (isRegister)
        {
            registerController.Register(username, password);
        }
    }
    //菜单选择对战
    public void onClickBattle()
    {
        hideCanvas(menuCanvas);
        showCanvas(battleCanvas);
    }
   
    //进入游戏关卡选择界面
    public void onClickRunServer()
    {
        hideCanvas(menuCanvas);
        showCanvas(gameLevelCanvas);
    }
    //展示游戏菜单
    public void showMenu()
    {
        menuCanvas.gameObject.SetActive(true);
    }
    //展示登录界面
    public void showLoginCanvas()
    {
        loginCanvas.gameObject.SetActive(true);
    }
    //展示注册界面
    public void showRegisterCanvas() 
    {
        registerCanvas.gameObject.SetActive(true);
    }
    //展示双人对战菜单界面
    public void showBattleCanvas()
    {

    }
    //注册完成后隐藏注册界面
    public void hideRegisterCanvas()
    {
        registerCanvas.gameObject.SetActive(false);
    }
    //登录完成后隐藏登录界面
    public void hideLoginCanvas()
    {
        loginCanvas.gameObject.SetActive(false);
    }
    public void hideMenu()
    {
        menuCanvas.gameObject.SetActive(false);
    }
    public void showCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
    }
    public void hideCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
    }
    void OnDestroy() {
        if (roleController != null)
        {
            roleController.OnAddRole -= OnAddRole;
            roleController.OnGetRole -= OnGetRole;
        }
    }
    //后面有方法的注册，获得角色方法
    public void OnGetRole(List<Role> roleList)
    {
        //回调得到角色信息的处理
        if (roleList != null && roleList.Count > 0)
        {
            //进入角色选择界面
            Role role = roleList[0];
            GameController.role = role;
            //role.intSpeed = 8888;
            //roleController.UpdateRole(role);
            //Debug.Log(role.Name + role.Level+role.Level+role.speedAmount+role.timeAmount);
            Debug.Log("获得角色信息成功");
        }
        else
        {
            Debug.Log("获取角色信息失败");
            //没有获取角色
        }
    }
    public void OnAddRole(Role role)
    {

    }
    //选择角色后点击操作
    public void OnSelectRole()
    {
        //异步调用加载场景
        //AsyncOperation operation = Application.LoadLevelAsync(1);
    }
    public void ShowRole(Role role)
    {
        //将角色信息保存，PhotonEngine永远不会注销
        
        
    }
}
