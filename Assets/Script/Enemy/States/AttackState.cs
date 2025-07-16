using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
	private float shotTimer;

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
			shotTimer += Time.deltaTime;
			enemy.transform.LookAt(enemy.Player.transform);
			//if shot Timer > fireRate.
			if (shotTimer > enemy.fireRate)
			{	
				Shoot();
			}

            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
			enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 2)
            {
                //Change to the search state.
                stateMachine.ChangeState(new SearchState());
            }
        }
    }
	public void Shoot()
{
    	GameObject bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
    	if (bulletPrefab != null)
    	{
        	Transform gunBarrel = enemy.gunBarrel; // récupère la référence depuis enemy

        	GameObject bullet = GameObject.Instantiate(bulletPrefab, gunBarrel.position, enemy.transform.rotation);
        	Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.position).normalized;
        	bullet.GetComponent<Rigidbody>().linearVelocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 40;
        	Debug.Log("Shoot");
        	shotTimer = 0;
    	}
    	else
    	{
        	Debug.LogError("Bullet prefab not found! Check Resources/Prefabs/Bullet path.");
    	}
	}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
