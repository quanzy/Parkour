using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    private Transform player;

    private Vector3 offset = Vector3.zero;
    private bool haveChangeOffset = false;
    public float moveSpeed = 4;

    void Start() {
        player = GameController.Instance.player.transform;
        offset = transform.position - player.position;
    }

	
	
	// Update is called once per frame
	void Update () {
        if (GameController.gameState == GameState.Playing)
        {
            Vector3 targetPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
        } else if (GameController.gameState == GameState.End) {
            if (haveChangeOffset == false) {
                offset += new Vector3(0, 10, -20);
                haveChangeOffset = true;
            }
            Vector3 targetPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
        //transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, moveSpeed * Time.deltaTime);
	}
}
