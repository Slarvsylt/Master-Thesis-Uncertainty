using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Text;
using System.Linq;
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

    public GameSystem gameSystem;

    public bool hasLost;

    public bool hasClicked = false;
    public bool isSelectionMode = false, onSelectionModeEnabled = false, keyDown = false;
    public bool isTurn = false;

    [SerializeField]
    public List<Unit> Units;
    public bool[] deadIndexes = new bool[3];
    public bool[] deadEnemies = new bool[3];
    public List<GameObject> GameObjectUnits;
    public List<StoredOrder> ordersToBeDone;
    public StoredOrderForOne OneOrder;
    public Order selectedOrder;

    public StateHandler stateHandler;

    [SerializeField]
    public Unit chosenUnit;
    [SerializeField]
    public Move chosenMove;

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

    public TextMeshProUGUI UnitNameText;
    public TextMeshProUGUI UnitTypeText;
    public TextMeshProUGUI UnitActiveEffectsText;

    public TextMeshProUGUI OrderText;


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

        Enemy1Button.interactable = false;
        Enemy2Button.interactable = false;
        Enemy3Button.interactable = false;

        //Enemy1Button.onClick.AddListener(delegate { SelectedEnemy(Enemy1Button.gameObject.GetComponent<Unit>()); });
       // Enemy2Button.onClick.AddListener(delegate { SelectedEnemy(Enemy2Button.gameObject.GetComponent<Unit>()); });
        //Enemy3Button.onClick.AddListener(delegate { SelectedEnemy(Enemy3Button.gameObject.GetComponent<Unit>()); });

        //Enemy1Button.onClick.AddListener(delegate { SelectedEnemy(0); });
        //Enemy2Button.onClick.AddListener(delegate { SelectedEnemy(1); });
        //Enemy3Button.onClick.AddListener(delegate { SelectedEnemy(2); });

        Unit1Button.interactable = true;
        Unit2Button.interactable = true;
        Unit3Button.interactable = true;

        //Unit1Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button,Unit1Button.gameObject.GetComponent<Unit>()); });
        //Unit2Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button, Unit2Button.gameObject.GetComponent<Unit>()); });
        //Unit3Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button, Unit3Button.gameObject.GetComponent<Unit>()); });

        //Unit1Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button, 0); });
        //Unit2Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit2Button, 1); });
        //Unit3Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit3Button, 2); });

        //onBattleSelectionModeConfirmCallback += ConfirmSelectionModeChoice;
        ordersToBeDone = new List<StoredOrder>();
        OneOrder = null;
        //LoadUnits();
        //PopulateField(); // remove later

        //Unit1Button.mouse

        
    }

    // Update is called once per frame
    void Update()
    {
        if (deadIndexes.All(x => x))
        {
            Debug.Log("Everyone is dead!");
            hasLost = true;
        }
            

        if (isSelectionMode)
        {
            Enemy1Button.interactable = true;
            Enemy2Button.interactable = true;
            Enemy3Button.interactable = true;

            if (!opponent.Units[0].isDead)
            {
                Enemy1Button.interactable = true;
            }
            else
            {
                Enemy1Button.interactable = false;
            }
            if (!opponent.Units[1].isDead)
            {
                Enemy2Button.interactable = true;
            }
            else
            {
                Enemy2Button.interactable = false;
            }
            if (!opponent.Units[2].isDead)
            {
                Enemy3Button.interactable = true;
            }
            else
            {
                Enemy3Button.interactable = false;
            }
        }
        else
        {
            Enemy1Button.interactable = false;
            Enemy2Button.interactable = false;
            Enemy3Button.interactable = false;
        }


        if (!UnitToggleGroup.AnyTogglesOn())
        {
            ResetStates();
        }

        showOrders();
        //StatusText.text = selectedUnit + " " + someOrder + "s " + selectedEnemy;
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
        Debug.Log(whichOne);
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
            isSelectionMode = true;
            selectedOrder = Order.MOVE;

            chosenMove = chosenUnit.Moves[i];
        }
    }

    public void ChooseUnit(Toggle change, int whichUnit)
    {
        ColorBlock cb = change.colors;
        if (change.isOn)
        {
            cb.normalColor = new Color(255, 205, 0);

            Unit unit = Units[whichUnit];
           // Debug.Log("Selected!" + unit.Name);
            if (!unit.isDead)
            {
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
                UnitNameText.text = chosenUnit.Name + " " + chosenUnit.currentHP + "/" + chosenUnit.maxHP + " HP \n" + chosenUnit.currentMP +"/" +chosenUnit.maxMP+ "MP";

                StringBuilder sb = new StringBuilder();
                foreach (MoveType move in chosenUnit.Strengths)
                {
                    sb.Append(move + "\n");
                }
                UnitTypeText.text = sb.ToString();

                sb = new StringBuilder();
                if (gameSystem.statusEffects.ContainsKey(unit))
                {
                    if(gameSystem.statusEffects[unit].Count > 0)
                    {
                        foreach (StatusEffect effect in gameSystem.statusEffects[unit])
                        {
                            sb.Append(effect.effect.EffectName + "\n");
                        }
                    }
                    else
                    {
                        sb.Append("No active effects.");
                    }
                }
                else
                {
                    sb.Append("No active effects.");
                }

                UnitActiveEffectsText.text = sb.ToString();
            }
            else
            {
                Debug.Log("dead" + unit.isDead);
                //deadIndexes[whichUnit] = true;
                //Cannot select unit
            }
            //Debug.Log("Clicked a Unit! " + whichUnit);

        }
        else
        {
        //    Debug.Log("DEselected!");
            cb.normalColor = Color.white;
        }
        change.colors = cb;
    }

    public void Cancel()
    {
       // Debug.Log("Cancelled!");
        ResetStates();
    }

    public void Done()
    {
        StartCoroutine(Done1());
    }

    private void showOrders()
    {
        StringBuilder sb = new StringBuilder();
        if (ordersToBeDone.Count > 0)
        {
            foreach (StoredOrder order in ordersToBeDone)
            {
                if (order.order == Order.ATTACK)
                {
                    sb.Append(order.unit.Name + "  " + order.order.ToString() + "  " + order.Target.Name + "! \n");
                }
                if (order.order == Order.DEFEND)
                {
                    sb.Append(order.unit.Name + "  " + order.order.ToString() + "  " + order.Target.Name + "! \n");
                }
                if (order.order == Order.MOVE)
                {
                    if (order.unit.currentMP >= order.move.MPcost)
                    {
                        sb.Append(order.unit.Name + " performs the move  " + order.move.MoveName + " on " + order.Target.Name + "! \n");
                    }
                    else
                    {
                        sb.Append(order.unit.Name + " does not have enough MP to perform " + order.move.MoveName);
                    }
                }
            }
        }
        else
        {
            sb.Append("No Orders given!");
        }
        OrderText.text = sb.ToString();
    }

    public IEnumerator Done1()
    {
       // StringBuilder sb = new StringBuilder();
        if(ordersToBeDone.Count > 0)
        {
            foreach (StoredOrder order in ordersToBeDone)
            {
                if (order.order == Order.ATTACK)
                {
                   // sb.Append(order.unit.Name + "  " + order.order.ToString() + "  " + order.Target.Name + "! \n");
                    yield return StartCoroutine(gameSystem.Attack(order.unit, order.Target));
                    //gameSystem.Attack(order.unit, order.Target);
                }
                if (order.order == Order.DEFEND)
                {
                   // sb.Append(order.unit.Name + "  " + order.order.ToString() + "  " + order.Target.Name + "! \n");
                    gameSystem.Defend(order.unit, order.Target);
                }
                if (order.order == Order.MOVE)
                {
                    if (order.unit.currentMP >= order.move.MPcost)
                    {
                        yield return StartCoroutine(gameSystem.Move(order.unit, order.move, order.Target));
                     //   sb.Append(order.unit.Name + " performs the move  " + order.move.MoveName + " on " + order.Target.Name + "! \n");
                        //Debug.Log("Cast " + order.move.MoveName);
                    }
                    else
                    {
                        Debug.Log("Not enough mp");
                    }
                }
            }
        }
        else
        {
           // sb.Append("No Orders given!");
        }
       
      //  OrderText.text = sb.ToString();

        ResetStates();
        opponent.ResetStates();

        Unit1Button.onValueChanged.RemoveAllListeners();
        Unit2Button.onValueChanged.RemoveAllListeners();
        Unit3Button.onValueChanged.RemoveAllListeners();

        Enemy1Button.onClick.RemoveAllListeners();
        Enemy2Button.onClick.RemoveAllListeners();
        Enemy3Button.onClick.RemoveAllListeners();

        attackButton.onValueChanged.RemoveAllListeners();

        defendButton.onValueChanged.RemoveAllListeners();

        move1Button.onValueChanged.RemoveAllListeners();

        move2Button.onValueChanged.RemoveAllListeners();

        move3Button.onValueChanged.RemoveAllListeners();

        //UnityEditor.Events.UnityEventTools.RemovePersistentListener(Unit1Button.onValueChanged, 0);
        //UnityEditor.Events.UnityEventTools.RemovePersistentListener(Unit2Button.onValueChanged, 0);
        //UnityEditor.Events.UnityEventTools.RemovePersistentListener(Unit3Button.onValueChanged, 0);

        //Unit1Button.onValueChanged.RemoveListener(delegate { ChooseUnit(Unit1Button, 0); });
        //Unit2Button.onValueChanged.RemoveListener(delegate { ChooseUnit(Unit2Button, 1); });
        //Unit3Button.onValueChanged.RemoveListener(delegate { ChooseUnit(Unit3Button, 2); });
        //Enemy1Button.onClick.RemoveListener(delegate { SelectedEnemy(0); });
        //Enemy2Button.onClick.RemoveListener(delegate { SelectedEnemy(1); });
        //Enemy3Button.onClick.RemoveListener(delegate { SelectedEnemy(2); });

        /*attackButton.onValueChanged.RemoveListener(delegate {
            ChooseAttack(attackButton);
        });

        defendButton.onValueChanged.RemoveListener(delegate {
            ChooseDefend(defendButton);
        });

        move1Button.onValueChanged.RemoveListener(delegate {
            ChooseMove(move1Button, 0);
        });
        move2Button.onValueChanged.RemoveListener(delegate {
            ChooseMove(move2Button, 1);
        });
        move3Button.onValueChanged.RemoveListener(delegate {
            ChooseMove(move3Button, 2);
        });*/

        doneButton.onClick.RemoveListener(Done);

        cancelButton.onClick.RemoveListener(Cancel);

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
        selectedEnemy = null;
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
        if (!unit.isDead)
        {
            attackButton.GetComponent<Button>().interactable = true;
            defendButton.GetComponent<Button>().interactable = true;
            chosenUnit = unit;
        }
        else
        {
            //Cannot target
        }
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
        Units = new List<Unit>();
        for (int i = 0; i < GameObjectUnits.Count; i++)
        {
            Unit unit = GameObject.Instantiate<Unit>(GameObjectUnits[i].GetComponent<Unit>());
            List<Move> NewMoves = new List<Move> { MoveDatabase.Instance.GetMove("fire"), MoveDatabase.Instance.GetMove("fart"), MoveDatabase.Instance.GetMove("headache") };
            unit.AddMoves(NewMoves);
            unit.index = i;
            //Debug.Log(unit.Name);
            unit.transform.SetParent(GameObject.Find("Canvas").transform, false);
            unit.GetComponent<Image>().enabled = false;
            Units.Add(unit);
            
           // Debug.Log("Loaded a unit! " + unit.GetComponent<Unit>().Name + " plus moves!" + " index : " + i);
        }
    }

    public void DisableUnit(int index)
    {
        
    }

    /// <summary>
    /// Method for populating the field with portraits of the players own Units.
    /// Should happen at the start of every turn before the player make their moves.
    /// Purpose: To differentiate which player's turn it is.
    /// </summary>
    public void PopulateField()
    {

        Unit1Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit1Button, 0); });
        Unit2Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit2Button, 1); });
        Unit3Button.onValueChanged.AddListener(delegate { ChooseUnit(Unit3Button, 2); });

        Enemy1Button.onClick.AddListener(delegate { SelectedEnemy(0); });
        Enemy2Button.onClick.AddListener(delegate { SelectedEnemy(1); });
        Enemy3Button.onClick.AddListener(delegate { SelectedEnemy(2); });

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

        cancelButton.onClick.AddListener(Cancel);


        if (!Units[0].isDead)
        {
            Unit1Button.interactable = true;
        }
        else
        {
            Unit1Button.interactable = false;
        }
        if (!Units[1].isDead)
        {
            Unit2Button.interactable = true;
        }
        else
        {
            Unit2Button.interactable = false;
        }
        if (!Units[2].isDead)
        {
            Unit3Button.interactable = true;
        }
        else
        {
            Unit3Button.interactable = false;
        }




        doneButton.onClick.AddListener(Done);
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].isDead)
            {
                deadIndexes[i] = true;
            }
            else
            {
                deadIndexes[i] = false;
            }
        }

        for (int i = 0; i < opponent.Units.Count; i++)
        {
            if (opponent.Units[i].isDead)
            {
                deadEnemies[i] = true;
            }
            else
            {
                deadEnemies[i] = false;
            }
        }
        Unit1Button.gameObject.GetComponent<Image>().sprite = GameObjectUnits[0].GetComponent<Image>().sprite;
        Unit2Button.gameObject.GetComponent<Image>().sprite = GameObjectUnits[1].GetComponent<Image>().sprite;
        Unit3Button.gameObject.GetComponent<Image>().sprite = GameObjectUnits[2].GetComponent<Image>().sprite;

        Units[0].transform.position = Unit1Button.transform.position;
        Units[1].transform.position = Unit2Button.transform.position;
        Units[2].transform.position = Unit3Button.transform.position;

        Units[0].attachedObject = Unit1Button.gameObject;
        Units[1].attachedObject = Unit2Button.gameObject;
        Units[2].attachedObject = Unit3Button.gameObject;

        Enemy1Button.gameObject.GetComponent<Image>().sprite = opponent.GameObjectUnits[0].GetComponent<Image>().sprite;
        Enemy2Button.gameObject.GetComponent<Image>().sprite = opponent.GameObjectUnits[1].GetComponent<Image>().sprite;
        Enemy3Button.gameObject.GetComponent<Image>().sprite = opponent.GameObjectUnits[2].GetComponent<Image>().sprite;

        opponent.Units[0].transform.position = Enemy1Button.transform.position;
        opponent.Units[1].transform.position = Enemy2Button.transform.position;
        opponent.Units[2].transform.position = Enemy3Button.transform.position;

        opponent.Units[0].attachedObject = Enemy1Button.gameObject;
        opponent.Units[1].attachedObject = Enemy2Button.gameObject;
        opponent.Units[2].attachedObject = Enemy3Button.gameObject;

        //Debug.Log("Pop " + gameObject.name);
    }
}
