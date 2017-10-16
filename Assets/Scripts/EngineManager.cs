using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour {

  public Vector3[] enginePositions;
  public Transform enginePrefab;

  Transform[] engines;
  ParticleSystem[] engineParticles;
  ShipMovement shipMovement;

  void Awake() {
    engines = new Transform[enginePositions.Length];
    engineParticles = new ParticleSystem[enginePositions.Length];
    for(int i = 0; i < enginePositions.Length; i++)
    {
      engines[i] = Instantiate(enginePrefab, transform);
      engines[i].transform.localPosition = enginePositions[i];
      engineParticles[i] = engines[i].GetComponentInChildren<ParticleSystem>();
    }
    shipMovement = transform.GetComponentInParent<ShipMovement>();
  }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    foreach (var particles in engineParticles)
    {
      if (shipMovement.powered)
      {
        particles.Play();
      } else {
        particles.Stop();
      }
    }
	}
}
