using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public Arm arm1;
    public Arm arm2;

    public Rope ropeR;
    public Rope ropeL;

    private bool calledAction = false;
    private float smooth = 1f;
    Quaternion target;
    Vector3 scaled;

    void Start()
    {
    
    }

    public void SetTargetRotation(float targetRot)
    {
        target = Quaternion.Euler(targetRot, 0, 0);
    }


    public void SetTargetScale(float targetScale)
    {
        transform.localScale = new Vector3(targetScale, 0.1f, 0.1f);
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
