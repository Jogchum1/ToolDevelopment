using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FSM 
{

    private Dictionary<System.Type, BaseState> stateDictionary = new Dictionary<System.Type, BaseState>();
    private BaseState currentState;

    public FSM(System.Type startState, params BaseState[] states)
    {
        foreach (BaseState state in states)
        {
            state.Initialize(this);
            stateDictionary.Add(state.GetType(), state);
        }
        SwitchState(startState);
    }

    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }

    public void SwitchState(System.Type newStateType)
    {
        currentState?.OnExit();
        currentState = stateDictionary[newStateType];
        currentState?.OnEnter();
    }
}

public abstract class BaseState : MonoBehaviour
{

    protected FSM owner;

    public void Initialize(FSM owner)
    {
        this.owner = owner;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
