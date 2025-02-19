using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Elements")]
    [SerializeField] private LevelSO[] levels;

    private GameObject finishLine;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        GenerateLevels();

        finishLine = GameObject.FindGameObjectWithTag("Finish");
    }

    private void GenerateLevels()
    {
        int currentLevel = GetLevels();

        currentLevel = currentLevel % levels.Length;
        LevelSO level = levels[currentLevel];

        CreateLevels(level.chunks);
    }

    private void CreateLevels(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;
            for (int i = 0; i < levelChunks.Length; i++)
            {
                Chunk chunkToCreate = levelChunks[i];

                if(i > 0)
                    chunkPosition.z += chunkToCreate.GetLenght() / 2;

                Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
                chunkPosition.z += chunkInstance.GetLenght() / 2;
            }
    }

/*  private void CreateRandomLevels()
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
    }*/

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevels()
    {
        return PlayerPrefs.GetInt("Level", 0);
    }
}
