using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using UnityEngine;

//1:战斗数据
//2:关卡数据
namespace Assets.Scripts.Photon.controller
{
    public class DataController : ControllerBase
    {
        public override OperationCode OpCode
        {
            get { return OperationCode.Data; }
        }
        public void getData(int type)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(parameters, ParameterCode.DataType, type, false);
            PhotonEngine.Instance.SendRequest(OpCode, SubCode.GetData, parameters);
        }
        public void updateData<T>(T data, int type)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            parameters.Add((byte)ParameterCode.Data, JsonMapper.ToJson(data));
            ParameterTool.AddParameter(parameters, ParameterCode.DataType, type, false);
            PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateData, parameters);
        }
        public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response)
        {
            int type;
            SubCode subcode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode, false);
            switch (subcode)
            {
                case SubCode.GetData:
                    type=ParameterTool.GetParameter<int>(response.Parameters,ParameterCode.DataType,false);
                    if(type==1)
                    {
                        List<level1Data> list = ParameterTool.GetParameter<List<level1Data>>(response.Parameters, ParameterCode.Data);
                        GameController.Instance.setLevel1Data(list[0]);
                        //level1Data le = GameController.Instance.getLevel1Data();
                        //le.topSpeed=1234;
                        //le.shortestTime=1234;
                        //updateData<level1Data>(le,1);
                        Debug.Log("获得战信息"+GameController.Instance.getLevel1Data().shortestTime);
                    }
                    if (type == 2)
                    {
                        List<level2Data> levelData = ParameterTool.GetParameter<List<level2Data>>(response.Parameters, ParameterCode.Data);

                    }
                   
                    Debug.Log("受到role");
                    break;
                case SubCode.UpdateData:
                   
                    break;
            }
        }
    }
}
