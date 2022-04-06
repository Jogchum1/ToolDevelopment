using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidState : BaseState
{
    public BoidMaster boidMaster;
    public GameObject sliders;
    public GameObject saveButton;

    public override void OnEnter()
    {
        sliders.SetActive(true);
        boidMaster.spawnBoids();
        saveButton.SetActive(true);
    }

    public override void OnExit()
    {
        boidMaster.DeleteBoids();
        saveButton.SetActive(false);
    }

    public override void OnUpdate()
    {
        
    }

    public void ChangeState()
    {
        owner.SwitchState(typeof(ObjectState));
    }
}
