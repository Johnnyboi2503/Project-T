using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class zombie : MonoBehaviour, Interactable
{
    public NavMeshAgent navMeshAgent;
    public GameObject Player;
    public GameObject Zombie;
    public Vector3 SpawnPoint;
    public Timer_Script Timer;
    public float TimerMax;
    public void Interacted()
    {
        Destroy(this.gameObject);
    }
    public void Spawn()
    {
        Timer.Timer_Time = Random.Range(15f, 30f);
        Instantiate(Zombie, this.transform.position, new Quaternion(0,0,0,1));
    }
    public void Awake()
    {
        SpawnPoint = transform.position;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updateRotation = false;
    }
    public void Update()
    {
        navMeshAgent.SetDestination(new Vector3(Player.transform.position.x, Player.transform.position.y, SpawnPoint.z));
    }
}
