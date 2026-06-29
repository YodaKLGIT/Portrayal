using UnityEngine;

public class WeepingAngelChecker : MonoBehaviour
{
    [SerializeField] private GameObject Angel;
    [SerializeField] private GameObject Spawn;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<WeepingAngel>() != null)
        {
            transform.position = Vector3.zero;
        }
    }
}
