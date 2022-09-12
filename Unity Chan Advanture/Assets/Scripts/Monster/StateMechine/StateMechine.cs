using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMechine : Role
{
	protected Dictionary<int, BaseState> states;
	protected BaseState currentState;
	protected int stateNum;
	protected bool isStop;
	public virtual void Stop()
	{
		isStop = true;
	}

	public virtual void Continue()
	{
		isStop = false;
	}

	protected virtual void Update()
	{
		if (!isStop)
		{
			currentState.LogicUpdate();
			ChangeState();
		}
	}

	protected virtual void FixedUpdate()
	{
		currentState.PhysicsUpdate();
	}

	protected virtual void ChangeState()
	{
		int nextStateNum = currentState.ChangeState();
		if (stateNum != nextStateNum)
		{
			SwitchState(nextStateNum, states[nextStateNum]);
		}
	}
	protected virtual void SwitchState(int stateNum, BaseState nextBaseState)
	{
		if (currentState!=null)
			currentState.ExitState();
		//Debug.Log(nextBaseState);
		nextBaseState.EnterState();
		currentState = nextBaseState;
		this.stateNum = stateNum;
	}
}
