using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

  public Transform indicatorPrefab;

  public bool active;
  public Vector3? target;
  public float targetAltitude;

  Movements? selectedMovement;

  enum Movements { Forward=0, OutForward, OutBackward, Backward, InBackward, InForward }

  const string indicators = "huikmn";
  readonly float[] dt = { 10, 8, -8, -10, -5, 5 };
  readonly float[] da = { 0f, .5f, .5f, 0f, -.5f, -.5f };
  readonly string[] controls = {"Forward", "OutForward", "OutBackward", "Backward", "InBackward", "InForward"};
  Transform[] movementIndicators;
  OrbitingObject orbit;
  ShipMovement ship;

  // Use this for initialization
  void Start () {
    movementIndicators = new Transform[(int)Movements.InForward + 1];
    foreach (Movements m in System.Enum.GetValues(typeof(Movements)))
    {
      movementIndicators[(int)m] = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
      movementIndicators[(int)m].GetComponentInChildren<Text>().text = indicators[(int)m].ToString();
    }
    orbit = GetComponent<OrbitingObject>();
    ship = GetComponent<ShipMovement>();
  }

  // Update is called once per frame
  void Update () {
    if (GameManager.currentState != GameManager.State.PlayersTurn)
    {
      foreach (Movements m in System.Enum.GetValues(typeof(Movements)))
      {
        int i = (int)m;
        movementIndicators[i].gameObject.SetActive(selectedMovement.HasValue && selectedMovement.Value == m);
      }
    }
    else
    {
      active = true;
      selectedMovement = null;
      foreach (Movements m in System.Enum.GetValues(typeof(Movements)))
      {
        int i = (int)m;
        bool valid = orbit.altitude + da[i] >= orbit.parent.GetComponent<PlanetoidOrbitals>().initialAltitude;
        if (valid && Input.GetButton(controls[i])) {
          selectedMovement = m;
          ship.SetTarget(orbit.GetWorldPositionAt(GameManager.gameTime + dt[i], orbit.altitude + da[i]), orbit.altitude+da[i]);
          active = false;
          break;
        }
      }
      foreach (Movements m in System.Enum.GetValues(typeof(Movements)))
      {
        int i = (int)m;
        bool valid = orbit.altitude + da[i] >= orbit.parent.GetComponent<PlanetoidOrbitals>().initialAltitude;
        movementIndicators[i].gameObject.SetActive(valid && (!selectedMovement.HasValue || selectedMovement.Value == m));
        var p = orbit.GetWorldPositionAt(GameManager.gameTime + dt[i], orbit.altitude + da[i]);
        movementIndicators[i].position = p;
      }

      if (Input.GetButton("Scan"))
      {
        active = false;
        // TBD do other stuff
      }

    }
  }
}
