using UnityEngine;

public class Camera_Switcher : MonoBehaviour
{
    public Camera FirstPerson;
    public Camera TopDown;

    
    void Start()
    {
        TopDown.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        FirstPerson.enabled = !FirstPerson.enabled;
        TopDown.enabled = !TopDown.enabled;
    }
}
