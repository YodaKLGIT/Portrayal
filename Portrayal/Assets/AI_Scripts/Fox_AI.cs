using UnityEngine;
using UnityEngine.AI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject FoxTarget;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(FoxTarget.transform.position);
    }

    //if(fox has been foto taken)
    //{
    //  FoxCaught();
    //}
}
