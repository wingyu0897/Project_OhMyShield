using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    [SerializeField] private float _attackTime;
    public float AttackTime => _attackTime;

    public virtual void DoAttack(Agent target)
	{
        if (AttackManager.Instance.CanAttack(this))
		{
            AttackManager.Instance.Attack(this, target);
		}
	}

    public abstract void Attack(Agent target);
}
