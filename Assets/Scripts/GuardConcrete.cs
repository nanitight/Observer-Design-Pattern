using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class GuardConcrete : Guard
{
    private Prisoner prisonerToGuard;
    private NavMeshAgent guardNavMeshAgent; 

    public Transform escapeThreshold, prisonCenter, restPosition;
    private bool bringBackPrisoner = false, lockedInOnPrisoner = false;
    public override void CheckPrisonerPos()
    {
        //if prisoner is out of the bounds of the cage, restrict him. 
        Debug.Log("Taking prisoner back: "+ Vector3.Distance(escapeThreshold.position, prisonerToGuard.gameObject.transform.position));

        if (Vector3.Distance(escapeThreshold.position , prisonerToGuard.gameObject.transform.position) < 3.5f )
        {
            Debug.Log("Taking prisoner back from: "+ prisonerToGuard.gameObject.transform.position);
            bringBackPrisoner=true;
            guardNavMeshAgent.SetDestination(prisonerToGuard.gameObject.transform.position);
        }
        
    }

    private void Start()
    {
        Prisoner prisoner = FindObjectOfType<PrisonerConcrete>();
        if (prisoner != null)
        {
            prisonerToGuard= prisoner;
        }
        else
        {
            throw new NullReferenceException("prisonerToGuard is null.");
        }

        guardNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 offset = new(5f, 0, 5f);
        if (bringBackPrisoner)
        {
            guardNavMeshAgent.SetDestination(prisonerToGuard.gameObject.transform.position);
            if (Vector3.Distance(this.transform.position, prisonerToGuard.gameObject.transform.position) <= 0.5f)
            {
                lockedInOnPrisoner= true;
                prisonerToGuard.GetComponent<PrisonerConcrete>().enabled = !lockedInOnPrisoner;
                guardNavMeshAgent.SetDestination(prisonCenter.position);
                //offset *= Time.deltaTime;
                prisonerToGuard.transform.position = Vector3.MoveTowards(prisonerToGuard.transform.position, transform.position, 60f);
            }
        }

        if (Vector3.Distance(transform.position,prisonCenter.position) <= 0.5f && bringBackPrisoner)
        {
            bringBackPrisoner = false;
            guardNavMeshAgent.SetDestination(restPosition.position); 
        }

        if (Vector3.Distance(transform.position, restPosition.position) <= 0.5f && lockedInOnPrisoner )
        {
            lockedInOnPrisoner = false;
            prisonerToGuard.GetComponent<PrisonerConcrete>().enabled = !lockedInOnPrisoner;
        }

    }
}
