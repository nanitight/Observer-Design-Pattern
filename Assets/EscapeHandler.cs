using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Some collision happened: "+other.gameObject.name);
        if (other.gameObject.CompareTag("Escape"))
        {
            GetComponent<PrisonerConcrete>().NotifyGuards();
        }
    }
}
