using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;

public class WeepingAngel : AngelChecker
{
    [SerializeField] private Camera PlayerCamera;
    private NavMeshAgent agent;
    public GameObject AngelTarget;
    [SerializeField] private GameObject StartingPoint;
    private bool stage2 = true;
    private bool Smoving;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    { 

        if (Input.GetKey(KeyCode.J))
        {
            if (stage2 == true)
            {
                stage2 = false;
            }
            else
            {
                stage2 = true;
            }
        }
    }
    public void AngelsAwake()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(PlayerCamera);
        if (GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            agent.speed = 0f;
            Smoving = false;
        }
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            agent.speed = 3f;
            if (stage2 == true)
            {
                Moving();
            }
            else
            {
                ReturnToSpawn();
            }
        }
    }
    void Moving()
    {
        agent.SetDestination(AngelTarget.transform.position);
        Smoving = true;
    }

    void ReturnToSpawn()
    {
        agent.SetDestination(StartingPoint.transform.position);
    }
   
}

//Make 2 triggers one for if the angel touches the player the other too make sur4e4 they don't leave the Maze.
