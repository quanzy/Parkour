using UnityEngine;
using System.Collections;

public class TeamPower : MonoBehaviour {


    public float moveSpeed2 = 150;

    void Update()
    {
        if (GameController.gameState == GameState.Playing)
        {
            float a = 0;
            a += moveSpeed2 * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + a);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player || other.tag == Tags.Player1)
        {
            Destroy(this.gameObject);
        }

    }

}
