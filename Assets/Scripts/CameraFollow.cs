using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

  public Transform target;
  public float smoothing = 5.0f;

  Vector3 offset;

  void Awake() {
    // use the camera setting relative (0,0,0) as the offset
    offset = transform.position;
  }
	
	// Update is called once per frame
	void Update () {
    if (target) {
      transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
    }
	}
}
