using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T _instance;

    public static T Instance
	{
		get
		{
			if (_instance is null)
			{
				_instance = FindObjectOfType<T>();

				if (_instance is null)
				{
					GameObject temp = new GameObject($"{typeof(T)}Instance");
					_instance = temp.AddComponent<T>();
					Debug.LogWarning("Temp Object Created: No Instance.");
				}
			}

			return _instance;
		}
	}

	protected virtual void Awake()
	{
		if (_instance is null)
		{
			_instance = this as T;
		}
		else
		{
			if (_instance != this)
			{
				Debug.LogWarning("Object Destroyed: Multiple Singleton Instance is running.");
				Destroy(gameObject);
			}
		}
	}
}
