using UnityEngine;
using Enums;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
	[Header("Prefabs")]
	[SerializeField] private GameObject _mainCanvas;

	[Header("Datas")]
	[SerializeField] private PoolObjectDataSO _poolList;

	[Header("Instances")]
	[HideInInspector] public GameFlow gameFlow;
	[HideInInspector] public PoolManager poolManager;

	private List<ISystemComponent> _systemComponents;

	private T AddSystemComponent<T>() where T : Component, ISystemComponent
	{
		T systemComp = gameObject.AddComponent<T>();
		_systemComponents.Add(systemComp);
		return systemComp;
	}

	private void InitializeGame()
	{
		_systemComponents = new List<ISystemComponent>();
		AddSystemComponent<UIViewController>();

		gameFlow = new GameFlow();
		gameFlow.OnSceneChanged += OnSceneChanged;

		poolManager = gameObject.AddComponent<PoolManager>();
		poolManager.CreatePools(_poolList, transform);

		DontDestroyOnLoad(gameObject);
		EditorLog.Log("GameManager Initialized");
	}

	public void ChangeScene(GAME_STATE state)
	{
		gameFlow.ChangeScene(state);
	}

	private void OnSceneChanged(GAME_STATE state)
	{
		foreach (ISystemComponent sys in _systemComponents)
		{
			sys.OnSceneChanged(state);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		InitializeGame();

		gameFlow.ChangeScene(GAME_STATE.Menu);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if (gameFlow is not null)
		{
			gameFlow.OnSceneChanged -= OnSceneChanged;
		}
	}
}
