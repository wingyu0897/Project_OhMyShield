using Pooling;
using System.Collections.Generic;

public abstract class Agent : PoolMono
{
    public abstract Agent Target { get; set; }

    public List<AttackBase> attacks;
}
