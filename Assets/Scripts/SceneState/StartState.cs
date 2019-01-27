using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class StartState : ISceneState
{
    public StartState(SceneStateController controller) : base("01StartScene", controller)
    {

    }

    public Image BG;

    public override void StateStart()
    {
        //BG = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Bg").GetComponent<Image>();
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
        //BG.transform. DOShakePosition(90, 2,1, 50,true);
    }
    public void OnStartButtonClick()
    {
        mController.SetState(new MainState(mController));
    }
    public void OnExitBtnClick()
    {
        Application.Quit();
    }
}
