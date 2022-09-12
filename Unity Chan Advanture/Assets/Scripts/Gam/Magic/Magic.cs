using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagicAttribute: Attribute
{
	public MagicType MagicType { get; set; }
}

public abstract class Magic : MonoBehaviour
{
	protected string particlePath;
	protected GameObject particle;


	protected virtual void Start()
	{
		ActivateMagic();
	}
	/// <summary>
	/// 生成特效，实现效果
	/// </summary>
	public abstract void ActivateMagic();

	private void OnDestroy()
	{
		if (particle != null)
		{
			Destroy(particle);
		}
	}
	protected virtual void Damage()
	{
	}
	protected void CreateParticle(string particlePath)
	{
		if (particle == null)
		{
			Role role = GetComponent<Role>();
			Vector3 position = transform.position;
			position.y += role.halfHight;
			particle = role.SetChilrenGameObect(Resources.Load<GameObject>(particlePath), position);
		}
	}

}

