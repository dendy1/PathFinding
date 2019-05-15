using UnityEngine;
using UnityEngine.AI;

public class EnemyInteraction : Interactable
{
    private Combative _combative;

    private void Start()
    {
        _combative = GetComponent<Combative>();
    }

    public override void Interact()
    {
        base.Interact();
        
        Combative playerComb = PlayerManager.Instance.Player.GetComponent<Combative>();
        GameObject[] minions = PlayerManager.Instance.Minions;

        foreach (var minion in minions)
        {
            Combative minionComb = minion.GetComponent<Combative>();
            if (minionComb)
            {
                minionComb.Attack(_combative);
            }
        }
        
        if (playerComb)
        {
            playerComb.Attack(_combative);
        }
    }
}
