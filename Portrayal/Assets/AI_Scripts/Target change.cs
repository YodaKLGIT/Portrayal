using KinematicCharacterController.Examples;
using System.Collections.Generic;
using UnityEngine;

public class Targetchange : MonoBehaviour
{


    public GameObject NPC;
    public int AmountOfAreas;
    public List<Vector3> locations;
    public int currentLocation = 0;

    void Start()
    {

    }


    void Update()
    {

    }


    public void OnTriggerEnter(UnityEngine.Collider NPC)
    {
        Vector3 teleport = transform.position;

        teleport = locations[currentLocation];
        currentLocation++;
        currentLocation = (currentLocation++) % AmountOfAreas;


        Debug.Log("Fox has hit the trigger");

        //if(currentLocation == 0)
        //{
        //    teleport.x = 12;
        //    teleport.y = 10;
        //    teleport.z = -8;
        //}

        //if (currentLocation == 1)
        //{
        //    teleport.x = 0;
        //    teleport.y = 9;
        //    teleport.z = 14;


        //}
        //if (currentLocation == 2)
        //{
        //    teleport.x = -11;
        //    teleport.y = 4;
        //    teleport.z = 14;


        //}


        transform.position = teleport;
        //make a list for all teleports
        //make it easy to change the destinations


    }

    public void test(int GiveAreaNumber)
    {
        GiveAreaNumber = AmountOfAreas;
        return;
    }
}

