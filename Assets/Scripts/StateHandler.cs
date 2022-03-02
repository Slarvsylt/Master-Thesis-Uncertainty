using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {PLAYERTURN, END, START, NEXTTURN, ENDTURN, STARTTURN}

public class StateHandler : MonoBehaviour
{
    public static StateHandler stateHandler;
    public GameState currentState;
    public Player currentPlayer;
    public Player inactivePlayer;

    void Awake()
    {

    }

    private void Start()
    {
        ChangeState(GameState.START);
    }

    public void ChangeState(GameState newState)
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

    private void pickStartPlayer()
    {
        float random = Random.value;
        if(random >= 0.5)
        {
            currentPlayer = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
            inactivePlayer = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        }
        else
        {
            inactivePlayer = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
            currentPlayer = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        }
    }

    void StartGame()
    {
        pickStartPlayer();
        inactivePlayer.gameObject.SetActive(false);
        currentPlayer.gameObject.SetActive(true);
        ChangeState(GameState.PLAYERTURN);
        //StartCoroutine(GameSystem.gameSystem.StartGame());
        //Populate with units
    }

    void EndGame()
    {
        //StartCoroutine(GameSystem.gameSystem.EndGame());
        //End Game
        //Display winner
    }

    private void StartTurn()
    {
        //Remove defend
        //Do stuff at the start of turn for active player
        //Wait for the player having done their decisions and have clicked the end turn button.
       // Debug.Log("Starting new turn and populating field :" + currentPlayer.gameObject.name);
        currentPlayer.PopulateField();
        //StartCoroutine(GameSystem.gameSystem.StartOfturnEffects());
        //StartCoroutine(currentPlayer.ChooseAction());
 
    }

    private void EndTurn()
    {
        //Do stuff at the end of turn for active player
        //StartCoroutine(GameSystem.gameSystem.EndOfTurnEffects());
        ChangeState(GameState.NEXTTURN);
    }

    void NextTurn()
    {
       // Debug.Log("Switching players, current player: " + currentPlayer.gameObject.name);
        Player tmp = currentPlayer;
        currentPlayer = inactivePlayer;
        inactivePlayer = tmp;
       // Debug.Log("Switching players, current player: " + currentPlayer.gameObject.name);
     //   Debug.Log("tmp " + tmp.gameObject.name);
        inactivePlayer.gameObject.SetActive(false);
        Debug.Log("Deactivated " + inactivePlayer.gameObject.name);
        currentPlayer.gameObject.SetActive(true);
        Debug.Log("Activated " + currentPlayer.gameObject.name);
        //   Debug.Log("Switched players, current player now: " + currentPlayer.gameObject.name);
        ChangeState(GameState.PLAYERTURN);

        //Do stuff between turn and switch active player.
        //Switch active Player.
        //re-render sprites to indicate player switch.
    }

}
