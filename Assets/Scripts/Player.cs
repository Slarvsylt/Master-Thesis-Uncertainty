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

    [SerializeField]
    public List<Unit> Units;
    public Unit chosenUnit { get; set; }
    public Move chosenMove { get; set; }

    public GameObject attackButton;
    public GameObject defendButton;
    public GameObject move1Button;
    public GameObject move2Button;
    public GameObject move3Button;
    public GameObject cancelButton;

    public Func<Action<bool, bool>, IEnumerator> DoAction;

    // Start is called before the first frame update
    void Start()
    {
        attackButton.GetComponent<Button>().interactable = false;
        defendButton.GetComponent<Button>().interactable = false;
        move1Button.GetComponent<Button>().interactable = false;
        move2Button.GetComponent<Button>().interactable = false;
        move3Button.GetComponent<Button>().interactable = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Attack(Unit Target)
    {
        chosenUnit.PerformAttack();
        Target.TakeDamage(1);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Defend(Unit Target)
    {
        chosenUnit.Defend();
        Target.defended = true;
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Move()
    {
        chosenUnit.MakeMove(chosenMove);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Move(Unit Target)
    {
        chosenUnit.MakeMove(chosenMove);
        Target.HitByMove(chosenMove);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator SelectAction()
    {
        yield return new WaitForSeconds(1);
    }

    public IEnumerator ChooseAction()
    {
        yield return new WaitForSeconds(1);
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
