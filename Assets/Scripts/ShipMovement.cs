using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {


  Quaternion? startRot;
  Vector3? startPos;
  Vector3? target;
  float targetAltitude;

  public bool rotating, powered;

  OrbitingObject orbit;
  ShipRotation shipRotate;

  public void SetTarget(Vector3 point, float altitude)
  {
    target = point;
    targetAltitude = altitude;
  }

  // Use this for initialization
  void Start () {
    orbit = GetComponent<OrbitingObject>();
    shipRotate = GetComponent<ShipRotation>();
    rotating = false;
    powered = false;
  }

  // Update is called once per frame
  void Update () {
    rotating = false;
    powered = false;
    if (GameManager.currentState == GameManager.State.Update)
    {
      if (target.HasValue)
      {
        shipRotate.enabled = false;
        orbit.enabled = false;
        if (!startRot.HasValue) {
          startRot = transform.rotation;
        }
        transform.LookAt(target.Value);
        transform.rotation = Quaternion.Slerp(startRot.Value, transform.rotation, GameManager.updateProgress*3);

        rotating = GameManager.updateProgress*3 < 1.0;


        if (!startPos.HasValue) {
          startPos = transform.position;
        }
        const float movementStart = .25f;
        if (GameManager.updateProgress > movementStart)
        {
          transform.position = Vector3.Lerp(startPos.Value, target.Value, (GameManager.updateProgress - movementStart)/(1.0f-movementStart));
          powered = GameManager.updateProgress < 1.0;
        }
      }
      else
      {
        shipRotate.enabled = true;
        orbit.enabled = true;
      }
    }
    else if (GameManager.currentState == GameManager.State.EndUpdate)
    {
      if (target.HasValue)
      {
        orbit.SetOrbit(target.Value, targetAltitude);
        target = null;
        startRot = null;
        startPos = null;
        targetAltitude = 0f;
      }

      shipRotate.enabled = true;
      orbit.enabled = true;
    }
  }
}
