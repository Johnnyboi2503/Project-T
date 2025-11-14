using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] EnemySpawn;
    public GameObject enemy;

    public float spawnTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > 2)
        {
            int random = Random.Range(0, EnemySpawn.Length);

            SpawnEnemy(random);

            spawnTimer = 0;
        }
    }

    void SpawnEnemy(int index)
    {// get the position and rotation of random enemy spawn
        Transform spawnPoint = EnemySpawn[index].transform; 

        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
