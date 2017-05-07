using System.Collections.Generic;
using TaidouCommon.Model;
using UnityEngine;
public delegate void OnGetRoleEvent(List<Role>roleList);
public delegate void OnAddRoleEvnet(Role role);
public delegate void OnSelectRoleEvent();
public delegate void OnGetTeamEvent(List<Role> rolelist, int masterRoleID);//组队成功

public delegate void OnGetRoomNumber(int roomNumber);//获得房间号
public delegate void OnWaitingTeamEvent();//等待组队中

public delegate void OnCancelTeamEvent();//取消组队的响应

public delegate void OnSyncPositionAndRotationEvent(int roleid, Vector3 position, Vector3 eulerAngles);

public delegate void OnSyncDirection(int roleId,TouchDir dir);
public delegate void OnSyncBool(bool istrue);
public delegate void OnSyncElements(int roleId,Dictionary<byte,object> dic);
public delegate void OnSyncSpeed(int roleId,float speed);
public delegate void OnSyncMoveAnimationEvent(int roleid, PlayerState playState);
//public delegate void OnCreateEnemyEvent(CreateEnemyModel model);

//public delegate void OnSyncEnemyPositionRotationEvent(EnemyPositionModel model);

//public delegate void OnSyncEnemyAnimationEvent(EnemyAnimationModel model);

public delegate void OnSyncPlayerAnimationEvent(int roleId, PlayerAnimationModel model);

public delegate void OnGameStateChangeEvent(GameStateModel model);

//public delegate void OnSyncBossAnimationEvent(BossAnimationModel model);
