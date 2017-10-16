using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetoidOrbitals : MonoBehaviour {

  public int orbitalCount = 5;
  public float deltaAltitude = 0.5f;
  public float initialAltitude = 1.0f;
  public GameObject orbitalPrefab;

  void Awake() {
  }

	// Use this for initialization
	void Start () {
    for (int i = 0; i < orbitalCount; i++) {
      var nl = Instantiate(orbitalPrefab, Vector3.zero, Quaternion.identity, transform);
      var ol = nl.GetComponent<OrbitalLine>();
      ol.altitude = initialAltitude + deltaAltitude * i;
    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
