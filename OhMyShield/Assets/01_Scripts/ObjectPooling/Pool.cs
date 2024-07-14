using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class Pool
    {
        private Transform _parent;
        private PoolMono _prefab;
        private Stack<PoolMono> _pool;

        public Pool(Transform parent, PoolMono prefab, int count)
		{
            _parent = parent;
            _prefab = prefab;
            _pool = new Stack<PoolMono>();

            for (int i = 0; i < count; ++i)
			{
                PoolMono instance = GameObject.Instantiate(_prefab, _parent);
                instance.name = _prefab.name;
                instance.gameObject.SetActive(false);
                _pool.Push(instance);
			}
		}

        public void Push(PoolMono obj)
		{
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
		}

        public PoolMono Pop()
		{
            PoolMono obj;
            if (_pool.Count > 0)
			{
                obj = _pool.Pop();
			}
            else
			{
                obj = GameObject.Instantiate(_prefab, _parent);
                obj.name = _prefab.name;
            }

            obj.gameObject.SetActive(true);
            obj.PoolInit();

            return obj;
		}
    }
}
