public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        base.Die();
        GameManager.Instance.Restart();
    }
}
