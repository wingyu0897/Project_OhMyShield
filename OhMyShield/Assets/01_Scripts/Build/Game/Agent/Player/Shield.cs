using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private ShieldDataSO _myShield;


    [SerializeField] private float _shieldDistance = 3f;

	private void Awake()
	{
		CreateShield();
	}

	private void CreateShield()
	{
        GameObject shield = new GameObject("Shield Body");
        shield.transform.SetParent(transform);
		shield.transform.localPosition = new Vector3(_shieldDistance, 0);
		shield.layer = LayerMask.NameToLayer("Shield");

		SpriteRenderer sprite = shield.AddComponent<SpriteRenderer>();
		sprite.sprite = _myShield.ShieldImage;

		shield.AddComponent<PolygonCollider2D>();
	}

	public void SetDirection(float angle)
	{
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

    public void SetShieldData(ShieldDataSO inShieldData)
	{
        _myShield = inShieldData;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out AttackBase attack))
		{
			attack.StopAttack();
		}
	}
}
