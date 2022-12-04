using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public enum HeartState
    {
        STARTING,
        FULL,
        EMPTY,
        ADDING,
        TAKINGOFF
    }

    public HeartState currentState;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentState = HeartState.STARTING;
    }

    private void Update()
    {
        for (int i = 0; i < Enum.GetValues(typeof(HeartState)).Length; i++)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag(i.ToString()))
            {
                currentState = (HeartState)i;
                break;
            }
        }
    }

    public void AddLife()
    {
        animator.SetTrigger("Add");
    }

    public void TakeOffLife()
    {
        animator.SetTrigger("TakeOff");
    }
}
