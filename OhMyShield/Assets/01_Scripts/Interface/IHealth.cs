
public interface IHealth
{
    public float Health { get; set; }

    public virtual void ModifyHealth(float value)
	{
		Health += value;
		if (Health < 0)
		{
			Health = 0;
		}
	}
}
