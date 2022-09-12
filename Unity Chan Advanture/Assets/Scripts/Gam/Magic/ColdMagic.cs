using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MagicAttribute(MagicType = MagicType.Cold)]
public class ColdMagic : Magic
{
	public override void ActivateMagic()
	{
		CreateParticle(MagicSetting.ColdParticlePath);
		Cold cold = gameObject.AddComponent<Cold>();
		cold.StartBuff(500, 1);
		//GetComponent<Role>().Damaged(MagicSetting.FireDamage);
		Destroy(this, 2);
	}
}
