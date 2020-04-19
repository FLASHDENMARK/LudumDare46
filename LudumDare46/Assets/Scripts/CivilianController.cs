using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class CivilianController : ControllerBase
{
    public int MinWaitTime;
    public int MaxWaitTime;
    public float randomDeathForce = 20.0F;

    private HotspotManager hotspotManager;
    private NavMeshAgent navMeshAgent;
    private bool roam;

    void Awake()
    {
        Random.InitState(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        hotspotManager = GameObject.Find("HotspotManager").GetComponent<HotspotManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        roam = true;
        Roam();
    }

    private async void Roam() {
        while (roam) { // Roam around forever
            Hotspot nextHotspot;
            Vector3 nextPosition;

            do {
                nextHotspot = hotspotManager.Hotspots[Random.Range(0, hotspotManager.Hotspots.Count)].GetComponent<Hotspot>();
            } while (nextHotspot.TakePosition(out nextPosition) == false);

            navMeshAgent.SetDestination(nextPosition);
            bool hasReachedDestination = await HasReachedDestination(navMeshAgent);

            if (hasReachedDestination) {
                await Task.Delay(Random.Range(MinWaitTime, MaxWaitTime) * 1000);
                nextHotspot.LeavePosition(nextPosition);
            }
        }
    }

    private async Task<bool> HasReachedDestination(NavMeshAgent navMeshAgent) {
        bool hasReachedDestination = false;

        // Based on https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
        while (roam && hasReachedDestination == false) {
            if (navMeshAgent.pathPending == false) {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
                    if (navMeshAgent.hasPath == false || navMeshAgent.velocity.sqrMagnitude == 0f) {
                        hasReachedDestination = true;
                    }
                }
            }
            
            await Task.Delay(100);
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
