using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
using LitJson;
using System.Collections.Generic;
using TaidouCommon;
using ExitGames.Client.Photon;
using TaidouCommon.Tools;

public class LoginController : ControllerBase{

    //private RoleController roleController;

    public override void Start()
    {
        base.Start();
    }
    public override TaidouCommon.OperationCode OpCode
    {
        get { return TaidouCommon.OperationCode.Login; }
    }
    public void Login(string username, string password)
    {
        User user = new User() { Username = username, Password = password };
        string json = JsonMapper.ToJson(user);
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.User, json);
        ParameterTool.AddParameter(parameters, ParameterCode.DataType, 0,false);
        PhotonEngine.Instance.SendRequest(OperationCode.Login, parameters);

    }
    public void updateUser(User u)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.User, u);
        ParameterTool.AddParameter(parameters, ParameterCode.DataType, 1,false);
        PhotonEngine.Instance.SendRequest(OperationCode.Login, parameters);
        
    }
    public override void OnOperationResponse(OperationResponse response)
    {
        switch (response.ReturnCode)
        {
            case (short)ReturnCode.Success:
                Debug.Log("login success");
                //获得userID
                GameController.user = ParameterTool.GetParameter<User>(response.Parameters, ParameterCode.User);
                Debug.Log("用户名:"+GameController.user.Username+"金币："+GameController.user.coin+GameController.user.money+GameController.user.shortestTime);
                GameController.user.money = 20;
                updateUser(GameController.user);
                GameController.Instance.setUserId(ParameterTool.GetParameter<int>(response.Parameters, ParameterCode.UserId,false));
                //获得user相关数据
                GameController.dataController.getData(1);
                StartMenu._instance.roleController.GetRole();
                StartMenu._instance.hideLoginCanvas();
                StartMenu._instance.showMenu();
                break;
            case (short)ReturnCode.Fail:
                Debug.Log("login fail");
                break;
        }
    }
}
