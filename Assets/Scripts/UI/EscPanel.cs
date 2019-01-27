using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EscPanel:MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    public void Show()
    {
        transform.DOMoveX(0, 0.3f).SetEase(Ease.InOutExpo);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        var top_idx = gameObject.transform.parent.childCount - 3;
        gameObject.transform.SetSiblingIndex(top_idx);
    }
    public virtual void Hide()
    {
        transform.DOMoveX(2000, 0.3f).SetEase(Ease.InOutExpo);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    public void DisplaySwitch()
    {
        if (canvasGroup.alpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (gameObject.transform.Find("CloseBtn") != null)
        {
            Button closeBtn = UITool.GetButton(gameObject, "CloseBtn");
            closeBtn.onClick.AddListener(Hide);
        }
        Hide();
        Button MenuBtn = UITool.FindChild<Button>(gameObject, "MenuBtn");
        Button QuitBtn = UITool.FindChild<Button>(gameObject, "QuitBtn");

        MenuBtn.onClick.AddListener(OnMenuBtnClick);
        QuitBtn.onClick.AddListener(OnQuitBtnClick);
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene("01StartScene");
    }
    public void OnQuitBtnClick()
    {
        Application.Quit();
    }
}
