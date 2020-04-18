using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Civilian : ControllerBase
{
    private HotspotManager hotspotManager;
    private NavMeshAgent navMeshAgent;
    private bool roam;

    void Awake() {
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
    void OnDestroy() {
        roam = false;
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
                await Task.Delay(Random.Range(5, 11) * 1000);
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

    public override void Die (IDamageGiver damageGiver)
    {
        
    }
}
