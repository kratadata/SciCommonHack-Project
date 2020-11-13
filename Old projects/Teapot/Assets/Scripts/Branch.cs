using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public Arm arm1;
    public Arm arm2;

    private bool calledAction = false;
    private float smooth = 1f;
    Quaternion target;

    void Start()
    {
        
    }
    public void SetTargetRotation(float targetRot)
    {
        target = Quaternion.Euler(0f, targetRot, 0);
    }

    public void Action()
    {
        //Debug.Log("ACTION!!!");
        CEO.Instance.CallForSpawn(transform.position);
    }

       void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);  
        if (!calledAction && arm1.GetIsFilled() && arm2.GetIsFilled())
        {
            Action();
            calledAction = true;


        }    
    }
}
