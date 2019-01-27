using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{

    public Transform train;
    public Transform train2;
    public Image Black;
    public AudioClip Wen;
    public AudioClip kaqia;
    public AudioSource source;
    public string NextSceneName;


    private void Start()
    {
        StartCoroutine(BlackAnimToTrain2());
    }

    private void Update()
    {

    }
    public void Train1InAnim()
    {
        train.transform.position = new Vector3(-40, 0.74f, 0);
        train.DOMoveX(-0.32f, 3).SetEase(Ease.Linear);
    }

    IEnumerator PlayaudioInStation()
    {
        AudioSource.PlayClipAtPoint(Wen, Vector3.zero);
        yield return new WaitForSeconds(1);
        source.volume = 0;
        SceneManager.LoadScene(NextStageS.NextStageName);
    }
    public void PlayaudioOutStation()
    {
        source.volume = 1;
        AudioSource.PlayClipAtPoint(Wen, Vector3.zero);
    }

    public void NextStage()
    {
        train.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/carrige-side");
        NextStageAnim();
        PlayaudioOutStation();
        StartCoroutine(DelayForTrain2Map());
    }
    public void NextStageAnim()
    {
        train.DOMoveX(60, 4).SetEase(Ease.InExpo);
    }
    IEnumerator DelayForTrain2Map()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(BlackAnimToTrain2());
    }

    IEnumerator BlackAnimToTrain2()
    {
        Black.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        train.parent.gameObject.SetActive(false);
        train2.parent.gameObject.SetActive(true);
        Black.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.3f);
        AudioSource.PlayClipAtPoint(Wen, Vector3.zero);
        train2.position = new Vector3(-10, -2.3f, 0);
        train2.DOMoveX(30, 5).SetEase(Ease.Linear);
        yield return new WaitForSeconds(5);
        StartCoroutine(BlackAnimToTrain());
    }

    IEnumerator BlackAnimToTrain()
    {
        Black.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        //train.parent.gameObject.SetActive(true);
        //train2.parent.gameObject.SetActive(false);
        //Black.DOFade(0, 0.5f);
        //yield return new WaitForSeconds(0.5f);
        //Train1InAnim();
        StartCoroutine(PlayaudioInStation());
    }

}
