using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase }
    public EnemyState currentState;

    [Header("Referências")]
    public PatrolPath patrolPath;
    public Transform player;

    [Header("Configurações")]
    public float maxDistance = 20f;
    public float waitTimeAtWaypoint = 1f;

    private NavMeshAgent agent;
    private EnemyVision enemyVision;
    private Vector3 currentWaypoint;
    private float waitTimer = 0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyVision = GetComponent<EnemyVision>();
    }
    //verifica stopping inicial se ele é maior que >0
    private void Start()
    {
        
        currentState = EnemyState.Patrol;

        if (patrolPath != null && patrolPath.waypoints.Length > 0)
        {
            currentWaypoint = patrolPath.GetCurrentWaypoint();
            agent.SetDestination(currentWaypoint);
        }
    }

    private void Update()
    {
        bool canSeePlayer = enemyVision != null && enemyVision.PlayerDetected;
        //calcula a distância até o jodador vector3A ,vector3B (jogador) 

        float distanceToPlayer = player != null ? Vector3.Distance(transform.position, player.position) : float.MaxValue;
        //referencia ao script da visão do inimigo




        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                if (canSeePlayer)
                    currentState = EnemyState.Chase;//inicia a perseguição,se

                break;

            case EnemyState.Chase:
                Chase();
                if (!canSeePlayer)//se não a visão detectar o jogador
                    ReturnToPatrol();//volta a patrulha

                break;
        }
        if (distanceToPlayer > maxDistance)//ultrapassar da distancia pré estabelecida

        {
            ReturnToPatrol();
            return;
        }
    }

    private void Patrol()
    {
        if (patrolPath == null) return;
        //Chegou no waypoint atual ?

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        //<= esta perto do waypoint
        //esta lógica serve para se o inimigo calculou a rota e esta dentro dos waypoints, ele chamará para o próximo ponto
        //! serve para indicar negação logica, não está calculando um caminho
        //! = ==false
        //&& E lógico . só retorna se ambos foram verdadeiros  , não está calculado e chegou no destino

        {
            waitTimer += Time.deltaTime;//waiteTime inicial é 0

            if (waitTimer >= waitTimeAtWaypoint)
            {
                waitTimer = 0f;
                // Pega o próximo waypoint usando o Path

                currentWaypoint = patrolPath.GetNextWaypoint();
                agent.SetDestination(currentWaypoint);
            }
        }
    }

    private void Chase()
    {
        if (player == null) return;

        if (Vector3.Distance(agent.destination, player.position) > 0.1f)//maior que 0.1 porque o valor minimo é 0
            //vector3 precisa de a e b
            agent.SetDestination(player.position);//vai em direção ao player
    }

    private void ReturnToPatrol()
    {
        currentState = EnemyState.Patrol;

        if (patrolPath != null)
        {
            currentWaypoint = patrolPath.GetCurrentWaypoint();
            agent.SetDestination(currentWaypoint);
        }
        else
        {
            agent.ResetPath();
        }
    }
}