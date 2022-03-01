using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine;
using TMPro;

public enum BattleMenuOption {ATTACK, DEFEND, MOVE}
public enum Order { ATTACK, DEFEND, MOVE, NONE}

public class StoredOrder
{
    public Unit unit;
    public Order order;
    public Unit Target;
    public Move move;
}

public class StoredOrderForOne
{
    public Order order;
    public Unit Target;
    public Move move;
}
/// <summary>
/// This class should have max 3 units. It should handle Player interaction with the UI and communicate it to the units and game system.
/// </summary>
public class Player : MonoBehaviour
{

    private GameSystem gameSystem;

    public bool hasClicked = false;
    public bool isSelectionMode = false, onSelectionModeEnabled = false, keyDown = false;

    [SerializeField]
    public List<Unit> Units;
    public List<StoredOrder> ordersToBeDone;
    public StoredOrderForOne OneOrder;
    public Order selectedOrder;
    public Unit chosenUnit { get; set; }
    public Move chosenMove { get; set; }

    public ToggleGroup ActionToggleGroup;
    public ToggleGroup UnitToggleGroup;

    public Toggle attackButton;
    public Toggle defendButton;
    public Toggle move1Button;
    public Toggle move2Button;
    public Toggle move3Button;
    public Toggle Unit1Button;
    public Toggle Unit2Button;
    public Toggle Unit3Button;
    public Button cancelButton;
    public Button doneButton;

    public Button Enemy1Button;
    public Button Enemy2Button;
    public Button Enemy3Button;

    public TextMeshProUGUI StatusText;
    private string selectedUnit;
    private string someOrder;
    private string selectedEnemy;

    public Func<Action<bool, bool>, IEnumerator> DoAction;

    public delegate void OnBattleMenuSelectionCallback();
    public OnBattleMenuSelectionCallback onBattleMenuSelectionCallback;
    public delegate void OnBattleSelectionModeConfirmCallback();
    public OnBattleSelectionModeConfirmCallback onBattleSelectionModeConfirmCallback;

    // Start is called before the first frame update
    void Start()
    {
        chosenMove = null;

        attackButton.interactable = false;
        defendButton.interactable = false;
        move1Button.interactable = false;
        move2Button.interactable = false;
        move3Button.interactable = false;
        cancelButton.interactable = false;
        doneButton.interactable = false;

        Enemy1Button.interactable = false;
        //Enemy2Button.interactable = false;
        //Enemy3Button.interactable = false;

        attackButton.onValueChanged.AddListener(delegate {
            ChooseAttack(attackButton);
        });

        defendButton.onValueChanged.AddListener(delegate {
            ChooseDefend(defendButton);
        });

        move1Button.onValueChanged.AddListener(delegate {
            ChooseMove(move1Button, 0);
        });
        move2Button.onValueChanged.AddListener(delegate {
            ChooseMove(move2Button, 1);
        });
        move3Button.onValueChanged.AddListener(delegate {
            ChooseMove(move3Button, 2);
        });

        doneButton.onClick.AddListener(Done);
        cancelButton.onClick.AddListener(Cancel);

        Enemy1Button.onClick.AddListener(delegate { SelectedEnemy(Enemy1Button.gameObject.GetComponent<Unit>()); });

        Unit1Button.interactable = true;
        //Unit2Button.interactable = true;
        //Unit3Button.interactable = true;

        Unit1Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button,Unit1Button.gameObject.GetComponent<Unit>()); });

        gameSystem = GameSystem.gameSystem;
        //onBattleSelectionModeConfirmCallback += ConfirmSelectionModeChoice;
        ordersToBeDone = new List<StoredOrder>();
        OneOrder = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectionMode)
        {
            Enemy1Button.interactable = true;
            //Enemy2Button.interactable = true;
            //Enemy3Button.interactable = true;
        }
        if(ordersToBeDone.Count > 0 || OneOrder != null)
        {
            doneButton.interactable = true;
        }

        if(chosenUnit != null)
        {
            selectedUnit = chosenUnit.Name;
        }
        else
        {
            selectedUnit = "No Unit chosen!";
        }

        if(OneOrder != null)
        {
            someOrder = OneOrder.order.ToString();
        }
        else
        {
            someOrder = "No Order given!";
        }
        if(selectedEnemy == null)
        {
            selectedEnemy = "No enemy targeted!";
        }

        StatusText.text = selectedUnit + "\n" + someOrder + "\n" + selectedEnemy;
    }


    void ConfirmSelectionModeChoice()
    {
        if (isSelectionMode)
        {
            onSelectionModeEnabled = false;
            isSelectionMode = false;
            hasClicked = true;
        }
    }

    public IEnumerator Attack(Unit Target)
    {
        gameSystem.Attack(chosenUnit, Target);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Defend(Unit Target)
    {
        gameSystem.Defend(chosenUnit, Target);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Move()
    {
        gameSystem.Move(chosenUnit,chosenMove);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Move(Unit Target)
    {
        gameSystem.Move(chosenUnit, chosenMove, Target);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator SelectAction()
    {
        yield return new WaitForSeconds(1);
    }

    public void SelectedEnemy(Unit enemy)
    {
        Debug.Log("Clicked Enemy!");
        OneOrder = new StoredOrderForOne();
        OneOrder.move = chosenMove;
        OneOrder.order = selectedOrder;
        OneOrder.Target = enemy;
        isSelectionMode = false;

        //debug
        selectedEnemy = enemy.Name;

    }
    public void ChooseAttack(Toggle change)
    {
        if (change.isOn)
        {
            Debug.Log("Clicked Attack!");
            isSelectionMode = true;
            selectedOrder = Order.ATTACK;
        }
    }
    public void ChooseDefend(Toggle change)
    {
        if (change.isOn)
        {
            Debug.Log("Clicked Defend!");
            isSelectionMode = true;
            selectedOrder = Order.DEFEND;
        }
    }

    public void ChooseMove(Toggle change, int i)
    {
        if (change.isOn)
        {
            Debug.Log("Clicked a Move!");

            isSelectionMode = true;
            selectedOrder = Order.MOVE;
            chosenMove = chosenUnit.Moves[i];
        }
    }

    public void ChooseUnit(Toggle change, Unit unit)
    {
        if (change.isOn)
        {
            Debug.Log("Clicked a Unit!");
            ActionToggleGroup.SetAllTogglesOff();
            isSelectionMode = false;
            attackButton.interactable = true;
            defendButton.interactable = true;
            move1Button.interactable = true;
            move2Button.interactable = true;
            move3Button.interactable = true;

            move1Button.GetComponentInChildren<Text>().text = unit.Moves[0].MoveName;
            move2Button.GetComponentInChildren<Text>().text = unit.Moves[1].MoveName;
            move3Button.GetComponentInChildren<Text>().text = unit.Moves[2].MoveName;

            cancelButton.interactable = true;
            chosenUnit = unit;
        }
    }

    public void Cancel()
    {
        Debug.Log("Cancelled!");
        ResetStates();
    }

    public void Done()
    {
        if (ordersToBeDone.Count > 0 || OneOrder != null)
        {
            Debug.Log(chosenUnit.Name + " performs " + OneOrder.order.ToString() + " on " + OneOrder.Target.Name);
            
        }
        else
            Debug.Log("No Orders done!");

        ResetStates();
    }

    private void ResetStates() 
    {
        ActionToggleGroup.SetAllTogglesOff();
        UnitToggleGroup.SetAllTogglesOff();
        ordersToBeDone = new List<StoredOrder>();
        OneOrder = null;
        chosenMove = null;
        chosenUnit = null;
        isSelectionMode = false;
        Debug.Log("Reseted");
    }

    public IEnumerator ChooseTargetAction()
    {
        //Inactivate other buttons
        attackButton.GetComponent<Button>().interactable = false;
        defendButton.GetComponent<Button>().interactable = false;
        move1Button.GetComponent<Button>().interactable = false;
        move2Button.GetComponent<Button>().interactable = false;
        move3Button.GetComponent<Button>().interactable = false;
        //Click cancel to cancel action

        //Choose target


        yield return new WaitForSeconds(1);
    }

    public IEnumerator ChooseUnit(Unit unit)
    {
        attackButton.GetComponent<Button>().interactable = true;
        defendButton.GetComponent<Button>().interactable = true;
        chosenUnit = unit;
        yield return new WaitForSeconds(1);
    }

    public IEnumerator EndTurn()
    {
        //End turn and do stuff
        yield return new WaitForSeconds(1);
    }

}
