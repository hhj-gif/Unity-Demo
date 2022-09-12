using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Buff
{
	private float burnTime;

	protected override void BuffEffect()
	{
		burnTime += Time.deltaTime;
		if (burnTime >=0.5f)
		{
			role.Damaged(effect, false);
			burnTime -= 0.5f;
		}
	}

	protected override void BuffEnd()
	{
	}

	protected override void BuffStart()
	{
		burnTime = 0;
		CreateParticle(MagicSetting.BurnBuffParticlePath);
	}
}
