using UnityEngine;
using Enums;

public class GameManager : MonoSingleton<GameManager>
{
	[Header("Settings")]
	[SerializeField] private GameObject _mainCanvas;

	[HideInInspector] public GameFlow gameFlow;

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

		EditorLog.Log("GameManager Initialized");
	}
}
