using Pooling;
using UnityEngine;

public abstract class AttackBase : PoolMono
{
    [SerializeField] protected int _damage = 1;
    public int Damage => _damage;

    [SerializeField] protected float _attackTime;
    public float AttackTime => _attackTime;

    [SerializeField] protected float _cooltime;
    public float Cooltime => _cooltime;

    protected float _lastAttackTime = float.MinValue;

    public virtual bool IsAbleToAttack()
	{
        return Time.time > _lastAttackTime + _cooltime;
    }

    public virtual bool DoAttack(Agent target)
	{
        if (AttackManager.Instance.CanAttack(this) && IsAbleToAttack())
		{
            AttackManager.Instance.Attack(this, target);
            _lastAttackTime = Time.time;
            return true;
		}

        return false;
	}

    public abstract void Attack(Agent target);
    public abstract void StopAttack();

    public abstract override void PoolInit();
}
