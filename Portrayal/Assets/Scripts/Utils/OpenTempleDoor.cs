using System.Collections;
using UnityEngine;

public class OpenTempleDoor : MonoBehaviour
{

    bool isOpen = false;

    [SerializeField] private bool objectOneActive = false;
    [SerializeField] private bool objectTwoActive = false;
    [SerializeField] private bool objectThreeActive = false;

    [SerializeField] private GameObject door;
    [SerializeField] private GameObject CutsceneCam;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private PhotoCapture photoCapture;
    [SerializeField] private GameObject npc;

    void Update()
    {
        CanDoorOpen();
    }

    private void CanDoorOpen()
    {
        if (objectOneActive && objectTwoActive && objectThreeActive && !isOpen)
        {
            isOpen = true;
            npc.SetActive(true);
            SlideDoor();
        }
    }

    private void SlideDoor()
    {
        StartCoroutine(SlideDoorRoutine());
    }

    private IEnumerator SlideDoorRoutine()
    {
        // freeze player object
        playerCamera.SetSensitivity(0f);
        photoCapture.RemovePhoto();

        yield return new WaitForSeconds(1f);
        CutsceneCam.SetActive(true);

        Vector3 startPosition = door.transform.localPosition;
        Vector3 targetPosition = startPosition + new Vector3(0f, 0f, 3.4f);

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            door.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        door.transform.localPosition = targetPosition;

        yield return new WaitForSeconds(1f);

        Vector3 npcStartPosition = npc.transform.position;
        Vector3 npcTargetPosition = new Vector3(-3.40f, 2.60f, -40.26f); ;
        

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            npc.transform.position = Vector3.Lerp(
                npcStartPosition,
                npcTargetPosition,
                elapsed / duration);

            yield return null;
        }

        npc.transform.position = npcTargetPosition;

        // Keep the cutscene camera active for 2 more seconds
        yield return new WaitForSeconds(2f);

        CutsceneCam.SetActive(false);
        playerCamera.ResetSensitivity();
    }

    public void SetObjectOneActive()
    {
        objectOneActive = true;
    }
    public void SetObjectTwoActive()
    {
        objectTwoActive = true;
    }
    public void SetObjectThreeActive()
    {
        objectThreeActive = true;
    }
}
