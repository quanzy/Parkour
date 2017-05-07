using UnityEngine;
using System.Collections;

public class TeamPowerUps2 : MonoBehaviour {

    public Canvas powerButton;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player || other.tag == Tags.Player1)
        {
            
            Destroy(gameObject);
            powerButton.gameObject.SetActive(true);
        }
    }
}
