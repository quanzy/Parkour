using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    private Text timeLabel;//目前用时状态
    private double timer = 0;
    private double time = 3;
    private Canvas countDownCanvas;
    private bool isStart = true;
    void Start()
    {
        countDownCanvas = this.GetComponent<Canvas>();
        timeLabel = this.transform.Find("timeLabel").GetComponent<Text>();
    }

    void Update()
    {
        if (isStart)
        {
            timer += Time.deltaTime;
            int remainTime = (int)(time - timer);
            timeLabel.text = remainTime.ToString();
            if (timer > time)
            {
                timer = 0;
                isStart = false;
                countDownCanvas.gameObject.SetActive(false);
                endtime();
            }
        }
    }
    void endtime()
    {
        GameController.gameState = GameState.Playing;
    }
}
