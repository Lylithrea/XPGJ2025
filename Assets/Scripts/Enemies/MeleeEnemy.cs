using UnityEngine;

public class MeleeEnemy : EnemyAI
{
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        PlayerManager.Instance.Damage(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
