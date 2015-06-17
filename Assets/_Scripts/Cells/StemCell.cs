﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StemCell : BaseCell
{

    public GameObject stemtoHeat;
    public override void Mutation(CellType _newType)
    {
        GameObject newCell;
        switch (_newType)
        {
            case CellType.HEAT_CELL:
                newCell = GameObject.Instantiate(stemtoHeat, transform.position, Quaternion.identity) as GameObject;
                newCell.GetComponent<BaseCell>().currentProtein = currentProtein * 0.5f;
                newCell.GetComponent<BaseCell>().isAIPossessed = isAIPossessed;
                currentState = CellState.DEAD;
                break;
            case CellType.COLD_CELL:
                break;
            default:
                break;
        }
    }

    void DamagePreSecond()
    {
        primaryTarget.GetComponent<BaseCell>().currentProtein -= attackDamage;
    }

    public override void Attack(GameObject _target)
    {
        if (_target)
        {
            SetPrimaryTarget(_target);
            currentState = CellState.ATTACK;
        }
    }


    void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case CellState.IDLE:
                //guard mode auto attack enemy in range
                if (Input.GetKeyDown(KeyCode.S))
                {
                    base.PerfectSplit();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Mutation(CellType.HEAT_CELL);
                }
                //if (Vector3.Distance(GameObject.Find("HeatCell").transform.position, transform.position) <= fovRadius)
                //{
                //    Attack(GameObject.Find("HeatCell"));
                //}

                break;
            case CellState.ATTACK:
                if (primaryTarget)
                {
                    if (Vector3.Distance(primaryTarget.transform.position, transform.position) <= attackRange)
                    {
                        if (!IsInvoking("DamagePreSecond"))
                        {
                            if (GetComponent<ParticleSystem>().isStopped || GetComponent<ParticleSystem>().isPaused)
                            {
                                GetComponent<ParticleSystem>().Play();
                            }
                            InvokeRepeating("DamagePreSecond", 1.0f, 1.0f);
                        }

                    }
                    else if (Vector3.Distance(primaryTarget.transform.position, transform.position) <= fovRadius)
                    {
                        base.ChaseTarget();
                        if (IsInvoking("DamagePreSecond"))
                        {
                            if (GetComponent<ParticleSystem>().isPlaying)
                            {

                                GetComponent<ParticleSystem>().Stop();
                            }
                            CancelInvoke("DamagePreSecond");
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
            case CellState.CONSUMING:
                if (!primaryTarget)
                {
                    if (targets.Count > 0)
                    {
                        primaryTarget = targets[0];
                        targets.RemoveAt(0);
                    }
                    else
                    {
                        currentState = CellState.IDLE;
                    }
                }
                break;
            case CellState.MOVING:
                base.Update();

                break;
            case CellState.ATTACK_MOVING:
                if (!navAgent.isActiveAndEnabled && !primaryTarget && targets.Count == 0)
                {
                    currentState = CellState.IDLE;
                }
                break;
            case CellState.DEAD:
                base.Die();
                break;
            case CellState.CANCEROUS_SPLITTING:
                //Switch to split image
                //disable navAgent
                //start splitting timer
                //initialize splitting after timer

                break;
            case CellState.PERFECT_SPLITTING:
                break;
            case CellState.EVOLVING:
                break;
            case CellState.INCUBATING:
                break;
            case CellState.MERGING:
                break;
            default:
                break;
        }


    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    //LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        base.LateUpdate();
    }
}
