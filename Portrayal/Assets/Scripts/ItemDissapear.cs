using UnityEngine;

public class ItemDissapear : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    public void ItemFound()
    {
        audioSource.PlayOneShot(audioClip);
        gameObject.SetActive(false);
    }
}
