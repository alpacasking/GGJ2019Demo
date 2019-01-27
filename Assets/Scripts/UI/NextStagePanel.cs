using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextStagePanel : BasePanel<NextStagePanel>
{

    public override void Start()
    {
        base.Start();
        Button MenuBtn = UITool.FindChild<Button>(gameObject, "MenuBtn");
        Button NextBtn = UITool.FindChild<Button>(gameObject, "NextBtn");

        MenuBtn.onClick.AddListener(OnMenuBtnClick);
        NextBtn.onClick.AddListener(OnNextBtnClick);
    }

    private void OnMenuBtnClick()
    {
        SceneManager.LoadScene(0);
        Hide();
    }
    private void OnNextBtnClick()
    {
        //GameManager.Instance.NextStage();
        Hide();
    }
}
