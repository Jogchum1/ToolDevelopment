using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : BaseState
{
    public GameObject sliders;
    public BoidMaster boidmaster;

    public override void OnEnter()
    {
        Debug.Log("JOE");
        sliders.SetActive(false);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        Debug.Log(boidmaster.boidAmount);
    }

    public void ChangeState()
    {
        owner.SwitchState(typeof(BoidState));
    }
}
