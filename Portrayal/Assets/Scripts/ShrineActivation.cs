using System.Collections;
using UnityEngine;

public class ShrineActivation : MonoBehaviour
{
    [SerializeField] private GameObject shrineOne;
    [SerializeField] private GameObject shrineTwo;
    [SerializeField] private GameObject shrineThree;

    [SerializeField] private GameObject GateOne;
    [SerializeField] private GameObject GateTwo;
    [SerializeField] private GameObject GateThree;

    [SerializeField] private bool finishedAllShrines = false;

    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject cutsceneCamera;

    //[SerializeField] private CanvasGroup FadeInImage;
    //[SerializeField] private float fadeDuration = 1f;
    //[SerializeField] private float displayDuration = 4f;

    [SerializeField] private SceneSwitcher sceneSwitcher;

    private void Update()
    {
        if (finishedAllShrines)
        {
            StartEnding();
            finishedAllShrines = true;
            return;
        }
    }

    private void StartEnding()
    {
        //player.SetActive(false);
        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        cutsceneCamera.SetActive(true);
        yield return new WaitForSeconds(32.30f);
        sceneSwitcher.SimpleSceneSwitch("EindScene");
    }

    public void ActivateShrineOne()
    {
        shrineOne.SetActive(true);
        GateTwo.SetActive(false);
    }
    public void ActivateShrineTwo()
    {
        shrineTwo.SetActive(true);
        GateThree.SetActive(false);
    }
    public void ActivateShrineThree()
    {
        shrineThree.SetActive(true);
    }
}
