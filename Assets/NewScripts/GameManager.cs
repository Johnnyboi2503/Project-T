using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] EnemySpawn;
    public GameObject enemy;
    public GameObject boss;

    public float spawnTimer;
    public float bossTimer;

    public float safeTimer;
    public float dangerTimer;
    public bool onDanager = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onDanager == false) // enemys will not spawn until the safe timer is up
        {
            safeTimer += Time.deltaTime;
            bossTimer += Time.deltaTime;
            if (safeTimer > 20)
            {
                onDanager = true;
                safeTimer = 0;
            }
        }

        if (onDanager == true) // enemys will start to spawn and the dander timer starts
        {
            dangerTimer += Time.deltaTime;

            spawnTimer += Time.deltaTime;
            if (spawnTimer > 2)
            {
                int random = Random.Range(0, EnemySpawn.Length); // random enemyspawns

                SpawnEnemy(random); // will spawn enemy in random enemyspawners

                spawnTimer = 0;
            }
        }

        if (dangerTimer > 20) // will stop the enemys from spwaning 
        {
            onDanager = false;
            dangerTimer = 0;
            spawnTimer = 0;
        }

        if(bossTimer > 40)
        {
            int random = Random.Range(0, EnemySpawn.Length); // random enemyspawns
            SpawnBoss(random); //will spawn boss in random enemyspawner
            bossTimer = 0;
        }

    }

    void SpawnEnemy(int index)
    {
        Transform spawnPoint = EnemySpawn[index].transform; // get the position and rotation of random enemy spawn

        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); // spawn enemy 
    }

    void SpawnBoss(int index)
    {
        Transform spawnPoint = EnemySpawn[index].transform; // get the position and rotation of random enemy spawn

        Instantiate(boss, spawnPoint.position, spawnPoint.rotation); // spawn boss
    }
}
