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
        if (Input.GetMouseButtonDown(0)) // Detects left mouse button click
        {
            PolaroidImage.SetActive(false);
            Canvas.SetActive(false);
        }
    }
}
