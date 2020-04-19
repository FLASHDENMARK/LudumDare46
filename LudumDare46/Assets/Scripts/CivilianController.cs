using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class CivilianController : ControllerBase
{
    public int MinWaitTime;
    public int MaxWaitTime;
    public float randomDeathForce = 20.0F;

    private HotspotManager hotspotManager;
    public NavMeshAgent NavMeshAgent;
    private bool roam;

    Hotspot nextHotspot = null;
    Vector3 nextPosition = Vector3.zero;

    public Vector3 OverridePos;
    public bool IsOverridden;

    void Awake()
    {
        Random.InitState(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        hotspotManager = GameObject.Find("HotspotManager").GetComponent<HotspotManager>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        roam = true;
        StartCoroutine(Roam());
    }

    IEnumerator Roam()
    {
        while (roam)
        { // Roam around forever

            if (IsOverridden == false)
            {

                if (HasReachedDestination())
                {
                    Vector3 newPos;
                    Hotspot newHotspot;
                    if(hotspotManager.RequestHotspot(out newPos, out newHotspot))
                    {
                        if(nextHotspot != null) { 
                            nextHotspot.LeavePosition(nextPosition);
                        }

                        nextPosition = newPos;
                        nextHotspot = newHotspot;
                        NavMeshAgent.SetDestination(nextPosition);
                    }
                    yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private bool HasReachedDestination() {
        bool hasReachedDestination = false;

        // Based on https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
        if(roam) {
            if (NavMeshAgent.pathPending == false) {
                if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance) {
                    if (NavMeshAgent.hasPath == false || NavMeshAgent.velocity.sqrMagnitude == 0f) {
                        hasReachedDestination = true;
                    }
                }
            }
            
        }

        return hasReachedDestination;
    }

    private void OnDestroy()
    {
        roam = false;
    }

    public override void Die (IDamageGiver damageGiver)
    {
        roam = false;
        GetComponent<NavMeshAgent>().enabled = false;
        this.enabled = false;

        Vector3 force = new Vector3(Random.Range(-randomDeathForce, randomDeathForce), -20.0F, Random.Range(-randomDeathForce, randomDeathForce));

        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();

        // This is to give a bit of randomness to the "ragdolls" however it doesn't even look that good 
        rigidbody.AddForce(force);

        // This is to avoid collision with the player, other AIs and other corpses
        // TODO If this could be done with a LayerMask instead of a string that would be cool
        gameObject.layer = LayerMask.NameToLayer("Corpse");
    }
}
