using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : BaseState
{
    public GameObject sliders;
    public GameObject loadButton;
    public BoidMaster boidmaster;

    public override void OnEnter()
    {
        Debug.Log("JOE");
        sliders.SetActive(false);
        loadButton.SetActive(true);
    }

    public override void OnExit()
    {
        loadButton.SetActive(false);
    }

    public override void OnUpdate()
    {
        //Debug.Log(boidmaster.boidAmount);
    }

    public void ChangeState()
    {
        owner.SwitchState(typeof(BoidState));
    }
}
