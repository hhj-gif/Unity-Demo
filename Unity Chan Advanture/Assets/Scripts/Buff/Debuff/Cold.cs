using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cold : Buff
{
	protected override void BuffEffect()
	{

	}

	protected override void BuffEnd()
	{
		role.ChangeSpeed(effect);
	}

	protected override void BuffStart()
	{
		CreateParticle(MagicSetting.ColdBuffParticlePath);
		role.ChangeSpeed(-effect);
	}
}
