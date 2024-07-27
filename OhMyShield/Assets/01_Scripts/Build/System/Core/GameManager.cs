using UnityEngine;
using Enums;

public class GameManager : MonoSingleton<GameManager>
{
	[Header("Prefabs")]
	[SerializeField] private GameObject _mainCanvas;

	[Header("Datas")]
	[SerializeField] private PoolObjectDataSO _poolList;

	[Header("Instances")]
	[HideInInspector] public GameFlow gameFlow;
	[HideInInspector] public PoolManager poolManager;

	protected override void Awake()
	{
		base.Awake();

		DontDestroyOnLoad(gameObject);

		InitializeGame();

		gameFlow.ChangeScene(GAME_STATE.Game);
	}

	private void InitializeGame()
	{
		gameFlow = new GameFlow();

		poolManager = new PoolManager();
		poolManager.CreatePool(_poolList, transform);

		EditorLog.Log("GameManager Initialized");
	}
}
