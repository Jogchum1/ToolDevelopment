using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidState : BaseState
{
    public BoidMaster boidMaster;
    public GameObject sliders;

    public override void OnEnter()
    {
        sliders.SetActive(true);
        boidMaster.spawnBoids();
    }

    public override void OnExit()
    {
        boidMaster.DeleteBoids();
    }

    public override void OnUpdate()
    {
        
    }

    public void ChangeState()
    {
        owner.SwitchState(typeof(ObjectState));
    }
}
