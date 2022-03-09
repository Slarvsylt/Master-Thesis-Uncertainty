using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Player currentPlayer;

    Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            Toggled(toggle);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggled(Toggle change)
    {
        Debug.Log("Attack Toggled");
    }
}
