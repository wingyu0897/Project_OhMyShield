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
		if (prefab is null || _pools.ContainsKey(prefab.name)) return;

		if (_pools is null)
			_pools = new Dictionary<string, Pool>();

		Pool pool = new Pool(parent, prefab, createCnt);
		_pools.Add(prefab.name, pool);
	}

	public virtual void RemovePools(PoolObjectDataSO poolList)
	{
		if (poolList is null) return;

		if (_pools is null) return;

		foreach (PoolData data in poolList.poolingList)
		{
			RemovePool(data.prefab);
		}
	}

	public virtual void RemovePool(PoolMono prefab)
	{
		if (_pools is null || !_pools.ContainsKey(prefab.name))
			return;

		_pools[prefab.name].DestroyPool();
		_pools.Remove(prefab.name);
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
			if (_pools[push.name].ContainValue(push)) return;

			_pools[push.name].Push(push);
		}
	}
}
