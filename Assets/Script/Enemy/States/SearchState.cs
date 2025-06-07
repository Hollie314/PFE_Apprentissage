using UnityEngine;
using UnityEngine.AI;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;

    public override void Enter()
    {
        searchTimer = 0;
        moveTimer = 0;

        enemy.Agent.SetDestination(enemy.LastKnowPos);
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
            return;
        }

        searchTimer += Time.deltaTime;
        moveTimer += Time.deltaTime;

        // toutes les 3 à 5 secondes, cherche autour de la dernière position connue
        if (moveTimer > Random.Range(3f, 5f))
        {
            Vector3 randomDir = Random.insideUnitSphere * 5f;
            randomDir += enemy.LastKnowPos;
            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDir, out hit, 5f, NavMesh.AllAreas))
            {
                enemy.Agent.SetDestination(hit.position);
            }

            moveTimer = 0;
        }

        if (searchTimer > 10f)
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }

    public override void Exit()
    {
        // Optionnel : reset des timers si besoin
    }
}