using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    //Just for debugging purposes.

    [SerializeField] private string currentState;

    public Path path;
    private GameObject player;
    public float sightDistance = 20f;
    public float fielOfView = 85f;
    public float eyeHeight;
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            //is the player close enough to be seen?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fielOfView && angleToPlayer <= fielOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction.normalized * sightDistance, Color.green);
                            return true;
                        }
                    }
                    

                }
            }
            return false;
        }
        return true;
    }
}
