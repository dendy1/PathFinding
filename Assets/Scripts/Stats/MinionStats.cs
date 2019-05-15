public class MinionStats : CharacterStats
{
   public override void Die()
   {
      base.Die();
      GameManager.Instance.KillObject(gameObject);
   }
}
