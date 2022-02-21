using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {PLAYERONE, PLAYERTWO, END, START}

public class StateHandler : MonoBehaviour
{
    public GameState currentState;


    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        currentState = GameState.START;
    }

    void EndGame()
    {
        currentState = GameState.END;
    }



}
