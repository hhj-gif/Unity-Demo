using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    protected GameObject particle;
	private float continueTime;
	private float time;
	protected int effect;
	protected Role role;
	bool isStart = false;
	public void StartBuff(int time, int effect)
	{
		this.continueTime = time;
		this.effect = effect;
		role = GetComponent<Role>();
		BuffStart();
		isStart = true;
	}

	protected void CreateParticle(string particlePath)
	{
		Vector3 position = transform.position;
		position.y += role.halfHight;
		particle = role.SetChilrenGameObect(Resources.Load<GameObject>(particlePath), position);
	}

	private void Update()
	{
		if (isStart)
		{
			if (time < this.continueTime)
			{
				BuffEffect();
				this.time += Time.deltaTime;
			}
			else
			{
				Destroy(this);
			}
		}
	}

	private void OnDestroy()
	{
		if (particle != null)
		{
			Destroy(particle);
		}
		BuffEnd();
	}
	protected abstract void BuffStart();

	protected abstract void BuffEnd();
	
	protected abstract void BuffEffect();
}
