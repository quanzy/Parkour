using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaidouCommon;
using TaidouCommon.Model;
using UnityEngine;

namespace Assets.Scripts.Photon.controller
{
    public class RegisterController:ControllerBase
    {
        public override TaidouCommon.OperationCode OpCode
        {
            get { return OperationCode.Register; }
        }
        public void Register(string username, string password)
        {
            User user = new User(){Username=username,Password=password};
            string json = JsonMapper.ToJson(user);
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            parameters.Add((byte)ParameterCode.User, json);
            PhotonEngine.Instance.SendRequest(OperationCode.Register,parameters);
        }
        public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response)
        {
            switch (response.ReturnCode)
            {
                case (short)ReturnCode.Fail:
                    Debug.Log("register failed");
                    break;
                case (short)ReturnCode.Success:
                    /*Role role = new Role();
                    role.Name = "quan";
                    role.intSpeed = 40;
                    role.speedAmount = 2;
                    role.timeAmount = 2;
                    role.Level = 2;
                    StartMenu._instance.roleController.AddRole(role);*/
                    StartMenu._instance.hideRegisterCanvas();
                    StartMenu._instance.showLoginCanvas();
                    Debug.Log("register successed");
                    break;
            }
        }
    }
}
