using Pooling;
using System;
using System.Collections.Generic;

public abstract class Agent : PoolMono, IHealth
{
    public abstract Agent Target { get; set; }
	public abstract float Health { get; set; }

	protected bool _isDead = false;

	public List<AttackBase> attacks;

	public abstract event Action OnDie;

	private void Awake()
	{
		foreach (AttackBase attack in transform.Find("Attack")?.GetComponents<AttackBase>())
		{
			attacks.Add(attack);
		}
	}

	public void ModifyHealth(float change)
	{
		Health += change;
		if (Health <= 0)
		{
			Dead();
			_isDead = true;
			Health = 0;
		}
	}

	public abstract void Dead();
}
