using UnityEngine;

public class SpawnJalan : MonoBehaviour
{
    [SerializeField] GameObject tanah;
    Vector3 nextSpawnPoint;

    public void SpawnTile(bool spawnItems)
    {
        GameObject temp = Instantiate(tanah, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if (spawnItems)
        {
          temp.GetComponent<TriggerJalan>().SpawnObstacle();
            temp.GetComponent<TriggerJalan>().SpawnCoins();
        }
    }

    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            if (i < 3)
            {
                SpawnTile(false);
            }
            else
            {
                SpawnTile(true);
            }
        }
    }
}