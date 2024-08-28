using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
	protected Dictionary<Type, List<UnityEngine.Object>> _uiElements = new();

    public abstract void Initialize();

    public virtual void Show() => gameObject.SetActive(true);

    public virtual void Hide() => gameObject.SetActive(false);

	public virtual void Bind<T>(string name) where T : UnityEngine.Object
	{
		List<UnityEngine.Object> objects;
		if (_uiElements.ContainsKey(typeof(T)))
			objects = _uiElements[typeof(T)];
		else
		{
			objects = new List<UnityEngine.Object>();
			_uiElements.Add(typeof(T), objects);
		}

		T comp = Util.FindChild<T>(gameObject, name, true);
		if (comp != null)
		{
			objects.Add(comp);
		}
	}

	public virtual T Get<T>(string name) where T : UnityEngine.Object
	{
		if (_uiElements.ContainsKey(typeof(T)))
		{
			T elem = _uiElements[typeof(T)].Find(ui => ui.name.Equals(name)) as T;
			return elem;
		}
		else
		{
			EditorLog.LogWarning($"No {name} in this UIView.");
			return null;
		}
	}
}
