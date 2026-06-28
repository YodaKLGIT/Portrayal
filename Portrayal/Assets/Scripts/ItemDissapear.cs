using UnityEngine;

public class ItemDissapear : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject ExtraObject;

    public void ItemFound()
    {
        audioSource.PlayOneShot(audioClip);
        if(ExtraObject != null)
        {
            ExtraObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
