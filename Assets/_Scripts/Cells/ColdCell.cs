﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColdCell : BaseCell
{

    void Awake()
    {
        base.bAwake();
    }

    // Use this for initialization
    void Start()
    {
        base.bStart();
    }
    void DamagePreSecond()
    {
        if (primaryTarget != null)
        {
            primaryTarget.GetComponent<BaseCell>().currentProtein -= (attackDamage / primaryTarget.GetComponent<BaseCell>().defense);
        }
    }

    public void Guarding()
    {
        List<GameObject> aiUnits = GameObjectManager.FindAIUnits();
        for (int i = 0; i < aiUnits.Count; i++)
        {
            if (Vector3.Distance(aiUnits[i].transform.position, transform.position) <= fovRadius)
            {
                if (aiUnits[i] != this.gameObject)
                {
                    Attack(aiUnits[i]);
                }
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case CellState.IDLE:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    base.CancerousSplit();
                }
                Guarding();
                break;
            case CellState.ATTACK:
                if(primaryTarget != null)
                {
                    if (Vector3.Distance(primaryTarget.transform.position, transform.position) <= attackRange)
                    {
                        if (!IsInvoking("DamagePreSecond"))
                        {
                            InvokeRepeating("DamagePreSecond", 1.0f, 1.0f);

                        }
                    }
                    else if (Vector3.Distance(primaryTarget.transform.position, transform.position) <= fovRadius)
                    {
                        if (IsInvoking("DamagePreSecond"))
                        {
                            CancelInvoke("DamagePreSecond");
                        }
                        if (Vector3.Distance(primaryTarget.transform.position, transform.position) > attackRange)
                        {
                            base.ChaseTarget();
                        }
                    }
                    else
                    {
                        SetPrimaryTarget(null);
                        navAgent.Stop();
                    }

                }
                else
                {
                    currentState = CellState.IDLE;
                }
                break;
            case CellState.MOVING:
             base.bUpdate();
                if (primaryTarget != null)
                {
                    if (primaryTarget.GetComponent<BaseCell>())
                    {
                        currentState = CellState.ATTACK;
                    }
                    else if (primaryTarget.GetComponent<Protein>())
                    {
                        currentState = CellState.CONSUMING;
                    }

                }

                break;
            case CellState.ATTACK_MOVING:
             //  if (!navAgent.isActiveAndEnabled && !primaryTarget && targets.Count == 0)
             //  {
             //      currentState = CellState.IDLE;
             //  }
                break;
            case CellState.CONSUMING:
                base.bUpdate();
                break;
            case CellState.DEAD:
                base.Die();
                break;
         
            default:
                break;
        }
    }



    //LateUpdate is called after all Update functions have been called

    public override void Attack(GameObject _target)
    {
        if (_target)
        {
            SetPrimaryTarget(_target);
            currentState = CellState.ATTACK;
        }
    }

    void FixedUpdate()
    {
        base.bFixedUpdate();
    }

    void LateUpdate()
    {
        base.bLateUpdate();
    }
}