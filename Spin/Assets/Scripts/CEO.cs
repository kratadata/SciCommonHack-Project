using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEO : MonoBehaviour
{
    private DragParticle currentDragParticle = null;

    protected static CEO instance = null;

    public GameObject branch;
    public GameObject hangingBranch;
    int i = 7;

    public static CEO Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake ()
    {
        // set the singleton instance
        instance = this;
    }
    void Start ()
    {
        InstantiateBranch(Vector3.zero, Random.Range(-20f, 20f), 10);

    }

    public void CallForSpawn(Vector3 pos)
    {
        
        if( i > 1) { 
        InstantiateBranch(pos + new Vector3(CEO.Instance.GetCurrentDragParticle().GetPosition().x, -2f, 0f), Random.Range(-20f, 20f), i);
        InstantiateBranch(pos + new Vector3(-CEO.Instance.GetCurrentDragParticle().GetPosition().x, -2f, 0f), Random.Range(-20f, 20f), i);
        
        }
        i = i - 1;

    }
  

    private void InstantiateBranch(Vector3 branchPos, float rotation, int scale)
    {
        Branch br = Instantiate(branch, branchPos, Quaternion.identity).GetComponent<Branch>();
        br.SetTargetRotation(rotation);
        br.SetTargetScale(scale);
    }

    private void InstantiateHorizontalBranch(Vector3 branchPos, float rotation)
    {
        HangingBranch Hbr = Instantiate(hangingBranch, branchPos, Quaternion.identity).GetComponent<HangingBranch>();
        Hbr.SetTargetRotationHorizontal(rotation);
    }

    public void SetCurrentDragParticle(DragParticle pc)
    {
        currentDragParticle = pc;
    }

    public DragParticle GetCurrentDragParticle()
    {
        return currentDragParticle;
    }

    void Update ()
    {

    }
}