using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


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

    public Texture branchTexture;
    private Material mat;


    // public Texture branchTexture;
    // private Material mat;


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

        GetTexture();

        mat = gameObject.GetComponent<Renderer>().material;
        mat.mainTexture = branchTexture;


        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);  
        if (!calledAction && arm1.GetIsFilled() && arm2.GetIsFilled())
        {
            Action();
            calledAction = true;


        }    
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://phys.cam/qa/texbranch.png");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            branchTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }

    }

}
