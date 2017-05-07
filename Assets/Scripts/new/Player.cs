using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int roleID = -1;
    public double TopSpeed;
    public int ColiderCount;
    public int RocketSend;
    public int RocketBoom;
    public double UseTime;
    void Start()
    {
        TopSpeed = 0;
        ColiderCount = 0;
        RocketSend = 0;
        UseTime = 0;
        RocketBoom = 0;
    }

    void Update()
    {
        UseTime += Time.deltaTime;
    }

}
