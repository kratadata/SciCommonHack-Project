﻿using UnityEngine;
using System.Collections;


public class OrbitCamera2 : MonoBehaviour
{


  public Transform _target;


  public float _distance = 20.0f;

  public float _zoomStep = 1.0f;

  public float _xSpeed = 1f;
  public float _ySpeed = 1f;


  private float _x = 0.0f;
  private float _y = 0.0f;
  private Vector3 velocity = Vector3.zero;
  private Vector3 newPos = new Vector3(8f, 8f, 16f);
  private Quaternion startRotation;
  private Quaternion currentRotation;
  private GameObject actualTarget;
  private TargetBehaviour trg;
  private bool lockedRotation = false;


    public float minPitch = -30f;
    public float maxPitch = 30f;


  private Vector3 _distanceVector;

  void Start ()
  {
    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);

    transform.position = newPos;
    startRotation = transform.rotation;

    actualTarget = new GameObject("Target");
    actualTarget.transform.position = _target.position;
    trg = actualTarget.AddComponent<TargetBehaviour>();
    trg.orb = this;
    this.Rotate(_x, _y);
   
    }

    /**
     * Rotate the camera or zoom depending on the input of the player.
     */
    void LateUpdate()
  {
        currentRotation = transform.rotation;

        if ( _target && actualTarget)
    {
      if (!lockedRotation)
      {
        this.RotateControls();
      }
      this.Zoom();


      transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, 0.2f);
      transform.LookAt(actualTarget.transform);
    }
           
      if (currentRotation != startRotation)
        {
            Invoke("GoToTargetRotation", 3.0f);
        }
        startRotation = currentRotation;
    }

  /**
   * Rotate the camera when the first button of the mouse is pressed.
   *
   */
  void RotateControls()
  {
    if ( Input.GetButton("Fire1") )
    {
      _x += Input.GetAxis("Mouse X") * _xSpeed;
      _y += -Input.GetAxis("Mouse Y") * _ySpeed;

      this.Rotate(_x, _y);
           
     }

    if (Input.GetKey("d"))
    {
      _x -= Time.deltaTime * _xSpeed * 16f;
      this.Rotate(_x, _y);
           
    }

    if (Input.GetKey("a"))
    {
      _x += Time.deltaTime * _xSpeed * 16f;
      this.Rotate(_x, _y);
            
        }

    if (Input.GetKey("s"))
    {
      _y -= Time.deltaTime * _ySpeed * 16f;
      this.Rotate(_x, _y);
         
        }

    if (Input.GetKey("w"))
    {
      _y += Time.deltaTime * _ySpeed * 16f;
      this.Rotate(_x, _y);
            
        }

    if (Input.GetKey("."))
    {
      GoToTargetRotation();
           
        }

  }

    void Rotate( float x, float y )
  {
        //Transform angle in degree in quaternion form used by Unity for rotation.

    y = Mathf.Clamp(y, minPitch, maxPitch);
    x = Mathf.Clamp(x, minPitch, maxPitch);
    Quaternion rotation = Quaternion.Euler(y, x, 0.0f);

    newPos = rotation * _distanceVector  + actualTarget.transform.position;
    }

    private void ChangeTargetPosition(Vector3 tPos)
  {
    trg.GoToPosSmooth(tPos);
  }

  public void ResetDistance(Vector3 newTargetPos)
  {
    _distance = Vector3.Magnitude(newTargetPos - transform.position);
  }

    void Zoom()
  {
    if ( Input.GetAxis("Mouse ScrollWheel") < 0.0f )
    {

      this.ZoomOut();
            
        }
    else if ( Input.GetAxis("Mouse ScrollWheel") > 0.0f )
    {
      this.ZoomIn();
    }

    if (Input.GetKey("e"))
    {
      this.ZoomInKey();
    }

    if (Input.GetKey("q"))
    {
      this.ZoomOutKey();
    }

  }



  /**
   * Reduce the distance from the camera to the target and
   * update the position of the camera (with the Rotate function).
   */
  void ZoomIn()
  {
    if (_distance > 10.0f) { 
    _distance -= _zoomStep;
    
    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
    this.Rotate(_x, _y);
        }
    }

    void ZoomInKey()
    {
        if (_distance > 10.0f) { 
        _distance -= _zoomStep * Time.deltaTime * 3f;
        _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
        this.Rotate(_x, _y);
    }
  }

  /**
   * Increase the distance from the camera to the target and
   * update the position of the camera (with the Rotate function).
   */
  void ZoomOut()
  {
    if (_distance < 30.0f) { 
    _distance += _zoomStep;
 
    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
    this.Rotate(_x, _y);
    }
}

  void ZoomOutKey()
  {
    if (_distance < 30.0f)
    {
        _distance += _zoomStep * Time.deltaTime * 3f;

        _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
        this.Rotate(_x, _y);
        //Debug.Log(_distance);
    }
  }

  public void GoToTargetRotation()
  {
    _x = 0f;
    _y = 0f;
    _distance = 20f;
    _distanceVector = new Vector3(Mathf.Lerp(10.0f, 0.0f, 20.0f), Mathf.Lerp(10.0f, 0.0f, 20.0f), -_distance);
    Rotate(_x, _y);
     CancelInvoke("GoToTargetRotation");
    }

 public void GoToRotationDistanceSmooth(float x, float y, float distance, float time) {
 
     StartCoroutine(IGoToRotationDistanceSmooth(x, y, distance, time));
    
  }

  IEnumerator IGoToRotationDistanceSmooth(float x, float y, float distance, float aTime)
  {

    float origX = _x - 360f;
    float origY = _y;
    float origDist = _distance;

    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
    {
   
        _x = Mathf.SmoothStep(origX , x, t);
        _y = Mathf.SmoothStep(origY,  y, t);
        _distance = Mathf.SmoothStep(distance, 20.0f, t);
        _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
        Rotate(_x, _y);
      }
        
        yield return null;
  }

  public void SetLockRotation(bool lr)
  {
    lockedRotation = lr;
  }

} //End class