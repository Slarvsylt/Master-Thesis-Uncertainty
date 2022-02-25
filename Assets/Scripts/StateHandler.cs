using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {PLAYERTURN, END, START, NEXTTURN, ENDTURN, STARTTURN}

public class StateHandler : MonoBehaviour
{
    public GameState currentState;
    public Player currentPlayer;
    public Player inactivePlayer;

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
            case GameState.PLAYERTURN:
                StartTurn();
                break;
            case GameState.NEXTTURN:
                NextTurn();
                break;

        }
    }

    void StartGame()
    {
        StartCoroutine(GameSystem.gameSystem.StartGame());
        //Populate with units
    }

    void EndGame()
    {
        StartCoroutine(GameSystem.gameSystem.EndGame());
        //End Game
        //Display winner
    }

    IEnumerator StartTurn()
    {
        //Remove defend
        //Do stuff at the start of turn for active player
        //Wait for the player having done their decisions and have clicked the end turn button.
        StartCoroutine(GameSystem.gameSystem.StartOfturnEffects());
        StartCoroutine(currentPlayer.ChooseAction());
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator EndTurn()
    {
        //Do stuff at the end of turn for active player
        StartCoroutine(GameSystem.gameSystem.EndOfTurnEffects());
        yield return new WaitForSeconds(2.0f);
        ChangeState(GameState.NEXTTURN);
    }
    void NextTurn()
    {
        Player tmp = currentPlayer;
        currentPlayer = inactivePlayer;
        inactivePlayer = currentPlayer;
        ChangeState(GameState.PLAYERTURN);
        //Do stuff between turn and switch active player.
        //Switch active Player.
        //re-render sprites to indicate player switch.
    }

}
