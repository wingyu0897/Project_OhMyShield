using UnityEngine;

public class OnGameScene : MonoSingleton<OnGameScene>
{
    [SerializeField] private Transform _enemyPositionTrm;
	[SerializeField] private Player _player;
	[SerializeField] private Enemy _testEnemy;

	private void Start()
	{
		Init();
	}

	public void Init()
	{
		Enemy enemy = GameManager.Instance.poolManager.Pop(_testEnemy.gameObject.name) as Enemy;
		enemy.transform.position = _enemyPositionTrm.position;

		_player.Target = enemy;
		enemy.Target = _player;
	}
}
