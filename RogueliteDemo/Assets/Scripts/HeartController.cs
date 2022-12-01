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
        //currentState = (HeartState)animator.GetCurrentAnimatorStateInfo(0);
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
