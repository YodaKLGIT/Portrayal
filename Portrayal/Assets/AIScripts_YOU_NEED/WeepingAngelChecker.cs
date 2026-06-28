using UnityEngine;

public class WeepingAngelChecker : MonoBehaviour
{
    [SerializeField] private GameObject Angel;
    [SerializeField] private GameObject Spawn;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Angel)
        {
            transform.position = new Vector3(0, 0, 0);

        }
    }
}
