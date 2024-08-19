using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class PoolManager : MonoSingleton<PoolManager>
{
	private Dictionary<string, Pool> _pools;

	public virtual void CreatePools(PoolObjectDataSO poolList, Transform parent)
	{
		if (poolList is null) return;

		if (_pools is null)
			_pools = new Dictionary<string, Pool>();

		foreach (PoolData data in poolList.poolingList)
		{
			CreatePool(parent, data.prefab, data.preCreateCount);
		}
	}

	public virtual void CreatePool(Transform parent, PoolMono prefab, int createCnt = 0)
	{
		if (prefab is null) return;

		if (_pools is null)
			_pools = new Dictionary<string, Pool>();

		Pool pool = new Pool(parent, prefab, createCnt);
		_pools.Add(prefab.name, pool);
	}

	public PoolMono Pop(string name)
	{
		PoolMono ret = null;

		if (_pools.ContainsKey(name))
		{
			ret = _pools[name].Pop();
		}
		else
		{
			EditorLog.Log($"No Pool({name})");
		}

		return ret;
	}

	public void Push(PoolMono push)
	{
		if (_pools != null || _pools.ContainsKey(push.name))
		{
			_pools[push.name].Push(push);
		}
	}
}
