using UnityEngine;
using System.Collections;
public class TexFadeInAndOut : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayDuration = 4f;
    [SerializeField] private CanvasGroup canvasGroup;

    public void StartFadeInAndOut()
    {
        StartCoroutine(FadeInAndOutCoroutine());
    }

    private IEnumerator FadeInAndOutCoroutine()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        // Wait for display duration
        yield return new WaitForSeconds(displayDuration);
        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
