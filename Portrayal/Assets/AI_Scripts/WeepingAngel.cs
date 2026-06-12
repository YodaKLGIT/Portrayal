using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class WeepingAngel : AngelChecker
{
    [SerializeField] private Camera PlayerCamera;
    private NavMeshAgent agent;
    public GameObject AngelTarget;  
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(PlayerCamera);
        if(GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            agent.speed = 0f;
        }
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            agent.speed = 3f;
            Moving();
        }

    }

    void Moving()
    {
        agent.SetDestination(AngelTarget.transform.position);

    }

}
