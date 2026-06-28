using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance { get; private set; }

    [SerializeField] private Image image;
    private bool hasTransitioned = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasTransitioned)
        {
            StartSceneTransition("NewMapScene");
            hasTransitioned = true;
        }
    }

    public void SimpleSceneSwitch(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartSceneTransition(string sceneName)
    {
        StartCoroutine(SmoothSceneTransition(sceneName));
    }

    public IEnumerator SmoothSceneTransition(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(SmoothFadeIn());

        SceneManager.LoadScene(sceneName);

        yield return null;

        yield return StartCoroutine(SmoothFadeOut());
    }

    private IEnumerator SmoothFadeIn()
    {
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);

            image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
                alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(
            image.color.r,
            image.color.g,
            image.color.b,
            1f);
    }

    private IEnumerator SmoothFadeOut()
    {
        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
                alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(
            image.color.r,
            image.color.g,
            image.color.b,
            0f);
    }
}