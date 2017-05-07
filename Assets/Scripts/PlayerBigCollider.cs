using UnityEngine;
using System.Collections;
public class PlayerBigCollider : MonoBehaviour
{
    private float a = 1000;
    private float b = 100000;
    private float c = 100000;
    private PlayerMove playerMove1;
    private PlayerMove playerMove;
    private float tag11 = 4;

    private float targetJumpHeight = 21;
    public float jumpSpeed = 40f;
    public float downSpeed = 10f;
    public bool isup = false;
    public bool isdown = false;
    private float timer = 0.792f;
    public bool issofa = false;
    private float cuTime = 0;
    private bool isEnd = false;
    public static bool ontop = false;

    //private bool returnspeed = false;
    private bool wakeup = false;
    private bool returnspeed2 = false;
    private GameObject player;
    private GameObject player1;
    private BattleController battleController;

    public bool isCollider = false;
    private float time = 2;

    void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMove>();
        if(GameController.isTeam)
        {
            playerMove1= GameObject.FindGameObjectWithTag(Tags.Player1).GetComponent<PlayerMove>();
        }
        battleController = GameController.battleController;
        player = GameObject.FindGameObjectWithTag(Tags.player).transform.Find("Prisoner").GetComponent<GameObject>();
        player1 = GameObject.FindGameObjectWithTag(Tags.Player1).transform.Find("Prisoner").GetComponent<GameObject>();

        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.obstacles && !playerMove.isSliding)
        {
            Debug.Log("撞障碍物到了");
            GameController.gameState = GameState.End;
            isEnd = true;
            Destroy(other.gameObject);
        }
        if (other.tag == Tags.diamonds)
        {
            Destroy(other.gameObject);
        }
        if (other.tag == Tags.going && !playerMove.isSliding)
        {
            playerMove.isJumping = false;
        }
        if (other.tag == Tags.up && !playerMove.isSliding)
        {
            isup = true;
            targetJumpHeight = 21;

        }
        if (other.tag == Tags.ontop && !playerMove.isSliding)
        {
            Debug.Log("slzss");
            isdown = false;
            ontop = true;
            playerMove.isJumping = false;
        }
        /*if (other.tag == Tags.Player1 && GameController.gameState == GameState.Playing&&GameController.isTeam)
        {
            //playerMove.speed = 0;
            //battleController.SyncElements<float>(playerMove.speed, SyncType.Speed);
            //player1撞player
            if ((playerMove.targetLaneIndex - playerMove.nowLaneIndex) == 0 && (playerMove1.targetLaneIndex - playerMove1.nowLaneIndex) != 0)
            {
                playerMove.speed -= 40;
                a = 0.5f;
                GameController.Instance.setReturnSpeed(true);
                battleController.SyncElements<bool>(GameController.Instance.getReturnSpeed(), SyncType.ReturnSpeed);
                //battleController.SyncElements<float>(playerMove.speed,SyncType.Speed);
            }
            //两相互碰撞弹开
            if ((playerMove.targetLaneIndex - playerMove.nowLaneIndex)!=0 && (playerMove1.targetLaneIndex - playerMove1.nowLaneIndex) !=0)
            {
                if (playerMove.targetLaneIndex - playerMove.nowLaneIndex == 1)
                {
                    playerMove.targetLaneIndex--;
                    playerMove.moveHorizontal = -10;
                    battleController.SyncElements<TouchDir>(TouchDir.Left, SyncType.Direction);
                }
                else if (playerMove.targetLaneIndex - playerMove.nowLaneIndex == -1)
                {
                    playerMove.targetLaneIndex++;
                    playerMove.moveHorizontal = 10;
                    battleController.SyncElements<TouchDir>(TouchDir.Right, SyncType.Direction);
                }
            }
            
        }*/
        if (other.tag == Tags.Player1 && GameController.gameState == GameState.Playing && GameController.isTeam)
        {
            //发生主动碰撞
            if ((playerMove.targetLaneIndex - playerMove.nowLaneIndex) != 0 && (playerMove1.targetLaneIndex - playerMove1.nowLaneIndex) == 0)
            {
                isCollider = true;
                Debug.Log("同步碰撞");
                battleController.SyncElements<bool>(isCollider, SyncType.Collider);
                playerMove1.speed -= 10;
            }
            //同时碰撞，都往两边弹开，不需要同步
            if ((playerMove.targetLaneIndex - playerMove.nowLaneIndex) != 0 && (playerMove1.targetLaneIndex - playerMove1.nowLaneIndex) != 0)
            {
                if (playerMove.targetLaneIndex - playerMove.nowLaneIndex == 1)
                {
                    //playerMove.targetLaneIndex--;
                    //playerMove.moveHorizontal = -10;
                    //battleController.SyncElements<TouchDir>(TouchDir.Left, SyncType.Direction);
                    playerMove.SetTouchDir(TouchDir.Left);
                }
                if (playerMove.targetLaneIndex - playerMove.nowLaneIndex == -1)
                {
                    //playerMove.targetLaneIndex++;
                    //playerMove.moveHorizontal = 10;
                    //battleController.SyncElements<TouchDir>(TouchDir.Right, SyncType.Direction);
                    playerMove.SetTouchDir(TouchDir.Right);
                }
                if (playerMove1.targetLaneIndex - playerMove1.nowLaneIndex == 1)
                {
                    //playerMove1.targetLaneIndex--;
                    //playerMove1.moveHorizontal = -10;
                    //battleController.SyncElements<TouchDir>(TouchDir.Left, SyncType.Direction);
                    playerMove1.SetTouchDir(TouchDir.Left);
                }
                if (playerMove1.targetLaneIndex - playerMove1.nowLaneIndex == -1)
                {
                    //playerMove1.targetLaneIndex++;
                    //playerMove1.moveHorizontal = 10;
                    //battleController.SyncElements<TouchDir>(TouchDir.Right, SyncType.Direction);
                    playerMove1.SetTouchDir(TouchDir.Right);
                }
            }
            //从上碰撞到
            if(player.transform.position.y>player1.transform.position.y)
            {
                if (!isCollider)
                { 
                    Debug.Log("同步上跳碰撞");
                    battleController.SyncElements<bool>(isCollider, SyncType.Collider);
                    playerMove1.speed -= 10;
                    isCollider = true;
                }
            }
        }
        if (other.tag == Tags.teampowerups && GameController.gameState == GameState.Playing)
        {
            if (GameController.isTeam)
            {
                GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Player>().RocketBoom += 1;
            }

            playerMove.speed -= 100;
            b = 1f;
            wakeup = true;
        }
        if (other.tag == Tags.obstacles && GameController.gameState == GameState.Playing && !playerMove.isSliding)
        {
            playerMove.speed -= 60;
            c = 0.5f;
            if (GameController.isTeam)
            {
                GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Player>().ColiderCount += 1;
            }
            returnspeed2 = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.ontop && !playerMove.isSliding)
        {
            targetJumpHeight = 21;
            isdown = true;
        }
        if (other.tag == Tags.going && !playerMove.isSliding)
        {
            issofa = true;
        }
    }
    void Update()
    {
        if (isEnd)
        {

            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
                GameController.gameState = GameState.Playing;
                isEnd = false;
                timer = 0.792f;

            }
        }
        if (isup)
        {
            if (targetJumpHeight > 0)
            {
                float yJump = jumpSpeed * Time.deltaTime;
                targetJumpHeight -= yJump;
                playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y + yJump, playerMove.transform.position.z);
            
                if (targetJumpHeight < 2f)
                {
                    this.playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y + targetJumpHeight, playerMove.transform.position.z);
                    targetJumpHeight = 0;
                    isup = false;
                }
            }
        }
        if (isdown)
        {
            if (playerMove.isJumping)
            {

            }else
            {
                if (targetJumpHeight > 0)
                {
                    float yJump = jumpSpeed * Time.deltaTime;
                    playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y - yJump, playerMove.transform.position.z);

                    targetJumpHeight -= yJump;
                    if (targetJumpHeight < 2f)
                    {
                        this.playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y - targetJumpHeight, playerMove.transform.position.z);
                        targetJumpHeight = 0;
                        isdown = false;
                        ontop = false;
                    }
                }
            }
        }

        if (issofa)
        {
            if (playerMove.isJumping)
            {

            }
            else
            {
                if (tag11 > 0)
                {
                    float yJump = downSpeed * Time.deltaTime;
                    playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y - yJump, playerMove.transform.position.z);
                    tag11 -= yJump;
                    if (tag11 < 0.5f)
                    {
                        Debug.Log("哈哈");
                        this.playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y - tag11, playerMove.transform.position.z);
                        tag11 = 4;
                        issofa = false;
                    }
                }
            }
        }
        /*a -= Time.deltaTime;
        b -= Time.deltaTime;
        c -= Time.deltaTime;
        if (GameController.Instance.getReturnSpeed() && a < 0)
        {
            playerMove.speed += 40;
            //battleController.SyncElements<float>(playerMove.speed, SyncType.Speed);
            a += 1000;
            GameController.Instance.setReturnSpeed(false);
            battleController.SyncElements<bool>(GameController.Instance.getReturnSpeed(), SyncType.ReturnSpeed);

        }
        if (wakeup && b < 0)
        {
            playerMove.speed += 100;
            b += 100000;
            wakeup = false;
        }
        if (returnspeed2 && c < 0)
        {
            playerMove.speed += 60;
            c += 100000;
            returnspeed2 = false;
        }*/
        if (isCollider)
        {
            time = time - Time.deltaTime;
            if (time <= 0)
            {
                playerMove1.speed += 10;
                Debug.Log("速度增加回来！");
                isCollider = false;
                time = 2;
            }
        }
    }
}
