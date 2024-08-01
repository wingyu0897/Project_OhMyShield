
public interface IHealth
{
    public float Health { get; set; }

    public void ModifyHealth(float change);
    public void Dead();
}
