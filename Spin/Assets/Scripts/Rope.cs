using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
   
    [SerializeField] private Transform objectToRotate;
    [SerializeField] private bool rotateThisObject = false;
    [SerializeField] private Vector3 rotatingSpeed;
    [SerializeField] private Vector3 beginAngles;
    [SerializeField] private Vector3 endAngles;
    private Vector3 angles;

    private void Start()
    {
        angles = Vector3.zero;
        if (rotateThisObject)
        {
            objectToRotate = transform;
        }
        
    }
    private void Update()
    {
       
        // if (rotatingSpeed.z != 0.0f)
        // {
             // transform.localScale = transform.localScale;
            // angles.z = Mathf.PingPong(Time.time * rotatingSpeed.z, endAngles.z) - beginAngles.z;
            // transform.RotateAround(transform.position, Vector3.up, 20*Time.deltaTime);
        // }
            //objectToRotate.eulerAngles = angles;
    }
}

