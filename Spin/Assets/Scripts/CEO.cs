using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEO : MonoBehaviour
{
    private DragParticle currentDragParticle = null;

    protected static CEO instance = null;

    public GameObject branch;

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
        InstantiateBranch(Vector3.zero, Random.Range(-90f, 90f));
    }

    public void CallForSpawn(Vector3 pos)
    {
        InstantiateBranch(pos  + new Vector3 (0f, -2f, 0f), Random.Range(-90f, 90f));
        

    } 

    private void InstantiateBranch(Vector3 branchPos, float rotation)
    {
        Branch br = Instantiate(branch, branchPos, Quaternion.identity).GetComponent<Branch>();
        br.SetTargetRotation(rotation);

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