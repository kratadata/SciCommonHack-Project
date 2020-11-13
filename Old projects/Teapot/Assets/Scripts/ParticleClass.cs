using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClass : MonoBehaviour
{

    public Properties prop;
    private Vector3 origPosition;
    void Start ()
    {

        DragParticle dp = transform.GetChild (0).gameObject.AddComponent (typeof (DragParticle)) as DragParticle;
        dp.SetParticleClass(this);
        origPosition = transform.position;

    }

    public void Respawn()
    {        
        Instantiate(this.gameObject, origPosition, Quaternion.identity);
    }

    void Update ()
    {

    }
}

public struct Properties
{
    public float spin;
    public float potato;
    public bool isBornOnFriday;

}