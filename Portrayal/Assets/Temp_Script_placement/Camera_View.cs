using System.Collections.Generic;
using UnityEngine;

public class Camera_View : MonoBehaviour
{
    public List<Vector3> locations;
    public int currentLocation = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextView()
    {
        Vector3 teleport = transform.position;

        teleport = locations[currentLocation];
        currentLocation++;
        transform.position = teleport;
    }
}
