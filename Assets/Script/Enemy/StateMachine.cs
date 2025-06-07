using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Initialise()
    {
        ChangeState(new PatrolState());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activeState != null
        if (activeState != null)
        {
            //run cleanup on activeState.
            activeState.Exit();
        }
        //change to a new state.
        activeState = newState;
        
        //fail-safe null check to make sur new state wasn't null
        if (activeState != null)
        {
            //Setup new state.
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
    
}
