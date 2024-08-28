using Pooling;
using System;
using UnityEngine;

public abstract class AttackBase : PoolMono
{
    [Header("Attack")]
    [SerializeField] protected int _damage = 1;
    public int Damage => _damage;
    [SerializeField] protected float _attackTime;
    public float AttackTime => _attackTime;
    [SerializeField] protected float _cooltime;
    public float Cooltime => _cooltime;
    protected float _lastAttackTime = float.MinValue;
    
    public virtual event Action<AttackBase> OnAttackEnd;


    public float RemainingCooltime() => Mathf.Clamp(_lastAttackTime + _cooltime - Time.time, 0, _cooltime);
    public virtual bool IsNotCool() => Time.time > _lastAttackTime + _cooltime;
    public virtual bool CanAttack() => AttackManager.CanAttack(_attackTime) && IsNotCool();

    public virtual bool DoAttack(Agent target)
	{
        if (CanAttack())
		{
            AttackManager.Instance.AddAttack(_attackTime);
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
