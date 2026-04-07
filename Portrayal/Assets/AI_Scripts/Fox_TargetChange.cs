using System.Collections.Generic;
using UnityEngine;

public class Fox_TargetChange : MonoBehaviour
{
    
    public int AmountOfAreas;
    public List<Vector3> locations;
    public int currentLocation = 0;

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        FoxCaught();
    }
    public void FoxCaught()
    {
        Vector3 teleport = transform.position;

        teleport = locations[currentLocation];
        currentLocation++;
        currentLocation = (currentLocation++) % AmountOfAreas;


        Debug.Log("Fox has hit the trigger");

        transform.position = teleport;
       //delete the old so it doesnt want to go back
       //make it Amountoflocations - 1
    }
}
