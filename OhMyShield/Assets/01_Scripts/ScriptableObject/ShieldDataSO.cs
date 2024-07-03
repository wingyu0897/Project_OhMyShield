using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ShieldData")]
public class ShieldDataSO : ScriptableObject
{
    [SerializeField] private string _shieldName;
    public string ShieldName => _shieldName;

    [SerializeField] private Sprite _shieldImage;
    public Sprite ShieldImage => _shieldImage;
}
