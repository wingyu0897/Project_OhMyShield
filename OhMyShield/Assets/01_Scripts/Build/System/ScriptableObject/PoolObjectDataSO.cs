using System.Collections.Generic;
using UnityEngine;
using Pooling;

[System.Serializable]
public class PoolData
{
    public PoolMono prefab;
    public int preCreateCount;
}

[CreateAssetMenu(menuName = "SO/Pool/ScriptableObjectData")]
public class PoolObjectDataSO : ScriptableObject
{
    public List<PoolData> poolingList;
}
