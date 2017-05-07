using UnityEngine;
using System.Collections;
using TaidouCommon;
using System.Collections.Generic;
using TaidouCommon.Tools;
using TaidouCommon.Model;
using LitJson;

public class RoleController : ControllerBase {

    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.Role; }
    }
    //获得角色信息请求
    public void GetRole()
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.SubCode,SubCode.GetRole);
        PhotonEngine.Instance.SendRequest(OperationCode.Role, parameters);
    }
    //添加角色请求
    public void AddRole(Role role)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.SubCode, SubCode.AddRole);
        //parameters.Add((byte)ParameterCode.Role, JsonMapper.ToJson(role));
        ParameterTool.AddParameter<Role>(parameters,ParameterCode.Role, role, true);
        PhotonEngine.Instance.SendRequest(OpCode, parameters);
    }
    public void SelectRole(Role role)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.Role,JsonMapper.ToJson(role));
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SelectRole, parameters);
    }
    public void UpdateRole(Role role)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.Role, JsonMapper.ToJson(role));
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateRole, parameters);
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response)
    {
        SubCode subcode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode,false);
        switch (subcode)
        {
            case SubCode.GetRole:
                List<Role> list = ParameterTool.GetParameter<List<Role>>(response.Parameters, ParameterCode.RoleList);
                OnGetRole(list);
                Debug.Log("收到role");
                //将角色信息存储在PhotonEngine中
                PhotonEngine.Instance.role = list[0];
                break;
            case SubCode.AddRole:
                Role role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                Debug.Log("添加角色成功！");
                OnAddRole(role);
                break;
            case SubCode.SelectRole:
                if (OnSelectRole != null)
                {
                    OnSelectRole();
                }
                break;
        }
    }
    public event OnGetRoleEvent OnGetRole;
    public event OnAddRoleEvnet OnAddRole;
    public event OnSelectRoleEvent OnSelectRole;
}
