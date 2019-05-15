using System.Collections;
using UnityEngine;

public class Combative : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 2f;
    [SerializeField] private float attackDelay = 0f;

    private float _attackCooldown = 0f;
    private CharacterStats _stats;

    private void Start()
    {
        _stats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        _attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (_attackCooldown <= 0)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));
            _attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats targetStart, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetStart.TakeDamage(_stats.Damage);
    }
}
