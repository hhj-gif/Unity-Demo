using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MagicManager
{
	private static Dictionary<MagicType, Type> MagicDictionary = new Dictionary<MagicType, Type>();
	public static Type GetMagic(MagicType magicType)
	{
		if (!MagicDictionary.ContainsKey(magicType))
		{
			var magicClass = typeof(Magic).Assembly.GetTypes().FirstOrDefault(x => x.GetCustomAttributes(typeof(MagicAttribute),true).Any(k => ((MagicAttribute)k).MagicType == magicType));
			if (magicClass == null)
			{
				Debug.Log("类型不存在");
			}
			MagicDictionary.Add(magicType, magicClass);
		}
		return MagicDictionary[magicType]; 
	}
}
