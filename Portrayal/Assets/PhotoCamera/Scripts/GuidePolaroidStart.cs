using System.Collections;
using UnityEngine;

public class GuidePolaroidStart : MonoBehaviour
{
    public GameObject PolaroidImage;
    public GameObject Canvas;

    void Start()
    {
        Canvas.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PolaroidImage.SetActive(false);
            Canvas.SetActive(false);
        }
    }
}
