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
    [SerializeField] private Transform _enemyPosition;
	[SerializeField] private Enemy _testEnemy;
	[SerializeField] private EnemyListSO _enemyList;

	private Enemy _currentEnemy;

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

	private void SpawnPlayer()
	{
		_player = Instantiate(_playerPrefab, _playerPosition.position, Quaternion.identity, null);
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

	private void OnEnemyDieHandler()
	{
		_currentEnemy.OnDie -= OnEnemyDieHandler;
		_currentEnemy = null;
		_player.Target = null;
		StartCoroutine(DelaySpawnEnemy());
	}

	private void SelectEnemy()
	{
		Enemy enemy = _enemyList.enemies[Random.Range(0, _enemyList.enemies.Count)];
		SpawnEnemy(enemy);
		SetTarget();
	}

	IEnumerator DelaySpawnEnemy()
	{
		yield return new WaitForSeconds(1f);
		SelectEnemy();
	}
}
