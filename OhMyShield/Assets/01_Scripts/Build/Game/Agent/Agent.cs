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
