using System.Collections;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{

	[Header("Player")]
	[SerializeField] private Transform _playerPosition;
	[SerializeField] private Player _playerPrefab;
	private Player _player;
	public Player Player => _player;

	[Header("Enemy")]
	[SerializeField] private EnemyListSO _enemyList;
	[Space]
    [SerializeField] private Transform _enemyPosition;
	[SerializeField] private Enemy _testEnemy;
	private Enemy _currentEnemy;

	[Header("Values")]
	private int _enemyCount = 0;

	private void Start()
	{
		Init();
	}

	public void Init()
	{
		SpawnPlayer();
		SpawnEnemy(_testEnemy);
		SetTarget();
	}

	#region Spawn
	private void SpawnPlayer()
	{
		_player = Instantiate(_playerPrefab, _playerPosition.position, Quaternion.identity, null);
		_player.OnDie += OnPlayerDieHandler;
	}

	private void SpawnEnemy(Enemy prefab)
	{
		_currentEnemy = PoolManager.Instance.Pop(prefab.gameObject.name) as Enemy;
		_currentEnemy.transform.position = _enemyPosition.position;
		_currentEnemy.OnDie += OnEnemyDieHandler;
	}

	private void SetTarget()
	{
		_player.Target = _currentEnemy;
		_currentEnemy.Target = _player;
	}

	private void SelectEnemy()
	{
		Enemy enemyPrefab;
		if ((_enemyCount + 1) % 5 == 0)
			enemyPrefab = _enemyList.bosses[Random.Range(0, _enemyList.bosses.Count)];
		else
			enemyPrefab = _enemyList.enemies[Random.Range(0, _enemyList.enemies.Count)];

		SpawnEnemy(enemyPrefab);
		SetTarget();
	}

	IEnumerator DelaySpawnEnemy()
	{
		yield return new WaitForSeconds(1f);
		SelectEnemy();
	}

	private void DespawnAll()
	{
		_player.OnDie -= OnPlayerDieHandler;
		_currentEnemy.OnDie -= OnEnemyDieHandler;

		_player.Despawn();
		_currentEnemy.Despawn();
	}
	#endregion

	private void OnEnemyDieHandler()
	{
		_currentEnemy.OnDie -= OnEnemyDieHandler;

		_enemyCount++;
		_currentEnemy = null;
		_player.Target = null;

		StartCoroutine(DelaySpawnEnemy());
	}

	private void OnPlayerDieHandler()
	{
		_player.OnDie -= OnPlayerDieHandler;
		DespawnAll();
		GameManager.Instance.gameFlow.ChangeScene(Enums.GAME_STATE.Menu);
	}
}
