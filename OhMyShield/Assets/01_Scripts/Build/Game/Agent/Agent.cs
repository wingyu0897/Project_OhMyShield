using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public abstract Agent Target { get; }
}
