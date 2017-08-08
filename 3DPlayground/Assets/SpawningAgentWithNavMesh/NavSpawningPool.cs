using UnityEngine;

public class NavSpawningPool : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Goal;

    private void Start()
    {
        this.Invoke("SpawnEnemy", 2);
    }

    private void SpawnEnemy()
    {
        var enemy = (GameObject)Instantiate(this.Enemy, this.transform.position, Quaternion.identity);
        enemy.GetComponent<WalkObjectToDestination>().Goal = this.Goal.transform;
        this.Invoke("SpawnEnemy", Random.Range(2, 5));
    }
}
