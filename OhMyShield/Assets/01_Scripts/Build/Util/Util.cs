using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T FindChild<T>(GameObject obj, string name = null, bool recursive = false) where T : Object
	{
		if (obj == null) return null;

		if (!recursive)
		{
			for (int i = 0; i < obj.transform.childCount; ++i)
			{
				Transform trm = obj.transform.GetChild(i);
				if (string.IsNullOrEmpty(name) || trm.name == name)
				{
					T component = trm.GetComponent<T>();
					if (component)
						return component;
				}
			}
		}
		else
		{
			foreach (T component in obj.GetComponentsInChildren<T>())
			{
				if (string.IsNullOrEmpty(name) || component.name == name)
				{
					return component;
				}
			}
		}

		return null;
	}
}
