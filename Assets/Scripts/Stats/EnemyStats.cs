using System;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        base.Die();
        EventManager.InvokeEvent("EnemyDied", gameObject, EventArgs.Empty);
        GameManager.Instance.KillObject(gameObject);
    }
}
