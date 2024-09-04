using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager>
{
    [SerializeField] private PoolObjectDataSO _effectPoolList;

	private void Start()
	{
		PoolManager.Instance.CreatePools(_effectPoolList, transform);
	}

	public MonoParticle PopEffect(string name, bool activeOnPop = true)
	{
		MonoParticle particle = PoolManager.Instance.Pop(name) as MonoParticle;

		if (activeOnPop)
			particle.Particle.Play();
		
		return particle;
	}

	public void PushEffect(MonoParticle particle)
	{
		PoolManager.Instance.Push(particle);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if (!PoolManager.InstanceIsNull)
			PoolManager.Instance.RemovePools(_effectPoolList);
	}
}
