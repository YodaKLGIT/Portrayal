using System.Collections;
using UnityEngine;

public class Gallery : MonoBehaviour
{
    [SerializeField] private Animator galleryAnimator;
    private string openAnimationName = "GalleryUp";
    private string closeAnimationName = "GalleryDown";

    private bool isGalleryOpen = false;
    private bool isAnimating = false;

    private void Update()
    {
        if (isAnimating)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleGallery();
        }
    }

    private void ToggleGallery()
    {
        string animationName = isGalleryOpen ? closeAnimationName : openAnimationName;
        StartCoroutine(PlayGalleryAnimation(animationName));
    }

    private IEnumerator PlayGalleryAnimation(string animationName)
    {
        isAnimating = true;

        galleryAnimator.Play(animationName, 0, 0f);

        yield return null;

        while (galleryAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               galleryAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        isGalleryOpen = !isGalleryOpen;
        isAnimating = false;

        Debug.Log(isGalleryOpen ? "Gallery open" : "Gallery close");
    }
}