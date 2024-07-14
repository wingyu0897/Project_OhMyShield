using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    // 방향 설정
    // 힘 설정
    

    private Rigidbody2D _myRigid;

	private void Awake()
	{
		_myRigid = GetComponent<Rigidbody2D>();
	}

	public virtual void SetValue(Vector2 direction, float power)
	{

	}
}
