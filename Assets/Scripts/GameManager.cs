using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Transform train;
    public Text Stage;
    public Image StageLabel;


    public float TimeLimit;
    public float Timer;
    public Text TimerText;
    public GameObject TimerObj;
    public Text ResultText;
    public float StartTime;
    public SpriteRenderer trainRenderer;
    public Sprite GOsprite;
    public Sprite Stopsprite;
    public EscPanel escpanel;
    public string NextStageName;
    public GameObject TrainBlockObj;
    public GameObject TrainBarrier;
    public enum GameState{
        Starting,
        Playing,
        Clear,
        Fail,
        Pause,
        End,
    }
    public GameState State;
    public StationManager stationManager;
    public InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        NextStagePanel.Instance.NextStageName = NextStageName;
        Timer = 0;
        ResultText.text = "开始动画";
        State = GameState.Starting;
        inputManager.IsValid = false;
        trainRenderer.sprite = GOsprite;
        StartAnim();
        TrainBlockObj.SetActive(false);
        TrainBarrier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameState.Playing:
                if (Timer <= 0)
                {
                    GameOverPanel.Instance.Show();
                    Debug.Log("fail");
                    inputManager.IsValid = false;
                    State = GameState.Fail;
                    break;
                }
                Timer -= Time.deltaTime;
                TimerText.text = (int)Timer + "s";
                if (stationManager.IsPassengeAllLeft())
                {
                    NextStagePanel.Instance.Show();
                    inputManager.IsValid = false;
                    State = GameState.Clear;
                    break;
                }
                break;
            case GameState.Starting:
                Timer += Time.deltaTime;
                if (Timer >= StartTime)
                {
                    TimerObj.SetActive(true);
                    State = GameState.Playing;
                    Timer = TimeLimit;
                    TrainBlockObj.SetActive(true);
                    ResultText.text = "游戏中";
                    inputManager.IsValid = true;
                    trainRenderer.sprite = Stopsprite;
                    TrainBarrier.SetActive(true);
                }
                break;
            case GameState.Pause:
                break;
            case GameState.Clear:
                break;
            case GameState.Fail:
                StartCoroutine(DelayEscPanel());
                State = GameState.End;
                break;
            default:
                break;
        }
    }


    IEnumerator DelayEscPanel()
    {
        yield return new WaitForSeconds(1);
        escpanel.Show();
    }

    public void StartAnim()
    {

        //StartCoroutine(Playaudio());
        Sequence sequence = DOTween.Sequence();
        sequence.Append(Stage.DOFade(1, 1));
        sequence.Join(StageLabel.DOFade(1, 1));
        sequence.Insert(1.5f, Stage.DOFade(0, 1));
        sequence.Insert(1.5f, StageLabel.DOFade(0, 1));


        train.transform.position = new Vector3(-40, train.transform.position.y, 0);
        train.DOLocalMoveX(-0.28f, 7).SetEase(Ease.OutExpo);

        TimerObj.SetActive(false);
    }
}
