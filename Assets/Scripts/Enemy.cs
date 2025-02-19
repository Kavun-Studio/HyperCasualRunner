using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State {Idle, Running}

    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;

    private State state;
    private Transform targetRunner;

    void Start()
    {
        
    }

    void Update()
    {
        MannageState();
    }

    private void MannageState()
    {
        switch (state)
        {
            case State.Idle:
                SearchForTarget();
                break;
            
            case State.Running:
                RunnerTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if(detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if(runner.IsTarget())
                    continue;
                
                runner.SetTarget();
                targetRunner = runner.transform;
                
                StartRunningToward();
            }
        }
    }

    private void StartRunningToward()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunnerTowardsTarget()
    {
        if(targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

        if(Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
}
