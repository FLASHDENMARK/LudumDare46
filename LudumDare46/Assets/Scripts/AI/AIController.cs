using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : ControllerBase
{
    /// <summary>
    /// How far the AI can see the actions of the player
    /// </summary>
    public float observeDistance = 10.0F;
    public float observeAngle = 60.0F;
    public LayerMask playerLineMask;

    public int MinWaitTime;
    public int MaxWaitTime;
    public float randomDeathForce = 20.0F;
    public Pickupable pickup;
    public Transform pickupPosition;

    private HotspotManager hotspotManager;
    private NavMeshAgent _navMeshAgent;
    private bool roam;

    Hotspot nextHotspot = null;
    Vector3 nextPosition = Vector3.zero;

    public Vector3 OverridePos;
    public bool IsOverridden;

    // Start is called before the first frame update
    void Awake ()
    {
        hotspotManager = GameObject.Find("HotspotManager").GetComponent<HotspotManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        roam = true;
        StartCoroutine(Roam());
    }

    void Update ()
    {
        PlayerController playerController = GameplayManager.GetPlayer();

        DebugObserveAngle();

        bool isLookingAtPlayer = IsLookingAtPlayer(playerController);
        bool isNearPlayer = IsNearPlayer(playerController);
        bool hasLineOfSightOnPlayer = HasLineOfSight(playerController);

        if (isNearPlayer  && isLookingAtPlayer && hasLineOfSightOnPlayer)
        {
            if (playerController.inventoryManager.isWeaponEquipped)
            {
                HUD.DisplaySubtitles("You have you weapon out", "Fail", 1F);
            }

            if (playerController._interaction.isHoldingSuspiciousItem)
            {
                HUD.DisplaySubtitles($"You are carrying a {playerController._interaction.suspiciousItemName}. That's illegal!" , "Fail", 1F);
            }
        }
    }

    private bool IsLookingAtPlayer (PlayerController controller)
    {
        float angle = Utility.GetAngle(controller.transform);

        return angle <= observeAngle;
    }

    private void DebugObserveAngle ()
    {
        int skipFraction = 10;

        Color color = Color.cyan - new Color(0, 0, 0, 0.75F);
        for (int i = 0; i < observeAngle; i += skipFraction)
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(0, i, 0) * transform.forward * observeDistance, color);
            Debug.DrawRay(transform.position, Quaternion.Euler(0, -i, 0) * transform.forward * observeDistance, color);
        }
    }

    private bool IsNearPlayer(PlayerController controller)
    {
        return Vector3.Distance(transform.position, controller.transform.position) <= observeDistance;
    }

    private bool HasLineOfSight (PlayerController controller)
    {
        RaycastHit hit;

        //Debug.DrawLine(transform.position, controller.transform.position)

        bool isHit = Physics.Linecast(transform.position, controller.transform.position, out hit/*, playerLineMask*/);

        if (isHit && hit.collider.GetComponent<PlayerController>())
        {
            return true;
        }
        else
        {
            return false;
        }
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
                        _navMeshAgent.SetDestination(nextPosition);
                    }
                    yield return new WaitForSeconds(UnityEngine.Random.Range(MinWaitTime, MaxWaitTime));
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetNavMeshDestination(Vector3 position)
    {
        if (!IsDead)
        {
            _navMeshAgent.SetDestination(position);
        }
    }

    public void Pickup (Pickupable pickup)
    {
        this.pickup = pickup;

        pickup.transform.SetParent(pickupPosition.transform);
        pickup.transform.localPosition = pickupPosition.localPosition;
    }

    private bool HasReachedDestination() {
        bool hasReachedDestination = false;

        // Based on https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
        if(roam) {
            if (_navMeshAgent.pathPending == false) {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) {
                    if (_navMeshAgent.hasPath == false || _navMeshAgent.velocity.sqrMagnitude == 0f) {
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
        if (!IsDead)
        {
            IsDead = true;

            roam = false;
            GetComponent<NavMeshAgent>().enabled = false;
            this.enabled = false;

            Vector3 force = new Vector3(UnityEngine.Random.Range(-randomDeathForce, randomDeathForce), -20.0F, UnityEngine.Random.Range(-randomDeathForce, randomDeathForce));

            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            var interactable = gameObject.AddComponent<Interactable>();
            var sb = gameObject.AddComponent<SuspisiousBehavior>();

            sb.damageGiver = damageGiver.DamageGiver;
            sb.lineCast = true;
            gameObject.name = "corpse";

            interactable.isSuspicious = true;

            // This is to give a bit of randomness to the "ragdolls" however it doesn't even look that good 
            rigidbody.AddForce(force);

            // This is to avoid collision with the player, other AIs and other corpses
            // TODO If this could be done with a LayerMask instead of a string that would be cool
            gameObject.layer = LayerMask.NameToLayer("Corpse");
        }
    }
}
