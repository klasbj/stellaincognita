using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public enum State { Init, Update, PlayersTurn, EnemiesTurn, EndUpdate }

  public static State currentState = State.Init;

  public static float gameTime;
  public static float updateProgress;

  public GameObject player;

  const float timePerTurn = 1.0f;
  float targetTime;
  float startTime;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {

    switch (currentState) {
      case State.Init:
        currentState = State.PlayersTurn;
        break;
      case State.PlayersTurn:
        if (!player.GetComponent<PlayerControl>().active)
        {
          startTime = gameTime;
          targetTime = gameTime + timePerTurn;
          currentState = State.EnemiesTurn;
        }
        break;
      case State.EnemiesTurn:
        currentState = State.Update;
        break;
      case State.Update:
        gameTime += Time.deltaTime;
        updateProgress = (gameTime - startTime) / (targetTime - startTime);
        if (gameTime >= targetTime) {
          gameTime = targetTime;
          updateProgress = 1.0f;
          currentState = State.EndUpdate;
        }
        break;
      case State.EndUpdate:
        updateProgress = 0.0f;
        currentState = State.PlayersTurn;
        break;
    }
  }
}
