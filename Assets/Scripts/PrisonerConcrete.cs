using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerConcrete : Prisoner
{
    public float _rotationSpeed = 100f , _movementSpeed = 10 , maxDistance = 0.05f;
    Rigidbody _rigidbody;
    Guard[] guardList; 
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        guardList = FindObjectsOfType<GuardConcrete>();
        if (guardList != null && guardList.Length>0 )
        {
            //Debug.Log("Gof->: Found Guards");
            foreach( Guard guard in guardList )
            {
                //Debug.Log("Gof->: Guard Found"+ guard);
                AttachGuard( guard );
            }
        }
        else
        {
            //Debug.Log("Gof->: No Guards Found "+guardList);

        }
    }

    // Update is called once per frame
  

    private void OnDestroy()
    {
        if (guardList != null && guardList.Length>0)
        {
            foreach(Guard guard in guardList)
            {
                DetachGuard( guard );
            }
        }
        else
        {
            //Debug.Log("Gof->: No guards to detach");
        }

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow)) { 
            transform.Translate(_movementSpeed * Time.deltaTime * -Vector3.forward);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(_movementSpeed * Time.deltaTime * Vector3.forward);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(_rotationSpeed * Time.deltaTime * -transform.up);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(_rotationSpeed * Time.deltaTime * transform.up);

        }

    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public void NotifyGuards()
    {
       base.Notify();
    }
}
