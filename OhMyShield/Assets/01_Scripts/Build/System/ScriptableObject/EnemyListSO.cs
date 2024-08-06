using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/List")]
public class EnemyListSO : ScriptableObject
{
    public List<Enemy> enemies;
}
