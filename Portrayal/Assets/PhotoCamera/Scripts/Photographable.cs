using UnityEngine;
using UnityEngine.Events;

public class Photographable : MonoBehaviour
{
    [Header("ID")]
    public string objectID;

    [Header("Capture Settings")]
    public float requiredCenterAccuracy = 0.2f;
    public float maxDistance = 25f;

    [Header("Gallery")]
    public bool saveToGallery = true;

    [Header("Events")]
    public UnityEvent onCaptured;

    private bool alreadyCaptured = false;

    public void Capture()
    {
        if (alreadyCaptured) return;

        alreadyCaptured = true;

        Debug.Log("Captured: " + objectID);
        onCaptured?.Invoke();
    }

    public bool IsCaptured()
    {
        return alreadyCaptured;
    }

    public bool ShouldSaveToGallery()
    {
        return saveToGallery;
    }
}