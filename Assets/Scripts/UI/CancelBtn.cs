using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CancelBtn : MonoBehaviour
{
    private static CancelBtn _instance;
    public static CancelBtn Instance
    {
        get
        {
            return _instance;
        }
    }

    public Button Cancelbtn;
    public CanvasGroup canvasGroup;
    
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Cancelbtn.onClick.AddListener(OnCancel);
        Hide();
    }

    private void OnCancel()
    {
        Hide();
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
}
