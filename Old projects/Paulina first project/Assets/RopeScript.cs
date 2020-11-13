using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RopeScript : MonoBehaviour
{

    public GameObject RopeBottom; //this is the player
    public GameObject RopeTop; //this is an empty object that anchors the rope

    private Vector3 newZ;
    public float shiftCenter;
    Color c1 = Color.white;
    Color c2 = new Color(1, 1, 1, 0);


    void Start()
    {

        newZ = new Vector3(RopeTop.transform.localPosition.x, RopeTop.transform.localPosition.y, RopeTop.transform.localPosition.z + shiftCenter);


        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, RopeBottom.transform.localPosition);
        lineRenderer.SetPosition(1, newZ);
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(c1, c2);
    }

    void Update()
    {

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, RopeBottom.transform.localPosition);
        lineRenderer.SetPosition(1, newZ);

    }

}