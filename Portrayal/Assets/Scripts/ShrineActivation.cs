using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShrineActivation : MonoBehaviour
{
    [SerializeField] private GameObject shrineOne;
    [SerializeField] private GameObject shrineTwo;
    [SerializeField] private GameObject shrineThree;

    [SerializeField] private GameObject GateOne;
    [SerializeField] private GameObject GateTwo;
    [SerializeField] private GameObject GateThree;

    [SerializeField] private bool finishedAllShrines = false;

    [SerializeField] private GameObject cutsceneCamera;

    private int shrinesActivated = 0;

    private void Update()
    {
        if (finishedAllShrines && shrinesActivated >= 3)
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
        SceneManager.LoadScene("EindScene");
    }

    public void ActivateShrineOne()
    {
        shrineOne.SetActive(true);
        GateTwo.SetActive(false);
        shrinesActivated++;
    }
    public void ActivateShrineTwo()
    {
        shrineTwo.SetActive(true);
        GateThree.SetActive(false);
        shrinesActivated++;
    }
    public void ActivateShrineThree()
    {
        shrineThree.SetActive(true);
        shrinesActivated++;
    }
}
