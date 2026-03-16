using UnityEngine;

public class Targetchange : MonoBehaviour
{

    public GameObject Fox;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("this works!");
        }
    }


    public void OnTriggerEnter(UnityEngine.Collider Fox)
    {
        Debug.Log("Fox has hit the trigger");

        //make it be able to teleport
        //make a list for all teleports
        //make it easy to change the destinations
    }
}
