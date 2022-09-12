using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MagicAttribute(MagicType = MagicType.Fire)]
public class FireMagic : Magic
{
	public override void ActivateMagic()
	{
		CreateParticle(MagicSetting.FireParticlePath);
		Burn burn = gameObject.AddComponent<Burn>();
		burn.StartBuff(MagicSetting.FireBurnTime, MagicSetting.FireBurnDamage);
		GetComponent<Role>().Damaged(MagicSetting.FireDamage);
		Destroy(this,2);
	}
}
