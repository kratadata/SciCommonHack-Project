using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{

    private bool isFilled = false;

    private ParticleClass pc = null;

    private float snappingDist = 1f;

    void Start ()
    {

    }

    public bool GetIsFilled()
    {
        return isFilled;
    }

    public void SetParticleClass(DragParticle dp)
    {
        pc = dp.GetParticleClass();
        //Debug.Log(pc);
    }



    public ParticleClass GetParticle()
    {
        return pc;
    }

    void Update ()
    {
        if (!isFilled && CEO.Instance.GetCurrentDragParticle () != null)
        {
            float dist = (CEO.Instance.GetCurrentDragParticle ().GetPosition () - transform.position).magnitude;
            if (dist <= snappingDist) 
            {
                CEO.Instance.GetCurrentDragParticle (). LockAtPosition (transform.position);
                isFilled = true;
                SetParticleClass(CEO.Instance.GetCurrentDragParticle ());
                //Debug.Log(isFilled);
            }                 
        }
    }
}