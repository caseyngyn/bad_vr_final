
using UnityEngine;
using UnityEngine.AI;
//req component type of rigid body
// req component typr of nav mesh agent
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class AiController : MonoBehaviour
{
    public NavMeshAgent agent;
    [Range(0,100)] public float speed;
    [Range(0, 100)] public float walkRadius;

    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }
    }


    // Update is called once per frame
    void Update()
    {
        FaceTarget();

        if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }
    }

    void FaceTarget()
    {
        var turnTowardNavSteeringTarget = agent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPos = Vector3.zero;
        Vector3 randpos = Random.insideUnitSphere * walkRadius;
        randpos += transform.position;
        if (NavMesh.SamplePosition(randpos, out NavMeshHit hit, walkRadius, 1))
        {
            finalPos = hit.position;
        }
        return finalPos;
    }

}
