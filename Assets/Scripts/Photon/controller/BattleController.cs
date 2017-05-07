using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;

public enum SyncType
{
    ChangeWay,
    Speed,
    Direction,
    ReturnSpeed,
    Collider,
}

public class BattleController : ControllerBase
{
    public override OperationCode OpCode
    {
        get { return OperationCode.Battle; }
    }

    /*public void SendGameState(GameStateModel model)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.GameStateModel, model);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SendGameState, parameters);
    }

    */
    
    //同步游戏元素
    public void SyncElements<T>(T elements,SyncType type)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.RoleID, PhotonEngine.Instance.role.ID, false);
        ParameterTool.AddParameter(parameters, ParameterCode.SyncElements, elements, false);
        ParameterTool.AddParameter(parameters, ParameterCode.SyncTypes, type, false);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SyncElements, parameters);
    }
    //发起同步障碍物
    public void SyncObstacles(Dictionary<Vector3,int> dic)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.RoleID, PhotonEngine.Instance.role.ID, false);
        ParameterTool.AddParameter(parameters, ParameterCode.Obstacles, dic, false);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SyncObstacles, parameters);
    }
    //发起同步gameObject
    public void SyncGameObject(GameObject gameObject)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.RoleID, PhotonEngine.Instance.role.ID, false);
        ParameterTool.AddParameter(parameters, ParameterCode.GameObject, gameObject);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SyncGameObject, parameters);
    }
    //发起组队的请求
    public void SendTeam(int roomNumber)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.RoomNumber, roomNumber, false);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SendTeam, parameters);
    }

    public void CancelTeam()
    {
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.CancelTeam, new Dictionary<byte, object>());
    }
    public void GetRoomNumber()
    {
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.GetRoomNumber, new Dictionary<byte, object>());
    }
    public override void OnEvent(EventData eventData)
    {
        SubCode subCode = ParameterTool.GetSubcode(eventData.Parameters);
        switch (subCode)
        {
            case SubCode.GetTeam:
                List<Role> roles = ParameterTool.GetParameter<List<Role>>(eventData.Parameters, ParameterCode.RoleList);
                int masterRoleID = ParameterTool.GetParameter<int>(eventData.Parameters, ParameterCode.MasterRoleID,false);
                
                if (OnGetTeam != null)
                {
                    Debug.Log("获得的组队角色个数："+roles.Count);
                    OnGetTeam(roles, masterRoleID);
                }
                break;
            case SubCode.SyncElements:
                int roleIDElements = ParameterTool.GetParameter<int>(eventData.Parameters, ParameterCode.RoleID, false);
                if (OnSyncElements != null) 
                {
                    Debug.Log("收到同步！");
                    OnSyncElements(roleIDElements, eventData.Parameters);
                }
                break;
            case SubCode.SyncObstacles:
                
                break;
            case SubCode.SyncGameObject:
                GameObject gameObject = ParameterTool.GetParameter<GameObject>(eventData.Parameters, ParameterCode.GameObject);
                Destroy(gameObject);
                break;
            case SubCode.SendGameState:
                GameStateModel model3 = ParameterTool.GetParameter<GameStateModel>(eventData.Parameters,
                    ParameterCode.GameStateModel);
                if (OnGameStateChange != null)
                {
                    OnGameStateChange(model3);
                }
                break;
        }
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        SubCode subcode = ParameterTool.GetSubcode(response.Parameters);
        switch (subcode)
        {
            case SubCode.SendTeam:
                if (response.ReturnCode == (int)ReturnCode.GetTeam)
                {
                    List<Role> roles = ParameterTool.GetParameter<List<Role>>(response.Parameters,
                        ParameterCode.RoleList);
                    int masterRoleID = ParameterTool.GetParameter<int>(response.Parameters, ParameterCode.MasterRoleID,false);
                    
                    if (OnGetTeam != null)
                    {
                         Debug.Log("获得的组队角色个数："+roles.Count);
                        OnGetTeam(roles, masterRoleID);
                    }
                }
                else if (response.ReturnCode == (int)ReturnCode.WaitingTeam)
                {
                    if (OnWaitingTeam != null)
                    {
                        OnWaitingTeam();
                    }
                }
                break;
            case SubCode.CancelTeam:
                if (OnCancelTeam != null)
                {
                    OnCancelTeam();
                }
                break;
            case SubCode.GetRoomNumber:
                int roomNumber = ParameterTool.GetParameter<int>(response.Parameters, ParameterCode.RoomNumber, false);
                if (OnGetRoomNumber != null)
                {
                    OnGetRoomNumber(roomNumber);
                }
                break;
        }
    }

    public event OnGetTeamEvent OnGetTeam;
    public event OnGetRoomNumber OnGetRoomNumber;
    public event OnWaitingTeamEvent OnWaitingTeam;
    public event OnCancelTeamEvent OnCancelTeam;
    public event OnSyncPositionAndRotationEvent OnSyncPositionAndRotation;
    public event OnSyncMoveAnimationEvent OnSyncMoveAnimation;
    public event OnSyncPlayerAnimationEvent OnSyncPlayerAnimation;
    public event OnGameStateChangeEvent OnGameStateChange;
    public event OnSyncDirection OnSyncDirection;
    public event OnSyncBool OnSyncBool;
    public event OnSyncSpeed OnSyncSpeed;
    public event OnSyncElements OnSyncElements;

}
