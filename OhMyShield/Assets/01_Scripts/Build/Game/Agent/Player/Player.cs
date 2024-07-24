using UnityEngine;

public class Player : Agent, IHealth
{
	[SerializeField] private AgentDataSO _agentData;

	private float _health;
	public float Health { get => _health; set => _health = value; }

	private Agent _target;
	public override Agent Target => _target;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		

		_health = _agentData.health;
	}
}
