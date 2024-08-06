
using System;

public interface IHealth
{
    public event Action OnDie;

    public float Health { get; set; }

    public void ModifyHealth(float change);
    public void Dead();
}
