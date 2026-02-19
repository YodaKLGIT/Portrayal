using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Camera")]
    [SerializeField] private Camera photoCamera;
    [SerializeField] private int photoWidth = 1920;
    [SerializeField] private int photoHeight = 1080;

    [Header("Photo Capture Settings")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;

    [Header("Photo Fade Effect")]
    [SerializeField] private Animator fadeAnimation;

    [Header("Photo Audio")]
    [SerializeField] private AudioSource cameraShutterSound;

    private Texture2D screenCapture;
    private bool viewingPhoto;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }
        }
    }

    IEnumerator CapturePhoto()
    {
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        // Create a temporary render texture with 1920x1080
        RenderTexture rt = new RenderTexture(photoWidth, photoHeight, 24);
        photoCamera.targetTexture = rt;

        // Create texture to store the photo
        screenCapture = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);

        // Render the photo camera
        photoCamera.Render();

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        // Read pixels from the photo camera
        screenCapture.ReadPixels(new Rect(0, 0, photoWidth, photoHeight), 0, 0);
        screenCapture.Apply();

        // Cleanup
        photoCamera.targetTexture = null;
        RenderTexture.active = currentRT;
        Destroy(rt);

        ShowPhoto();
        cameraShutterSound.Play();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(
            screenCapture,
            new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height),
            new Vector2(0.5f, 0.5f),
            100.0f
        );

        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
        fadeAnimation.Play("PhotoFade");
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }
}
