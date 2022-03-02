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
    public Player opponent;

    private GameSystem gameSystem;

    public bool hasClicked = false;
    public bool isSelectionMode = false, onSelectionModeEnabled = false, keyDown = false;

    [SerializeField]
    public List<Unit> Units;
    public List<GameObject> GameObjectUnits;
    public List<StoredOrder> ordersToBeDone;
    public StoredOrderForOne OneOrder;
    public Order selectedOrder;

    public StateHandler stateHandler;
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

    private List<Toggle> unitToggles;
    private List<Button> Enemybuttons;

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

    private void Awake()
    {

    }


    void Start()
    {
        LoadUnits();
        unitToggles = new List<Toggle> { Unit1Button, Unit2Button, Unit3Button };
        Enemybuttons = new List<Button> { Enemy1Button,Enemy2Button,Enemy3Button };
        chosenMove = null;

        attackButton.interactable = false;
        defendButton.interactable = false;
        move1Button.interactable = false;
        move2Button.interactable = false;
        move3Button.interactable = false;
        cancelButton.interactable = false;
        doneButton.interactable = false;

        Enemy1Button.interactable = false;
        Enemy2Button.interactable = false;
        Enemy3Button.interactable = false;

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

        //Enemy1Button.onClick.AddListener(delegate { SelectedEnemy(Enemy1Button.gameObject.GetComponent<Unit>()); });
       // Enemy2Button.onClick.AddListener(delegate { SelectedEnemy(Enemy2Button.gameObject.GetComponent<Unit>()); });
        //Enemy3Button.onClick.AddListener(delegate { SelectedEnemy(Enemy3Button.gameObject.GetComponent<Unit>()); });

        Enemy1Button.onClick.AddListener(delegate { SelectedEnemy(0); });
        Enemy2Button.onClick.AddListener(delegate { SelectedEnemy(1); });
        Enemy3Button.onClick.AddListener(delegate { SelectedEnemy(2); });

        Unit1Button.interactable = true;
        Unit2Button.interactable = true;
        Unit3Button.interactable = true;

        //Unit1Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button,Unit1Button.gameObject.GetComponent<Unit>()); });
        //Unit2Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button, Unit2Button.gameObject.GetComponent<Unit>()); });
        //Unit3Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button, Unit3Button.gameObject.GetComponent<Unit>()); });

        Unit1Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button, 0); });
        Unit2Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit2Button, 1); });
        Unit3Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit3Button, 2); });

        gameSystem = GameSystem.gameSystem;
        //onBattleSelectionModeConfirmCallback += ConfirmSelectionModeChoice;
        ordersToBeDone = new List<StoredOrder>();
        OneOrder = null;
        //LoadUnits();
        Debug.Log("Starting " + gameObject.name);
        //PopulateField(); // remove later
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectionMode)
        {
            Enemy1Button.interactable = true;
            Enemy2Button.interactable = true;
            Enemy3Button.interactable = true;
        }
        else
        {
            Enemy1Button.interactable = false;
            Enemy2Button.interactable = false;
            Enemy3Button.interactable = false;
        }
        if (ordersToBeDone.Count > 0 || OneOrder != null)
        {
            doneButton.interactable = true;
        }
        else
        {
            doneButton.interactable = false;
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
        //Debug.Log("Clicked Enemy!");
        StoredOrder newOrder = new StoredOrder();
        newOrder.move = chosenMove;
        newOrder.order = selectedOrder;
        newOrder.Target = enemy;
        newOrder.unit = chosenUnit;
        addOrder(newOrder);
        isSelectionMode = false;

        //debug
        selectedEnemy = enemy.Name;
    }

    //Better?
    public void SelectedEnemy(int whichOne)
    {
        //Debug.Log("Clicked Enemy!");
        StoredOrder newOrder = new StoredOrder();
        newOrder.move = chosenMove;
        newOrder.order = selectedOrder;
        newOrder.Target = opponent.Units[whichOne];
        newOrder.unit = chosenUnit;
        addOrder(newOrder);
        isSelectionMode = false;

        //debug
        selectedEnemy = opponent.Units[whichOne].Name;
    }


    public void ChooseAttack(Toggle change)
    {
        if (change.isOn)
        {
           // Debug.Log("Clicked Attack!");
            isSelectionMode = true;
            selectedOrder = Order.ATTACK;
        }
    }
    public void ChooseDefend(Toggle change)
    {
        if (change.isOn)
        {
          //  Debug.Log("Clicked Defend!");
            isSelectionMode = true;
            selectedOrder = Order.DEFEND;
        }
    }

    public void ChooseMove(Toggle change, int i)
    {
        if (change.isOn)
        {
          //  Debug.Log("Clicked a Move!");

            isSelectionMode = true;
            selectedOrder = Order.MOVE;
            chosenMove = chosenUnit.Moves[i];
        }
    }

    public void ChooseUnit(Toggle change, Unit unit)
    {
        if (change.isOn)
        {
          //  Debug.Log("Clicked a Unit!");
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

    public void ChooseUnit(Toggle change, int whichUnit)
    {
        if (change.isOn)
        {
            Unit unit = Units[whichUnit];
         //   Debug.Log("Clicked a Unit! " + whichUnit);
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
       // Debug.Log("Cancelled!");
        ResetStates();
    }

    public void Done()
    {
        if (ordersToBeDone.Count == 3 || OneOrder != null)
        {
          //  Debug.Log(ordersToBeDone[0].unit.Name + " performs " + ordersToBeDone[0].order.ToString() + " on " + ordersToBeDone[0].Target.Name);
          //  Debug.DrawLine(unitToggles[ordersToBeDone[0].unit.index].gameObject.transform.localPosition, unitToggles[ordersToBeDone[0].Target.index].gameObject.transform.localPosition, Color.red, 5);
          //  Debug.Log(ordersToBeDone[1].unit.Name + " performs " + ordersToBeDone[1].order.ToString() + " on " + ordersToBeDone[1].Target.Name);
          //  Debug.DrawLine(unitToggles[ordersToBeDone[1].unit.index].gameObject.transform.localPosition, unitToggles[ordersToBeDone[1].Target.index].gameObject.transform.localPosition, Color.red, 5);
           // Debug.Log(ordersToBeDone[2].unit.Name + " performs " + ordersToBeDone[2].order.ToString() + " on " + ordersToBeDone[2].Target.Name);
          //  Debug.DrawLine(unitToggles[ordersToBeDone[2].unit.index].gameObject.transform.localPosition, unitToggles[ordersToBeDone[2].Target.index].gameObject.transform.localPosition, Color.red, 5);

        }
        else
            Debug.Log("No Orders done!");

        ResetStates();
        stateHandler.ChangeState(GameState.NEXTTURN);
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
        attackButton.interactable = false;
        defendButton.interactable = false;
        move1Button.interactable = false;
        move2Button.interactable = false;
        move3Button.interactable = false;
        //Debug.Log("Reseted");
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

    private void addOrder(StoredOrder order)
    {
        Unit performer = order.unit;
        bool exist = false;
        int index;

        for (int i = 0; i < ordersToBeDone.Count; i++)
        {
            if (System.Object.ReferenceEquals(ordersToBeDone[i].unit, performer))
            {
                exist = true;
                index = i;
                ordersToBeDone[index] = order; //Overwrite order
               // Debug.Log("Added order, overwriting old order");
                return;
            }
        }
        if (!exist)
        {
            ordersToBeDone.Add(order);
           // Debug.Log("Added new order");
        }
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

    public void LoadUnits()
    {
        //Add method for adding new moves
        /*foreach (GameObject unit in GameObjectUnits)
        {
            List<Move> NewMoves = new List<Move> { MoveDatabase.Instance.GetMove("sleep"), MoveDatabase.Instance.GetMove("sleep2"), MoveDatabase.Instance.GetMove("sleep3") };
            unit.GetComponent<Unit>().AddMoves(NewMoves);
            Units.Add(unit.GetComponent<Unit>());
            Debug.Log("Loaded a unit! " + unit.GetComponent<Unit>().Name + " plus moves!");
        }*/
        for (int i = 0; i < GameObjectUnits.Count; i++)
        {
            GameObject unit = GameObjectUnits[i];
            List<Move> NewMoves = new List<Move> { MoveDatabase.Instance.GetMove("sleep"), MoveDatabase.Instance.GetMove("sleep2"), MoveDatabase.Instance.GetMove("sleep3") };
            unit.GetComponent<Unit>().AddMoves(NewMoves);
            unit.GetComponent<Unit>().index = i;
            Units.Add(unit.GetComponent<Unit>());
           // Debug.Log("Loaded a unit! " + unit.GetComponent<Unit>().Name + " plus moves!" + " index : " + i);
        }
    }

    /// <summary>
    /// Method for populating the field with portraits of the players own Units.
    /// Should happen at the start of every turn before the player make their moves.
    /// Purpose: To differentiate which player's turn it is.
    /// </summary>
    public void PopulateField()
    {
        Unit1Button.gameObject.GetComponent<Image>().sprite = GameObjectUnits[0].GetComponent<Image>().sprite;
        Unit2Button.gameObject.GetComponent<Image>().sprite = GameObjectUnits[1].GetComponent<Image>().sprite;
        Unit3Button.gameObject.GetComponent<Image>().sprite = GameObjectUnits[2].GetComponent<Image>().sprite;

        Enemy1Button.gameObject.GetComponent<Image>().sprite = opponent.GameObjectUnits[0].GetComponent<Image>().sprite;
        Enemy2Button.gameObject.GetComponent<Image>().sprite = opponent.GameObjectUnits[1].GetComponent<Image>().sprite;
        Enemy3Button.gameObject.GetComponent<Image>().sprite = opponent.GameObjectUnits[2].GetComponent<Image>().sprite;

        Debug.Log("Pop");
    }
}
