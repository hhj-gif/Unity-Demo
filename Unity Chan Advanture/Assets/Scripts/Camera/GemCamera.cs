using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class GemCamera : MonoBehaviour
	{
		private Camera rawCamera;

		private RenderTexture targetTexture;

		private GameObject gem;

		private void Awake()
		{
			rawCamera = GetComponent<Camera>();
			targetTexture = new RenderTexture(256, 256, 0);
			rawCamera.targetTexture = targetTexture;
		}

		public void SetGam(GameObject gam)
		{
			if (this.gem != null)
			{
				Destroy(this.gem);
			}
			else
			{
				this.gem = gam;
				GameObject.Instantiate(gam, this.transform.position + new Vector3(0, 0, 0.8f), Quaternion.Euler(-90, 0, 0), this.transform);
			}
		}

		public RenderTexture GetTexture()
		{
			return targetTexture;
		}
	}
