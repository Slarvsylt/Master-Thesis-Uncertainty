using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusHandler : MonoBehaviour
{

    public static StatusHandler statusHandler;


    private void Awake()
    {
        statusHandler = this;
    }

    public event Action OnMove;
    public void Move()
    {
        OnMove();
    }

    public event Action OnAttack;
    public void Attack()
    {
        OnAttack();
    }

    public event Action OnDefend;
    public void Defend()
    {
        OnDefend();
    }



}
