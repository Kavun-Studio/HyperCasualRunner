using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Chunk[] chunkPrefab;
    [SerializeField] private Chunk[] chunkLevels;
    void Start()
    {
        CreateOrderedLevels();
    }

    private void CreateOrderedLevels()
    {
        Vector3 chunkPosition = Vector3.zero;
            for (int i = 0; i < chunkLevels.Length; i++)
            {
                Chunk chunkToCreate = chunkLevels[i];

                if(i > 0)
                    chunkPosition.z += chunkToCreate.GetLenght() / 2;

                Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
                chunkPosition.z += chunkInstance.GetLenght() / 2;
            }
    }

    private void CreateRandomLevels()
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < 5; i++)
        {
            Chunk chunkToCreate = chunkPrefab[Random.Range(0, chunkPrefab.Length)];

            if(i > 0)
                chunkPosition.z += chunkToCreate.GetLenght() / 2;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLenght() / 2;
        }
    }
}
