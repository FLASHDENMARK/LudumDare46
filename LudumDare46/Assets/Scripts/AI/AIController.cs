﻿using Assets.Components.Weapons;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIController : ControllerBase
{
    // Easy
    public bool isTarget = false;
    /// <summary>
    /// How far the AI can see the actions of the player
    /// </summary>
    public float observeDistance = 10.0F;
    public float observeAngle = 60.0F;
    public float alertTimeForWeapon = 1.0F;
    public float alertTimeForSuspeciousItem = 1.0F;
    public float alertTimeAfterInitialDiscovery = 3.0F;
    public LayerMask playerLineMask;

    public AudioClip[] suspecionAudio;

    public int MinWaitTime;
    public int MaxWaitTime;
    public float randomDeathForce = 20.0F;
    public List<PickupableToWeapon> weaponPickups;
    public WeaponBase currentWeapon;

    private HotspotManager hotspotManager;
    private NavMeshAgent _navMeshAgent;
    private bool roam;

    Hotspot nextHotspot = null;
    Vector3 nextPosition = Vector3.zero;

    public Vector3 OverridePos;
    public bool IsOverridden;

    private float _alertTimeForWeapon = 1.0F;
    private float _alertTimeForSuspeciousItem = 1.0F;

    // Start is called before the first frame update
    void Awake()
    {
        hotspotManager = GameObject.Find("HotspotManager").GetComponent<HotspotManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        roam = true;

        _alertTimeForWeapon = alertTimeForWeapon;
        _alertTimeForSuspeciousItem = alertTimeForSuspeciousItem;


        if (hotspotManager.RequestHotspot(out nextPosition, out nextHotspot) && UnityEngine.Random.Range(0, 4) == 1)
        {
            _navMeshAgent.SetDestination(nextPosition);
        }

        StartCoroutine(Roam());
        StartCoroutine(SlowUpdate());
    }

    private static bool _playWeaponAudio = false;
    private static bool _playItemAudio = false;

    IEnumerator SlowUpdate()
    {
        while (IsDead == false)
        {
            yield return new WaitForSeconds(0.05f);
            PlayerController playerController = GameplayManager.GetPlayer();

            if (IsNearPlayer(playerController))
            {
                if (HasLineOfSight(playerController))
                {
                    if (IsLookingAtPlayer(playerController))
                    {
                        if (playerController.inventoryManager.isWeaponEquipped)
                        {
                            alertTimeForWeapon -= 0.05f;

                            if (alertTimeForWeapon <= 0)
                            {
                                //if (!_playWeaponAudio)
                                // {
                                alertTimeForWeapon = alertTimeAfterInitialDiscovery;
                                //  _playWeaponAudio = true;
                                Utility.PlayAudio(alertedAudio, gameObject);
                                //}
                                Debug.Log(1);
                                GameplayManager.OnFailedEvent("You were caught carrying a weapon during the party...", "Don't do that... That's rude");
                            }
                            else
                            {
                                if (!_playWeaponAudio)
                                {
                                    _playWeaponAudio = true;
                                    Utility.PlayAudio(suspecionAudio, gameObject);
                                }

                                HUD.DisplaySubtitles("GAME", "Do not let them see you with that weapon for too long...", 1f);
                            }
                        }
                        else
                        {
                            _playWeaponAudio = false;
                            alertTimeForWeapon = _alertTimeForWeapon;
                        }


                        if (playerController._interaction.isHoldingSuspiciousItem)
                        {
                            _alertTimeForSuspeciousItem -= 0.05f;

                            string itemName = playerController._interaction.suspiciousItemName;

                            if (_alertTimeForSuspeciousItem <= 0)
                            {
                                // if (!_playWeaponAudio)
                                // {
                                _alertTimeForSuspeciousItem = alertTimeAfterInitialDiscovery;
                                //_playWeaponAudio = true;
                                Utility.PlayAudio(alertedAudio, gameObject);
                                //}
                                Debug.Log(2);
                                GameplayManager.OnFailedEvent("You were caught carrying a weapon during the party...", "Don't do that... That's rude");
                            }
                            else
                            {
                                if (!_playItemAudio)
                                {
                                    _playItemAudio = true;
                                    Utility.PlayAudio(suspecionAudio, gameObject);
                                }

                                HUD.DisplaySubtitles("GAME", $"Do not let them see you with that {itemName} for too long...", 0.05f * 1000f);
                            }
                        }
                        else
                        {
                            _playItemAudio = false;
                            alertTimeForSuspeciousItem = _alertTimeForSuspeciousItem;
                        }
                    }
                }
            }
        }
    }


    private bool IsLookingAtPlayer(PlayerController controller)
    {
        Vector3 LookDir = transform.forward;
        Vector3 LookAtPLayerDir = controller.transform.position - transform.position;
        float angle = Vector3.Angle(LookDir, LookAtPLayerDir);

        return angle <= observeAngle;
    }

    private void DebugObserveAngle()
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

    private bool HasLineOfSight(PlayerController controller)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, controller.transform.position - transform.position, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                return true;
            }
        }

        return false;
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
                    if (hotspotManager.RequestHotspot(out newPos, out newHotspot))
                    {

                        nextHotspot.LeavePosition(nextPosition);

                        nextPosition = newPos;
                        nextHotspot = newHotspot;

                        _navMeshAgent.SetDestination(nextPosition);

                        yield return new WaitForSeconds(UnityEngine.Random.Range(MinWaitTime, MaxWaitTime));
                    }

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

    internal void SetNavMeshSpeed(int v)
    {
        if (!IsDead)
        {
            _navMeshAgent.speed = v;
        }
    }

    public void Pickup(Pickupable pickup)
    {
        PickupableToWeapon weap = weaponPickups.FirstOrDefault(p => p.weaponType == pickup.weapon);

        if(weap != null)
        {
            currentWeapon = weap.weapon;

            if (weap == null)
            {
                throw new Exception("Could not find the weapon to pickup");
            }
            else
            {
                currentWeapon.gameObject.SetActive(true);
            }
        }

        
    }

    public void ShootWeapon()
    {
        currentWeapon.Shoot();
    }

    private bool HasReachedDestination()
    {
        bool hasReachedDestination = false;

        // Based on https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
        if (roam)
        {
            if (_navMeshAgent.pathPending == false)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (_navMeshAgent.hasPath == false || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
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

    public GameObject happyFace;
    public GameObject deadFace;
    public AudioClip[] deathSounds;
    public override void Die(IDamageGiver damageGiver)
    {
        if (!IsDead)
        {
            int i = UnityEngine.Random.Range(0, deathSounds.Length - 1);
            GetComponent<AudioSource>().PlayOneShot(deathSounds[i]);

            GameplayManager.OnControllerKilledEvent(this, damageGiver);

            happyFace.SetActive(false);
            deadFace.SetActive(true);
            IsDead = true;

            roam = false;
            GetComponent<NavMeshAgent>().enabled = false;
            this.enabled = false;

            Vector3 force = new Vector3(UnityEngine.Random.Range(-randomDeathForce, randomDeathForce), -20.0F, UnityEngine.Random.Range(-randomDeathForce, randomDeathForce));

            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            var interactable = gameObject.AddComponent<Interactable>();
            var sb = gameObject.AddComponent<SuspisiousBehavior>();
            sb.isOnlySuspicious = false;
            sb.lineCast = true;
            sb.radius = 5f;

            //sb.damageGiver = damageGiver.DamageGiver;
            //sb.lineCast = true;
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
