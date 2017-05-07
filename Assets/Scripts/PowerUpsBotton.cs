using UnityEngine;
using System.Collections;

public class PowerUpsBotton : MonoBehaviour {
    public GameObject rocket;
    private Vector3 rocket1;
    public Canvas powerButton;
    public GameObject Player;

    public void teamrocketclick()
    {
        rocket1 = Player.transform.position;
        rocket1.z += 20;
        GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Player>().RocketSend += 1;
        GameObject rocketNEW = Instantiate(rocket, rocket1, Quaternion.identity) as GameObject;
        powerButton.gameObject.SetActive(false);
    }
}
