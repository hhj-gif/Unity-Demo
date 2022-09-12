using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedState : BaseState
{
	//������״̬
	public HittedState(Animator animator) : base(animator)
	{

	}
	public override void LogicUpdate()
	{
	}
	public override void PhysicsUpdate()
	{
	}
	public override void EnterState()
	{
		animator.SetTrigger("damage");
	}
	public override void ExitState()
	{

	}
	public override int ChangeState()
	{
		//������
		return (int)MonsterStateTpye.GetHit;
	}
}