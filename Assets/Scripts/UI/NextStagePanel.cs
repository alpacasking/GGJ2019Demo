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
        SceneManager.LoadScene("01StartScene");
        Hide();
    }
    private void OnNextBtnClick()
    {
        SceneManager.LoadScene("Level1");
        Hide();
    }
}
