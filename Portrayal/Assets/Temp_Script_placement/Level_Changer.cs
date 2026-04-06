using UnityEngine;

public class Level_Changer : MonoBehaviour
{
    bool level1;
    bool level2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 height = new Vector3();
        height.y = 1000;
        transform.position = height;
    }

    // Update is called once per frame
    void Update()
    {
        //if (level1 == true)
        //{
        //  height.y = -4.6
        //}

        //if (level2 == true)
        //{
        //  height.y = -45.1
        //}

        // alleen nog zorgen dat het met gameobject alleen de goede levels pakken.
    }
}
