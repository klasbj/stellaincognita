using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingObject : MonoBehaviour {

  public GameObject parent;
  public float mass;
  public float altitude;
  public float initialPosition;
  public float position;
  public enum Direction { CW, CCW };
  public Direction direction = Direction.CCW;

  const float G = 1.0e-3f; // universal gravitational constant

  // Use this for initialization
  void Start () {

  }

  float OrbitalPosition(float time, float altitude)
  {
    float parentMass = parent.GetComponent<OrbitingObject>().mass;
    if (parentMass <= 0.0f)
    {
      parentMass = 1.0f;
    }
    float period = 2.0f * Mathf.PI * Mathf.Sqrt(altitude*altitude*altitude / (G*parentMass));
    return Mathf.Repeat(time, period) / period;
  }

  public void SetOrbit(Vector3 position, float altitude, Direction direction = Direction.CCW)
  {
    // Find the closest orbit
    var orbitals = parent.GetComponent<PlanetoidOrbitals>();
    var index = Mathf.Round((altitude - orbitals.initialAltitude) / orbitals.deltaAltitude);
    this.altitude = orbitals.initialAltitude + index * orbitals.deltaAltitude;

    this.direction = direction;

    var dvec = position - parent.transform.position;
    var angle = Mathf.Atan2(dvec.z, dvec.x);
    this.position = angle;

    float mod = OrbitalPosition(GameManager.gameTime, altitude);

    if (direction == Direction.CCW)
    {
      initialPosition = angle - 2.0f * Mathf.PI * mod;
    }
    else
    {
      initialPosition = angle + 2.0f * Mathf.PI * mod;
    }
  }

  public float PositionAt(float time, float altitude, Direction direction, float initialPosition)
  {
    float mod = OrbitalPosition(time, altitude);

    if (direction == Direction.CCW)
    {
      return initialPosition + 2.0f * Mathf.PI * mod;
    }
    return position = initialPosition - 2.0f * Mathf.PI * mod;
  }

  public float PositionAt(float time, float altitude, Direction direction)
  {
    return PositionAt(time, altitude, direction, initialPosition);
  }

  public float PositionAt(float time, float altitude)
  {
    return PositionAt(time, altitude, direction);
  }

  public float PositionAt(float time)
  {
    return PositionAt(time, altitude, direction);
  }

  public Vector3 GetWorldPosition(float angle, float altitude)
  {
    return parent.transform.position + new Vector3(altitude*Mathf.Cos(angle), 0f, altitude*Mathf.Sin(angle));
  }

  public Vector3 GetWorldPositionAt(float time, float altitude)
  {
    return GetWorldPosition(PositionAt(time-GameManager.gameTime, altitude, direction, position), altitude);
  }

  // Update is called once per frame
  void Update () {
    if (parent == null) // || GameManager.currentState != GameManager.State.Update)
    {
      return; // not orbiting or not simulating
    }

    position = PositionAt(GameManager.gameTime);

    transform.position = GetWorldPosition(position, altitude);
  }

}
