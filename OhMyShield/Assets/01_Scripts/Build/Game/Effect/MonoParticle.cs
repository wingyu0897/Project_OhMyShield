using Pooling;
using UnityEngine;

[RequireComponent(typeof(MonoParticle))][DisallowMultipleComponent]
public class MonoParticle : PoolMono
{
	private ParticleSystem _particle;
	public ParticleSystem Particle { 
		get {
			if (_particle == null) 
				_particle = GetComponent<ParticleSystem>();
			return _particle;
		} }

	public override void PoolInit()
	{
		if (Particle.isPlaying)
			Particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
	}

	private void OnParticleSystemStopped()
	{
		EffectManager.Instance.PushEffect(this);
	}
}
