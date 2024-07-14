using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class PoolManager : MonoSingleton<PoolManager>
{
	[SerializeField] private PoolObjectDataSO _poolData;

	private Dictionary<string, Pool> _pools;

	protected override void Awake()
	{
		CreatePool();
	}

	private void CreatePool()
	{
		if (_poolData is null) return;

		foreach (PoolData data in _poolData.poolingList)
		{
			Pool pool = new Pool(transform, data.prefab, data.preCreateCount);
			_pools.Add(data.prefab.name, pool);
		}
	}

	public PoolMono Pop(string name)
	{
		PoolMono ret = null;

		if (_pools.ContainsKey(name))
		{
			ret = _pools[name].Pop();
		}

		return ret;
	}

	public void Push(PoolMono push)
	{
		if (_pools.ContainsKey(push.name))
		{
			_pools[push.name].Push(push);
		}
	}
}
