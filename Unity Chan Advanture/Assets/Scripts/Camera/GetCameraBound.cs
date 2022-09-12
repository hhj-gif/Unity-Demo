using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetCameraBound : MonoBehaviour
{
    CinemachineConfiner confiner;
    public void GetBound()
    {
        confiner.m_BoundingVolume = GameObject.FindGameObjectWithTag("Bound").GetComponent<Collider>();
    }

	private void OnEnable()
	{
        if(LevelManager.Instance!=null)
            LevelManager.Instance.LoadingSceneComplete += GetBound;
    }
	private void OnDisable()
	{
        if(LevelManager.Instance!=null)
            LevelManager.Instance.LoadingSceneComplete -= GetBound;
    }

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner>();
    }
}
