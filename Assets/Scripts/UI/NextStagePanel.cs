using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextStagePanel : BasePanel<NextStagePanel>
{
    public string NextStageName;
    public override void Start()
    {
        base.Start();
        Button MenuBtn = UITool.FindChild<Button>(gameObject, "MenuBtn");
        Button NextBtn = UITool.FindChild<Button>(gameObject, "NextBtn");

        MenuBtn.onClick.AddListener(OnMenuBtnClick);
        NextBtn.onClick.AddListener(OnNextBtnClick);
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene(NextStageName);
        Hide();
    }
    public void OnNextBtnClick()
    {
        NextStageS.NextStageName = NextStageName;
        SceneManager.LoadScene("03Animation");
        Hide();
    }
}
