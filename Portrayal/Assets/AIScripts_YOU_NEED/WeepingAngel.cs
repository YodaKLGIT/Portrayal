using UnityEngine;
using UnityEngine.AI;

public class WeepingAngel : AngelChecker
{
    [SerializeField] private Camera PlayerCamera;
    private NavMeshAgent agent;
    public GameObject AngelTarget;
    [SerializeField] private GameObject StartingPoint;
    private bool stage2 = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            stage2 = !stage2;
        }
    }

    public void AngelsAwake()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(PlayerCamera);
        bool inView = GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds);

        if (inView)
        {
         
            agent.isStopped = true;
            agent.velocity = Vector3.zero; 
        }
        else
        {
            agent.isStopped = false;
            agent.speed = 3f;

            if (stage2)
                Moving();
            else
                ReturnToSpawn();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = Vector3.zero;
        }
    }
    void Moving()
    {
        agent.SetDestination(AngelTarget.transform.position);
    }

    void ReturnToSpawn()
    {
        agent.SetDestination(StartingPoint.transform.position);
    }
}