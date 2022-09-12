using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Role : MonoBehaviour
{
	public float speed;
	[SerializeField] float Hight;
	public float halfHight => Hight / 2;
	public abstract void Damaged(int damageCount, bool isHit = true);
	public abstract void ChangeSpeed(int changeNumber);
	public abstract GameObject SetChilrenGameObect(GameObject gameObject,Vector3 position);
}
