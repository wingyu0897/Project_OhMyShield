using Pooling;
using System;
using UnityEngine;

public abstract class AttackBase : PoolMono
{
    [Header("Attack")]
    public int Damage = 1;
    public float AttackTime;
    public float CoolTime;
    protected float _lastAttackTime = float.MinValue;
    
    public virtual event Action<AttackBase> OnAttackEnd;


    public float RemainingCooltime() => Mathf.Clamp(_lastAttackTime + CoolTime - Time.time, 0, CoolTime);
    public virtual bool IsNotCool() => Time.time > _lastAttackTime + CoolTime;
    public virtual bool CanAttack() => AttackManager.CanAttack(AttackTime) && IsNotCool();

    public virtual bool DoAttack(Agent target)
	{
        if (CanAttack())
		{
            AttackManager.Instance.AddAttack(AttackTime);
            _lastAttackTime = Time.time;

            Attack(target);
            return true;
		}

        return false;
	}

    public abstract void Attack(Agent target);
    public abstract void StopAttack();
    public virtual void Blocked() => StopAttack();

    public override void PoolInit() { }
}
