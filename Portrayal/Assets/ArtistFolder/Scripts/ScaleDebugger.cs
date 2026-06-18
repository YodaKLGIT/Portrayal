using UnityEngine;
using KinematicCharacterController;

public class ScaleDebugger : MonoBehaviour
{
    void Update()
    {
        Transform t = GetComponent<KinematicCharacterMotor>().transform;
        while (t != null)
        {
            if (t.localScale != Vector3.one)
                Debug.LogError($"[LOCAL] Bad scale on: {t.name} → {t.localScale}", t.gameObject);
            t = t.parent;
        }

        // Also check world scale directly (what KCC actually checks)
        Transform root = GetComponent<KinematicCharacterMotor>().transform;
        if (root.lossyScale != Vector3.one)
            Debug.LogError($"[WORLD] Lossy scale on motor object: {root.lossyScale}", root.gameObject);
    }
}