using UnityEngine;

public class Player : Agent, IHealth
{
	[SerializeField] private AgentDataSO _agentData;

	private float _health;
	public float Health { get => _health; set => _health = value; }

	private Agent _target;
	public override Agent Target { get => _target; set => _target = value; }

	public void ModifyHealth(float change)
	{
		_health += change;
		_health = Mathf.Clamp(_health, 0, _agentData.health);

		EditorLog.Log($"Player Health : {_health}");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out AttackBase attack))
		{
			ModifyHealth(-attack.Damage);
		}
	}

	public override void PoolInit()
	{
		_health = _agentData.health;
	}
}
