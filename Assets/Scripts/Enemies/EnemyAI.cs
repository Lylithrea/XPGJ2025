using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public GameObject player;

    public enum AiType
    {
        Melee,
        Ranged
    }

    public AiType Type = AiType.Melee;

    public enum AiState
    {
        Aggro,
        Attack,
        Dead
    }

    public AiState State = AiState.Aggro;

    [SerializeField] private float m_Speed = 1;
    [SerializeField] private float m_AttackSpeed = 1;
    [SerializeField] private float m_AttackRange = 1;
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private Animator m_Animator;

    private float attackTimer = 0;

    public int health = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(m_Agent == null)
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }
        State = AiState.Aggro;
        m_Agent.speed = m_Speed;
        if(m_Animator == null)
        {
            m_Animator = GetComponent<Animator>();
        }
        m_Animator.SetBool("Melee", Type == AiType.Melee);
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case AiState.Aggro:
                ChasePlayer();
                break;
            case AiState.Attack:
                AttackPlayer();
                break;
            case AiState.Dead:
                break;
        }
    }

    private void ChasePlayer()
    {
        if (!m_Agent.isOnNavMesh)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= m_AttackRange)
        {
            State = AiState.Attack;
            m_Agent.isStopped = true;
        }
        else
        {
            m_Agent.isStopped = false;
            m_Agent.SetDestination(player.transform.position);
        }
    }

    protected virtual void AttackPlayer()
    {
        m_Animator.SetTrigger("Attack");
        attackTimer += Time.deltaTime;

        if (attackTimer >= 1 / m_AttackSpeed)
        {
            attackTimer = 0;
            Debug.Log("Attacking player");
        }

        if (Vector3.Distance(transform.position, player.transform.position) > m_AttackRange)
        {
            State = AiState.Aggro;
            m_Agent.isStopped = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
