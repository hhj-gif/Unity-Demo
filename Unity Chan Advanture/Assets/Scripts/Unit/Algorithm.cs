using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public static class Algorithm
{
	public static void QuickSort<T>(List<T> data,int start,int end, Func<T,T,bool> cmp)
	{
		if (start >= end)
		{
			return;
		}
		int l = start;
		int r = end;
		T p = data[start];
		while (l < r)
		{
			while (l < r && !cmp(p, data[r]))
			{
				r--;
			}
			while (l < r && cmp(p,data[l]))
			{
				l++;
			}
			if (l < r)
			{
				T temp = data[l];
				data[l] = data[r];
				data[r] = temp;
			}
		}
		data[start] = data[l];
		data[l] = p;
		QuickSort<T>(data, start, l - 1, cmp);
		QuickSort<T>(data, r+1, end, cmp);
	}
	public static void SelectSort<T>(List<T> data, Func<T, T, bool> cmp)
	{
		for (int i = 0; i < data.Count; i++)
		{
			int k = i;
			for(int j = i + 1; j < data.Count; j++)
			{
				if (cmp(data[k], data[j]))
				{
					k = j;
				}
			}
			T temp = data[i];
			data[i] = data[k];
			data[k] = temp;
		}
	}
	public static void InsertSort<T>(List<T> data, Func<T, T, bool> cmp)
	{
		int index = data.Count - 1;
		T t = data[index];
		int j = 0;
		for (int i = index-1; i >=0; i--)
		{
			if (cmp(data[i], t))
			{
				j = i+1;
				break;
			}
			else
			{
				data[i + 1] = data[i];
			}
		}
		data[j] = t;
	}
	public static void Test()
	{
		List<int> testdata = new List<int>() { 3, 5, 6, 7, 13, 654, 3, 123, 13,68,62,4,63,11,55,663,157,541 };
		QuickSort<int>(testdata, 0, testdata.Count - 1, (a, b) => { return a > b; });
		StringBuilder stringBuilder = new StringBuilder();
		foreach (var item in testdata)
		{
			stringBuilder.Append(item.ToString());
			stringBuilder.Append(" ");
		}
		Debug.Log(stringBuilder.ToString());
	}
	public static float CrossY(Vector3 A,Vector3 B)
	{
		return A.z * B.x - B.z * A.x;

	}
	public static bool IsIntersectionWithRay(Vector3 A,Vector3 B,Vector3 originPoint, Vector3 direction)
	{
		Vector3 ray = originPoint + direction * 1000f;
		float Across = CrossY(A - originPoint, ray - originPoint);
		float Bcross = CrossY(B - originPoint, ray - originPoint);
		if (Across * Bcross > 0)
		{
			return false;
		}
		float Ccross = CrossY(B - A, originPoint - A);
		float Dcross = CrossY(B - A, ray - A);
		if (Ccross * Dcross > 0)
		{
			return false;
		}
		return true;
	}
	public static int Quadrant(Vector3 v)
	{
		if (v.x > 0 && v.z >= 0)
		{
			return 4;
		}
		else if (v.x <= 0 && v.z > 0)
		{
			return 3;
		}
		else if (v.x < 0 && v.z <= 0)
		{
			return 2;
		}
		else if (v.x >= 0 && v.z < 0)
		{
			return 1;
		}
		return 0;
	}
	public static int Comparer(Vector3 center, Gem a, Gem b)
	{

		if (a.transform.position.x == 0 && b.transform.position.x == 0)
			return a.transform.position.z > b.transform.position.z ? 1 : -1;

		Vector3 OA = a.transform.position - center;
		Vector3 OB = b.transform.position - center;
		int i = Quadrant(OA);
		int j = Quadrant(OB);
		if (i != j)
		{
			return i - j;
		}
		float det = OA.x * OB.z - OA.z * OB.x;
		if (det > 0)
		{
			return 1;
		}
		else if (det < 0)
		{
			return -1;
		}
		if (OA.magnitude - OB.magnitude > 0)
		{
			return 1;
		}
		else
		{
			return -1;
		}
	}


}
