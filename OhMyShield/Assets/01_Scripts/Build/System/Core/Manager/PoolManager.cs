using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class PoolManager
{
	private Dictionary<string, Pool> _pools;

	public virtual void CreatePool(PoolObjectDataSO poolList, Transform parent)
	{
		if (poolList is null) return;

		_pools = new Dictionary<string, Pool>();

		foreach (PoolData data in poolList.poolingList)
		{
			Pool pool = new Pool(parent, data.prefab, data.preCreateCount);
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
