using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragParticle : MonoBehaviour
{

    Vector3 dist;
    float posx;
    float posy;
    private bool isFree = true;
    private bool isLocking = false;
    private Vector3 lockPosition = Vector3.zero;
    private Material mat;
    private float snapDistance = 0.6f;
    private Color freeColor = Color.red;
    private Color snappedColor = Color.green;
    private ParticleClass pc;

    void Start ()
    {
        mat = gameObject.GetComponent<Renderer> ().material;
        mat.color = freeColor;
    }

    public void SetParticleClass(ParticleClass paclass)
    {
        pc = paclass;
    }

    void OnMouseDown ()
    {
        if (isFree)
        {
            dist = Camera.main.WorldToScreenPoint (transform.position);
            posx = Input.mousePosition.x - dist.x;
            posy = Input.mousePosition.y - dist.y;
        }
    }

    void OnMouseDrag ()
    {
        if (isFree)
        {
            Vector3 curPos = new Vector3 (Input.mousePosition.x - posx, Input.mousePosition.y - posy, dist.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint (curPos);

            transform.parent.position = worldPos;
            CEO.Instance.SetCurrentDragParticle (this);
        }
    }

    public Vector3 GetPosition ()
    {
        return transform.position;
    }

    public void LockAtPosition (Vector3 lpos)
    {
        lockPosition = lpos;
        //transform.parent.position = lpos;
        Lock ();
    }

    public void Lock ()
    {
        isFree = false;
        isLocking = true;
        //gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
    }

    public void SnappingMovement ()
    {
        transform.parent.position = Vector3.Lerp (transform.parent.position, lockPosition, 0.01f);
        float dist = (transform.parent.position - lockPosition).magnitude;
        mat.color = Color.Lerp (freeColor, snappedColor, (snapDistance - dist) / snapDistance);

    }

    public ParticleClass GetParticleClass ()
    {
        return pc;
    }    

    void Update ()
    {
        if (isLocking)
        {
            SnappingMovement ();
            float dist = (transform.parent.position - lockPosition).magnitude;
            if (dist <= 0.02f)
            {
                isLocking = false;
                transform.parent.position = lockPosition;
                mat.color = snappedColor;
                pc.Respawn();

            }

        }

    }

}