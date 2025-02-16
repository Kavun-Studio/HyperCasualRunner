using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elemets")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;
    

    [Header("Settings")]
    [SerializeField] private float radius;
    [SerializeField] private float angel;

    void Update()
    {
        PlaceRunner();
    }

    private void PlaceRunner()
    {
        for(int i = 0; i < runnersParent.childCount; i ++)
        {
                Vector3 childLocalPosition = PlayerRunnerLocalPosition(i);
                runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 PlayerRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angel);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angel);
        return new Vector3(x ,0 , z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;

            case BonusType.Product:
                int runnerToAdd = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunners(runnerToAdd);
                break;
            
            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;
            
            case BonusType.Division:
                int runnerToRemove = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunners(runnerToRemove);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
            Instantiate(runnerPrefab, runnersParent);
    }

    private void RemoveRunners(int amount)
    {
        if(amount > runnersParent.childCount)
            amount = runnersParent.childCount;
        int runnersAmount = runnersParent.childCount;

        for(int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
