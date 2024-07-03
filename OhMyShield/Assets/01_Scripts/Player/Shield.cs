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
	}

    public void SetShieldData(ShieldDataSO inShieldData)
	{
        _myShield = inShieldData;
	}
}
