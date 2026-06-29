using JetBrains.Annotations;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AngelChecker : MonoBehaviour
{
    bool AngelActive = false;
    public WeepingAngel weepingAngel;
    private void Update()
    {
        if (AngelActive == true)
        {
            weepingAngel.AngelsAwake();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AngelActive = true;
    }
}