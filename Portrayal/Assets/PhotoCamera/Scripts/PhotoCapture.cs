using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    private const int MaxGalleryPhotos = 6;

    [Header("Camera")]
    [SerializeField] private Camera photoCamera;

    [Header("Resolution")]
    [SerializeField] private int photoWidth = 1920;
    [SerializeField] private int photoHeight = 1080;

    [Header("UI")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;

    [Header("Reticle")]
    [SerializeField] private Image focusReticle;
    [SerializeField] private Color idleColor = Color.white;
    [SerializeField] private Color validColor = Color.green;

    [Header("Animation")]
    [SerializeField] private Animator fadeAnimation;
    [SerializeField] private Animator pictureDownAnimation;

    [Header("Audio")]
    [SerializeField] private AudioSource cameraShutterSound;

    [Header("Gallery")]
    public List<Sprite> spriteList;
    public List<Image> galleryList;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    private bool isAnimating = false;
    private Vector3 photoFrameStartLocalPosition;

    private int pictureTillEnding = 0;

    private List<Photographable> currentValidTargets = new List<Photographable>();

    private void Awake()
    {
        spriteList = new List<Sprite>(MaxGalleryPhotos);
        photoFrameStartLocalPosition = photoFrame.transform.localPosition;
        RefreshGalleryDisplay();
    }

    private void Update()
    {
        if (isAnimating)
        {
            return;
        }

        UpdateFocusCheck();
        SendToEndScreen();

        if (Input.GetMouseButtonDown(0))
        {
            if (!viewingPhoto)
                StartCoroutine(CapturePhoto());
            else
                RemovePhoto();
        }
    }

    // reticle stuff
    private void UpdateFocusCheck()
    {
        currentValidTargets = GetValidTargets();

        if (focusReticle != null)
        {
            focusReticle.color = currentValidTargets.Count > 0 ? validColor : idleColor;
        }
    }

    // capture photo
    // capture photo
    private IEnumerator CapturePhoto()
    {
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        RenderTexture rt = new RenderTexture(photoWidth, photoHeight, 24);
        photoCamera.targetTexture = rt;

        screenCapture = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);

        photoCamera.Render();

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        screenCapture.ReadPixels(new Rect(0, 0, photoWidth, photoHeight), 0, 0);
        screenCapture.Apply();

        photoCamera.targetTexture = null;
        RenderTexture.active = currentRT;
        Destroy(rt);

        bool capturedNewObject = false;

        if (currentValidTargets.Count > 0)
        {
            foreach (var obj in currentValidTargets)
            {
                // ONLY capture if not already captured
                if (!obj.IsCaptured())
                {
                    obj.Capture();
                    capturedNewObject = true;
                    pictureTillEnding++;
                }
            }
        }

        // ALWAYS show photo, but only save if NEW object
        ShowPhoto(capturedNewObject);

        if (!capturedNewObject)
        {
            Debug.Log("No new object captured");
        }

        cameraShutterSound.Play();
    }

    // Target detect

    private List<Photographable> GetValidTargets()
    {
        List<Photographable> result = new List<Photographable>();

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(photoCamera);
        Photographable[] all = FindObjectsByType<Photographable>(FindObjectsSortMode.None);

        foreach (var obj in all)
        {
            Renderer rend = obj.GetComponentInChildren<Renderer>();
            if (rend == null) continue;

            // In camera view
            if (!GeometryUtility.TestPlanesAABB(planes, rend.bounds))
                continue;

            Vector3 viewportPos = photoCamera.WorldToViewportPoint(rend.bounds.center);

            // Behind camera
            if (viewportPos.z < 0)
                continue;

            // Center check
            if (Mathf.Abs(viewportPos.x - 0.5f) > obj.requiredCenterAccuracy ||
                Mathf.Abs(viewportPos.y - 0.5f) > obj.requiredCenterAccuracy)
                continue;

            // Distance check
            float distance = Vector3.Distance(photoCamera.transform.position, rend.bounds.center);
            if (distance > obj.maxDistance)
                continue;

            // Line of sight
            Vector3 dir = (rend.bounds.center - photoCamera.transform.position).normalized;

            if (Physics.Raycast(photoCamera.transform.position, dir, out RaycastHit hit, obj.maxDistance))
            {
                var hitPhoto = hit.collider.GetComponentInParent<Photographable>();

                if (hitPhoto == obj)
                {
                    result.Add(obj);
                }
            }
        }

        return result;
    }

    // display photo
    private void ShowPhoto(bool saveToGallery)
    {
        photoFrame.transform.localPosition = photoFrameStartLocalPosition;

        Sprite photoSprite = Sprite.Create(
            screenCapture,
            new Rect(0, 0, screenCapture.width, screenCapture.height),
            new Vector2(0.5f, 0.5f),
            100f
        );

        photoDisplayArea.sprite = photoSprite;

        // ONLY save if valid
        if (saveToGallery)
        {
            SavePhotoToGallery(photoSprite);
        }

        photoFrame.SetActive(true);
        fadeAnimation.Play("PhotoFade", 0, 0f);
    }

    private void SavePhotoToGallery(Sprite photoSprite)
    {
        Debug.Log("Saving photo to gallery");

        if (spriteList.Count >= MaxGalleryPhotos)
            spriteList.RemoveAt(spriteList.Count - 1);

        spriteList.Insert(0, photoSprite);

        Debug.Log("Gallery count: " + spriteList.Count);

        RefreshGalleryDisplay();
    }

    private void RefreshGalleryDisplay()
    {
        if (galleryList == null || galleryList.Count == 0)
        {
            Debug.LogWarning("Gallery UI not assigned!");
            return;
        }

        for (int i = 0; i < galleryList.Count; i++)
        {
            if (galleryList[i] == null)
            {
                Debug.LogWarning("Gallery slot is NULL at index " + i);
                continue;
            }

            galleryList[i].sprite = (i < spriteList.Count) ? spriteList[i] : null;
        }
    }

    // remove photo
    private void RemovePhoto()
    {
        if (!isAnimating)
            StartCoroutine(RemoveSequence());
    }

    private IEnumerator RemoveSequence()
    {
        isAnimating = true;

        pictureDownAnimation.Play("PictureDown", 0, 0f);

        yield return null;

        while (pictureDownAnimation.GetCurrentAnimatorStateInfo(0).IsName("PictureDown") &&
               pictureDownAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        viewingPhoto = false;
        photoFrame.SetActive(false);

        isAnimating = false;
    }

    public void SendToEndScreen()
    {
        if (pictureTillEnding == 6)
        {
            StartCoroutine(SceneSwitch());
        }
    }

    private IEnumerator SceneSwitch()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EindScene");
        yield return null;
    }
}