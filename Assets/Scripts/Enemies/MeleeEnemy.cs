using UnityEngine;

public class MeleeEnemy : EnemyAI
{
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
