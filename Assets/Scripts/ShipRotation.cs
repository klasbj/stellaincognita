using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotation : MonoBehaviour {

  OrbitingObject orbit;

	// Use this for initialization
	void Start () {
		orbit = GetComponent<OrbitingObject>();
	}
	
	// Update is called once per frame
	void Update () {
    if (GameManager.currentState == GameManager.State.Update)
    {
      float angle = -orbit.position*180f/Mathf.PI;
      if (orbit.direction == OrbitingObject.Direction.CW) {
        angle += 180;
      }
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), 1.0f*Time.deltaTime);
    }
	}
}
