using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalLine : MonoBehaviour {

  public int resolution = 100;
  public float altitude = 2f;


  LineRenderer line;

	void Awake()
  {
    line = GetComponent<LineRenderer>();
	}
	
  void Start()
  {
    line.positionCount = resolution;

    line.loop = true;
    var circle = new Vector3[resolution];
    for (var i = 0; i < resolution; i++) {
      float angle = i * 2.0f*Mathf.PI / resolution;
      var pos = new Vector3(altitude*Mathf.Cos(angle), 0.0f, altitude*Mathf.Sin(angle));
      circle[i] = transform.parent.position + pos;
    }

    line.SetPositions(circle);
  }

  void Update()
  {
  }
}
