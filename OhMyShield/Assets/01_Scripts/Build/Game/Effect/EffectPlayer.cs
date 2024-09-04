using Enums;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] private MonoParticle particle;

	[Header("Options")]
	public PARTICLE_OPTION particleOption = PARTICLE_OPTION.None;

    public void Play()
	{
		MonoParticle pop = EffectManager.Instance.PopEffect(particle.name, false);
		pop.transform.position = transform.position;

		float angle = particleOption switch
		{
			PARTICLE_OPTION.None => 0,
			PARTICLE_OPTION.AngleForward => 90f,
			PARTICLE_OPTION.AngleBackward => 270f,
			PARTICLE_OPTION.AngleLeft => 180f,
			PARTICLE_OPTION.AngleRight => 0,
			_ => 90f
		};

		pop.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, angle);
		pop.Particle.Play();
	}
}
