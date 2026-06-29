using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    [SerializeField] private Material newMat;

    public void ChangeMaterial()
    {
        // get gameobject and change mat to newMat
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.material = newMat;
        }
    }
}
