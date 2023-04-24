using System.Collections.Generic;
using UnityEngine;

abstract public class Prisoner : MonoBehaviour
{
    private List<Guard> guardConcreteList = new List<Guard>(); 

    public void AttachGuard(Guard guardToAattach)
    {
        //Debug.Log("Gof->: Attach");

        guardConcreteList.Add(guardToAattach);
    }

    public bool DetachGuard(Guard guardToDetach)
    {
        //Debug.Log("Gof->: Detach");

        int i = guardConcreteList.FindIndex(x => x == guardToDetach);
        if (i == -1)
        {
            return false;
        }
        else
        {
            guardConcreteList.RemoveAt(i);
            return true;
        }
    }

    protected virtual void Notify()
    {
        foreach (Guard guard in guardConcreteList)
        {
            guard.CheckPrisonerPos();
        }
    }
}

