using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class battleTimeDialog : MonoBehaviour {
    
    private Text stateLabel;//目前搜索状态
    public  Text timeLabel;//目前用时状态
    private float timer = 0;
    private bool isStart = true ;
    public float time = 30;
    public  Canvas searchCanvas;
    void Start()
    {
        //searchCanvas = this.transform.Find("searchCanvas").GetComponent<Canvas>();
        //从当前物体获得UI
        stateLabel = this.transform.Find("stateLabel").GetComponent<Text>();
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
                OnTimeEnd();
            }
        }
    }
    public void onCancelButtonClick()
    {
        OnTimeEnd();
    }
    //显示组队计时器
    public void ShowTimer()
    {

    }
    //隐藏计时器
    public void HideTimer() 
    {

    }
    void OnTimeEnd()
    {
        GameController.battleController.CancelTeam();
        searchCanvas.gameObject.SetActive(false);
    }
}
