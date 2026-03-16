using KinematicCharacterController.Examples;
using System.Collections.Generic;
using UnityEngine;

public class Targetchange : MonoBehaviour
{


    public GameObject Fox;
    
    void Start()
    {
        
    }

    
    void Update()
    {
    
    }


    public void OnTriggerEnter(UnityEngine.Collider Fox)
    {
        Vector3 teleport = transform.position;
        List<int> numbers = new List<int>();
        
        numbers.Add(1);
        numbers.Add(2);
        numbers.Add(3);
        Debug.Log("Fox has hit the trigger");
        for (int i = 0; i < numbers.Count; i++)
        {
            int number = numbers[i];
            if (number == 1)
            {
                teleport.x = 0;
                teleport.z = 14;
            }
        }

        transform.position = teleport;
        //make a list for all teleports
        //make it easy to change the destinations
    }
    
}
