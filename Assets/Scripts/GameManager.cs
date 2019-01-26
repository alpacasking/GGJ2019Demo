using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float TimeLimit;
    public float Timer;
    public Text TimerText;
    public Text ResultText;
    public float StartTime;
    public enum GameState{
        Starting,
        Playing,
        Clear,
        Fail,
        Pause,
    }
    public GameState State;
    public StationManager stationManager;
    public InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        ResultText.text = "开始动画";
        State = GameState.Starting;
        inputManager.IsValid = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameState.Playing:
                if (Timer <= 0)
                {
                    ResultText.text = "失败";
                    inputManager.IsValid = false;
                    break;
                }
                Timer -= Time.deltaTime;
                TimerText.text = Timer + "s";
                if (stationManager.IsPassengeAllLeft())
                {
                    ResultText.text = "过关";
                    inputManager.IsValid = false;
                    break;
                }
                break;
            case GameState.Starting:
                Timer += Time.deltaTime;
                if (Timer >= StartTime)
                {
                    State = GameState.Playing;
                    Timer = TimeLimit;
                    ResultText.text = "游戏中";
                    inputManager.IsValid = true;
                }
                break;
            case GameState.Pause:
                break;
            case GameState.Clear:
            case GameState.Fail:
                break;
            default:
                break;
        }
    }
    
}
