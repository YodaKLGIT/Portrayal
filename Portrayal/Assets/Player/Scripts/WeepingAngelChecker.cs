using UnityEngine;

public class WeepingAngelChecker : MonoBehaviour
{
    [SerializeField] private GameObject Angel;
    [SerializeField] private GameObject Spawn;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Angel)
        {
            Vector3 new_position = transform.position;
            new_position.x = Spawn.transform.position.x;
            new_position.z = Spawn.transform.position.z;

        }
    }
}
