using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DieState : BaseState
{
	bool isDie = false;
	//待机状态
	public DieState(Animator animator) : base(animator)
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
		if (!isDie)
		{
			animator.SetTrigger("isDie");
			animator.gameObject.AddComponent<MonsterDieEffect>();
			GameObject.Destroy(animator.gameObject, 5);
		}
	}
	public override void ExitState()
	{

	}
	public override int ChangeState()
	{
		return (int)MonsterStateTpye.Die;
	}
}
