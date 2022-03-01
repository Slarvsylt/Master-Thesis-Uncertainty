using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    static bool isPressedAction = false;

    public Player currentPlayer;

    // for setting up specific battle option
    public BattleMenuOption battleMenuOption;

    void Start()
    {
        currentPlayer.onBattleMenuSelectionCallback += TriggerAction;
    }

    void TriggerAction()
    {
        isPressedAction = true;
    }

    void Update()
    {
 
    }
}
