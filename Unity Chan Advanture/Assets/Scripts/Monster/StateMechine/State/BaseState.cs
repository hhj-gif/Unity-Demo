using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
	protected Transform transform;
	protected Animator animator;
	public BaseState(Animator animator)
	{
		this.animator = animator;
		transform = animator.transform;
	}
	public virtual void LogicUpdate()
	{
	}
	public virtual void PhysicsUpdate()
	{
	}

	public virtual int ChangeState()
	{
		return 0;
	}
	public virtual void EnterState()
	{

	}
	public virtual void ExitState()
	{

	}
}
