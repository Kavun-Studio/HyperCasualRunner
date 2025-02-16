using UnityEngine;

public class PlayerDection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    void Start()
    {
        
    }

    void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if(detectedColliders[i].TryGetComponent(out Doors doors))
            {
                Debug.Log("Hit the Doors");

                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.DisableDoorCollider();
                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }
        
            else if(detectedColliders[i].tag == "Finish")
            {
                Debug.Log("Hit Funish Line"); 
            }
        }
    }
}
