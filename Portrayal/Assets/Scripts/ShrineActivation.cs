using UnityEngine;

public class ShrineActivation : MonoBehaviour
{
    [SerializeField] private GameObject shrineOne;
    [SerializeField] private GameObject shrineTwo;
    [SerializeField] private GameObject shrineThree;

    [SerializeField] private GameObject GateOne;
    [SerializeField] private GameObject GateTwo;
    [SerializeField] private GameObject GateThree;


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
