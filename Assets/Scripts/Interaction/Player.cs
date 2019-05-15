using UnityEngine;

public class Player : Interactable
{
    private CharacterStats _stats;

    public override void Interact(Transform target)
    {
        base.Interact(target);
        
        if (target.CompareTag("Enemy"))
            Attack(target);
    }
    
    public override void Initialize()
    {
        _stats = GetComponent<CharacterStats>();
    }

    public void Attack(Transform target)
    {
        Combative combative = target.GetComponent<Combative>();
        if (combative)
        {
            Debug.Log(name + " attacked by " + target.name);
            combative.Attack(_stats);
            PlayerManager.Instance.UpdateHealthBar();
        }
    }  
}
