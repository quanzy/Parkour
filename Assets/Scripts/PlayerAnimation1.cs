using UnityEngine;
using System.Collections;
public class PlayerAnimation1 : MonoBehaviour {
    public AudioSource footStep;
    public Animation animation;

    private PlayerState state = PlayerState.Idle;
    private PlayerMove1 playerMove1;
    private BattleController battleController;

    void Start() {
        playerMove1 = this.GetComponent<PlayerMove1>();
        battleController = GameController.battleController;
    }

    // Update is called once per frame
    void Update() 
    {
            if (GameController.gameState == GameState.Menu)
            {
                state = PlayerState.Idle;
            }
            else if (GameController.gameState == GameState.Playing)
            {
                state = PlayerState.Running;

                if (playerMove1.isJumping)
                {
                    state = PlayerState.Jumping;
                }
                else if (playerMove1.isSliding)
                {
                    state = PlayerState.Sliding;
                }
            }
            else if (GameController.gameState == GameState.End)
            {
                state = PlayerState.Death;

            }
           
        
    }

    void LateUpdate()
    {
            switch (state)
            {
                case PlayerState.Idle:
                    PlayIdle();
                    break;
                case PlayerState.Running:
                    PlayRunning();
                    break;
                case PlayerState.Jumping:
                    PlayJump();
                    break;
                case PlayerState.Sliding:
                    PlaySlide();
                    break;
                case PlayerState.Death:
                    PlayDeath();
                    break;
            }
            if (state == PlayerState.Running)
            {
                if (!footStep.isPlaying)
                {
                    footStep.Play();
                }
            }
            else
            {
                footStep.Stop();
            }
        
    }

    void PlayRunning() {
        if (!animation.IsPlaying("run")) {
            animation.Play("run");
        }
    }

    void PlayIdle() {
        if (!animation.IsPlaying("Idle_1") && !animation.IsPlaying("Idle_2")) {
            animation.Play("Idle_1");
            animation.PlayQueued("Idle_2");
        }
    }

    void PlayJump() {
        if (!animation.IsPlaying("jump")) {
            //animation["slide"].speed = 1.5f;
            animation.Play("jump");
        }
    }
    void PlaySlide() {
        if (!animation.IsPlaying("slide")) {
            //animation["slide"].speed = 0.5f;
            animation.Play("slide");
        }
    }
    private bool havePlayDeath = false;
    void PlayDeath() {
        if (havePlayDeath == false) {
            havePlayDeath = true;
            animation.Play("death");
        }
    }
    public void setState(PlayerState playerState)
    {
        state = playerState;
    }
}
