using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {PLAYERONE, PLAYERTWO, END, START, NEXTTURN}

public class StateHandler : MonoBehaviour
{
    public GameState currentState;

    void Awake()
    {
        ChangeState(GameState.START);
    }

    void ChangeState(GameState newState)
    {
        currentState = newState;
        switch(currentState)
        {
            case GameState.START:
                StartGame();
                break;
            case GameState.END:
                EndGame();
                break;
            case GameState.PLAYERONE:
                StartTurn();
                break;
            case GameState.PLAYERTWO:
                StartTurn();
                break;
            case GameState.NEXTTURN:
                NextTurn();
                break;

        }
    }

    void StartGame()
    {
    }

    void EndGame()
    {

    }

    IEnumerator StartTurn()
    {
        //Do stuff at the start of turn for active player
        //Wait for the player having done their decisions and have clicked the end turn button.
        yield return new WaitForSeconds(2.0f);
        EndTurn();
    }

    IEnumerator EndTurn()
    {
        //Do stuff at the end of turn for active player
        yield return new WaitForSeconds(2.0f);
        ChangeState(GameState.NEXTTURN);
    }
    void NextTurn()
    {
        //Do stuff between turn and switch active player.
    }

}
