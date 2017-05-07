using UnityEngine;
using System.Collections;

public enum TouchDir {
    None,
    Left,
    Right,
    Top,
    Bottom
}

public class PlayerMove : MonoBehaviour {
    public float speedup = 0;
    public float speedadd = 0.02f;
    public static int difficulty = 3;
    public static bool walk = false;

    public AudioSource footLand;
    public float jumpHeight = 10f;
    public float speed = 10;
    public float horizontalMoveSpeed = 0.4f;
    public float jumpSpeed = 0.2f;
    public float minTouchLength = 20;
    public Transform prisoner;

    public bool isJumping = false;
    public bool isUp = false;
    public float slideTime = 1.4f;

    public int nowLaneIndex = 1;
    public int targetLaneIndex = 1;

    public bool isSliding = false;

    private float slideTimer=0;
   
    private float targetJumpHeight = 0;
    private EnvGenerator env;
    private Vector3 moveDownPos = Vector3.zero;
    public float moveHorizontal = 0;

   
    public bool isCanControl = false;
    private BattleController battleController;
    public bool isMove = true;
    private TouchDir dir = TouchDir.None;
    public bool twoWay = false;//变为两道
    private Vector3 pos;//当前物体所在位置
    private Vector3 nextWayPoint;
    private int targetWayPointIndex = 40;
    void Start() {
        env = Camera.main.GetComponent<EnvGenerator>();
        battleController = GameController.battleController;
    }
	
	// Update is called once per frame
	void Update () {
            if (GameController.gameState == GameState.Playing)
            {
                pos = transform.position;
                //下一个点位置
                //如果当前模式是关卡或者双人对战，新的模式点
                if (GameController.isTeam)
                {
                    Debug.Log("双人");
                    nextWayPoint = GameObject.FindGameObjectWithTag(Tags.tlWayPoints).GetComponent<tlWayPoints>().GetNextWayPoint();
                }
                else
                {
                    nextWayPoint = env.forest1.GetNextWayPoint();
                }
                //偏移后的下一个点位置
                nextWayPoint = new Vector3(nextWayPoint.x + GameController.xOffsets[targetLaneIndex], nextWayPoint.y, nextWayPoint.z);
                Vector3 dir = nextWayPoint - transform.position;//两位置差值
                Vector3 moveDir = dir.normalized * speed * Time.deltaTime;//单位距离
                this.transform.position += moveDir;
                transform.rotation = Quaternion.LookRotation(new Vector3(nextWayPoint.x, transform.position.y, nextWayPoint.z) - transform.position, Vector3.up);
                //transform.LookAt(nextWayPoint);

                if (targetLaneIndex != nowLaneIndex)
                {
                    float move = moveHorizontal * horizontalMoveSpeed;//0*speed
                    moveHorizontal -= moveHorizontal * horizontalMoveSpeed;
                    this.transform.position = new Vector3(transform.position.x + move, transform.position.y, transform.position.z);
                    if (Mathf.Abs(moveHorizontal) < 0.5f)
                    {
                        this.transform.position = new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
                        nowLaneIndex = targetLaneIndex;
                    }
                }
                if (isJumping)
                {
                    
                    float yMove = jumpSpeed * Time.deltaTime;
                    if (isUp)
                    {
                        prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y + yMove, prisoner.position.z);
                        targetJumpHeight += yMove;
                        if (jumpHeight <= targetJumpHeight || isSliding)
                        {
                            prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y + jumpHeight - targetJumpHeight, prisoner.position.z);
                            isUp = false;
                            targetJumpHeight = jumpHeight;
                        }
                    }
                    else
                    {
                        if (isSliding)
                        {
                            jumpSpeed = 50;
                            slideTime = 1.3f;
                        }
                        prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y - yMove, prisoner.position.z);
                        targetJumpHeight -= yMove;
                        if (targetJumpHeight < 0.5f)
                        {
                            prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y - (targetJumpHeight - 0), prisoner.position.z);
                            isJumping = false;
                            targetJumpHeight = 0;
                            jumpSpeed = 25;
                        }

                }
            }

                if (isSliding)
                {
                    slideTimer += Time.deltaTime;
                    if (slideTimer > slideTime)
                    {
                        isSliding = false;
                        slideTimer = 0;
                    }
                }

                MoveControll();
            }
	}
    void MoveControll() {
        //TouchDir touchDir = GetTouchDir();
        GetTouchDir();
        switch (dir) {
            case TouchDir.None:
                break;
            case TouchDir.Right:
                if (twoWay)
                {
                    targetLaneIndex = 2;
                    moveHorizontal = 20;
                }
                else
                {
                    if (targetLaneIndex < 2)
                    {
                        targetLaneIndex++;
                        moveHorizontal = 10;
                        
                    }
                }
                dir = TouchDir.None;
                break;
            case TouchDir.Left:
                if (twoWay)
                {
                    targetLaneIndex = 0;
                    moveHorizontal = -20;
                }
                else
                {
                    if (targetLaneIndex > 0)
                    {
                        targetLaneIndex--;
                        moveHorizontal = -10;
                    }
                }
                dir = TouchDir.None;
                break;
            case TouchDir.Top:
                if (isJumping == false)
                {
                    isSliding = false;

                    isJumping = true;
                    isUp = true;
                    //     targetJumpHeight = jumpHeight;
                    dir = TouchDir.None;
                }
                break;
            case TouchDir.Bottom:
                if (isSliding == false)
                {
                    isSliding = true;
                    dir = TouchDir.None;
                }
                break;
        }

    }

    void GetTouchDir() {
        
        if (Input.GetMouseButtonDown(0)) {
            
            moveDownPos = Input.mousePosition;
            if (GameController.isTeam == false)
            {
                 dir = TouchDir.None;
            }
            else
            {
                if (isCanControl)
                {
                    //battleController.SyncDirection(TouchDir.None);
                    battleController.SyncElements<TouchDir>(TouchDir.None,SyncType.Direction);
                    dir = TouchDir.None;
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            
            Vector3 moveOffset = Input.mousePosition - moveDownPos;
            if( Mathf.Abs( moveOffset.y ) > Mathf.Abs( moveOffset.x ) && moveOffset.y > minTouchLength)
            {

                if (GameController.isTeam == false)
                {
                    dir = TouchDir.Top;
                }
                else
                {
                    if (isCanControl)
                    {
                        Debug.Log("同步跳");
                        battleController.SyncElements<TouchDir>(TouchDir.Top, SyncType.Direction);
                        //battleController.SyncDirection(TouchDir.Top);
                        dir = TouchDir.Top;
                    }
                }
            }

            if (Mathf.Abs(moveOffset.y) > Mathf.Abs(moveOffset.x) && moveOffset.y < -minTouchLength) {
                if (GameController.isTeam == false)
                {
                        dir = TouchDir.Bottom;
                }
                else
                {

                    if (isCanControl)
                    {
                        //battleController.SyncDirection(TouchDir.Bottom);
                        battleController.SyncElements<TouchDir>(TouchDir.Bottom, SyncType.Direction);
                        dir = TouchDir.Bottom;
                    }
                }
            }

            if (Mathf.Abs(moveOffset.y) < Mathf.Abs(moveOffset.x) && moveOffset.x > minTouchLength) {

                if (GameController.isTeam == false)
                {
                        dir = TouchDir.Right;
                }
                else
                {
                    if (isCanControl)
                    {
                        //battleController.SyncDirection(TouchDir.Right);
                        battleController.SyncElements<TouchDir>(TouchDir.Right, SyncType.Direction);
                        dir = TouchDir.Right;
                    }
                }
            }

            if (Mathf.Abs(moveOffset.y) < Mathf.Abs(moveOffset.x) && moveOffset.x < -minTouchLength) {

                if (GameController.isTeam == false)
                {
                        dir = TouchDir.Left;
                }
                else
                {
                    if (isCanControl)
                    {
                        //battleController.SyncDirection(TouchDir.Left);
                        battleController.SyncElements<TouchDir>(TouchDir.Left, SyncType.Direction);
                        dir = TouchDir.Left;
                    }
                }
            }

        }
    }
   
    public void SetPositionAndRotation(Vector3 position, Vector3 eulerAngles)
    {
        transform.position = position;
        transform.eulerAngles = eulerAngles;
    }
    public void SetTouchDir(TouchDir dir1)
    {
        dir = dir1;
    }
    public void SetSpeed(float speed1)
    {
        speed = speed1;
    }
}
