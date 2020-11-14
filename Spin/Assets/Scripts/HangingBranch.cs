using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingBranch : MonoBehaviour
{
 
    private bool calledAction = false;
    Quaternion target;

    void Start()
    {

    }
   
    public void Action()
    {
        //Debug.Log("ACTION!!!");
        CEO.Instance.CallForSpawn(transform.position);
    }

    public void SetTargetRotationHorizontal(float targetRot)
    {
        target = Quaternion.Euler(0, 0, targetRot);
        transform.rotation = target;
    }

    void Update()
    {

        if (!calledAction)
        {
            calledAction = true;
            Action();
            
        }
       
    }
}
