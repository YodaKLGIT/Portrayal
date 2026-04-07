using System.Collections.Generic;
using UnityEngine;

public class NPC1AI : MonoBehaviour
{

    public int AmountOfAreas;
    public List<Vector3> locations;
    public int currentLocation = 0;
    [SerializeField] public float Interval = 15;
    float timer;

    void Start()
    {

    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Interval)
        {
            NextMove();
            timer -= Interval;
        }
    }


    public void NextMove()
    {
        Vector3 teleport = transform.position;

        teleport = locations[currentLocation];
        currentLocation++;
        currentLocation = (currentLocation++) % AmountOfAreas;


        

        transform.position = teleport;
        
    }
}
