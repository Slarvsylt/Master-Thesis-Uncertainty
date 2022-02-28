using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine;


/// <summary>
/// This class should have max 3 units. It should handle Player interaction with the UI and communicate it to the units and game system.
/// </summary>
public class Player : MonoBehaviour
{

    private GameSystem gameSystem;

    [SerializeField]
    public List<Unit> Units;
    public Unit chosenUnit { get; set; }
    public Move chosenMove { get; set; }

    public GameObject attackButton;
    public GameObject defendButton;
    public GameObject moveButton;
    public GameObject move1Button;
    public GameObject move2Button;
    public GameObject move3Button;
    public GameObject Unit1Button;
    public GameObject Unit2Button;
    public GameObject Unit3Button;
    public GameObject cancelButton;

    public Func<Action<bool, bool>, IEnumerator> DoAction;

    // Start is called before the first frame update
    void Start()
    {
        attackButton.GetComponent<Button>().onClick.AddListener(ChooseAttack);
        defendButton.GetComponent<Button>().interactable = false;
        moveButton.GetComponent<Button>().interactable = false;
        move1Button.GetComponent<Button>().interactable = false;
        move2Button.GetComponent<Button>().interactable = false;
        move3Button.GetComponent<Button>().interactable = false;
        cancelButton.GetComponent<Button>().interactable = false;

        attackButton.GetComponent<Button>().interactable = false;
        defendButton.GetComponent<Button>().interactable = false;
        moveButton.GetComponent<Button>().interactable = false;
        move1Button.GetComponent<Button>().interactable = false;
        move2Button.GetComponent<Button>().interactable = false;
        move3Button.GetComponent<Button>().interactable = false;
        cancelButton.GetComponent<Button>().interactable = false;

        gameSystem = GameSystem.gameSystem;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ChooseAction()
    {
    }
    public void ChooseAttack()
    {
    }
    public void ChooseDefend()
    {
    }

    public void ChooseMove()
    {
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

    public IEnumerator Cancel()
    {
        //Cancel chosen action for selected unit
        yield return new WaitForSeconds(1);
    }

    public IEnumerator EndTurn()
    {
        //End turn and do stuff
        yield return new WaitForSeconds(1);
    }

}
