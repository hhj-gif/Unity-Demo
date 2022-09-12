using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDamage : MonoBehaviour
{
	public int damageCount = 1;
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<Player>().Damaged(damageCount);
		}
	}
}
