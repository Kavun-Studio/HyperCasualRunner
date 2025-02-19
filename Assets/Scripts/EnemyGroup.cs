using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemyParent;

    [Header("Settings")]
    [SerializeField] private int amount;
    [SerializeField] private float radius;
    [SerializeField] private float angel;
    void Start()
    {
        EnemyGenerate();
    }

    private void EnemyGenerate()
    {
        for(int i = 0; i < amount; i++)
        {
            Vector3 enemyLocalPosition = PlayerRunnerLocalPositions(i);

            Vector3 enemyWorldPosition = enemyParent.TransformPoint(enemyLocalPosition);

            Instantiate(enemyPrefab, enemyWorldPosition, Quaternion.identity, enemyParent);
        }
    }

        private Vector3 PlayerRunnerLocalPositions(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angel);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angel);
        return new Vector3(x ,0 , z);
    }
}
