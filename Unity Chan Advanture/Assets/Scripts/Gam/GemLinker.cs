using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GemLinker : MonoBehaviour
{
	LineRenderer lineRenderer;
	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}
	
	public void SetStartPoint(Vector3 startPoint)
	{
		lineRenderer.SetPosition(0,startPoint);
	}

	public void SetPoints(Vector3[] points)
	{
		lineRenderer.positionCount = points.Length;
		lineRenderer.SetPositions(points);
	}

}
