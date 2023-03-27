using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SpotData
{
    public Vector3 pos;
    public bool occupied = false;
}
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject cubeObstacle;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject enemy;
    GameObject instBomb;
    private Dictionary<Vector3, SpotData> freeSpots = new Dictionary<Vector3, SpotData>();
    public Dictionary<Vector3, SpotData> GetEmptySpots() => freeSpots;

    public static Action OnSpotFreed;

    public static Spawner Instance;

    private int enemiesToSpawn = 5;

    public int EnemyDied 
    {
        get
        {
            return enemiesToSpawn;
        }
        set
        {
            enemiesToSpawn -= value;
            MenuManager.Instance?.UpdateGameEnemyLeftCount(enemiesToSpawn);
            if(enemiesToSpawn <= 0)
            {
                MenuManager.Instance?.EnableGameEndMenuMenu(GameEndType.WIN);
            }
        } }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }   
        SpawnLevel();
        SpawnEnemies();
        instBomb = Instantiate(bomb, Vector3.zero, Quaternion.identity);
        instBomb.SetActive(false);
        instBomb.GetComponent<Bomb>().SetUp(false);
    }
    void SpawnLevel()
    {
        for(int i = 0; i <=14; i++)
        {
            for(int j = 0; j<=10;j++)
            {
                freeSpots.Add(new Vector3(i, 0, j), new SpotData
                {
                    pos = new Vector3(i, 0, j),
                    occupied = false
                });
                if (i % 2 != 0 && j % 2 != 0)
                {
                    Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity);
                    freeSpots.Remove(new Vector3(i, 0, j));
                }
                else
                {
                    if(UnityEngine.Random.Range(0f,10f) < 3 && i != 0)
                    {
                        Instantiate(cubeObstacle, new Vector3(i, 0, j), Quaternion.identity);
                        freeSpots.Remove(new Vector3(i, 0, j));
                    }
                    else
                    {
                        
                    }
                }
            }
        }
        Transform playerTransform  = Instantiate(player, Vector3.zero, Quaternion.identity).transform;
        CameraController.OnPlayerSpawned(playerTransform);
    }

    public void SpawnBomb(Vector3 playerPos)
    {
        if (instBomb.activeInHierarchy) return;
        Vector3 newPos = new Vector3(Mathf.RoundToInt(playerPos.x), 0f, Mathf.RoundToInt(playerPos.z));
        instBomb.transform.SetLocalPositionAndRotation(newPos, Quaternion.identity);
        instBomb.SetActive(true);
        instBomb.GetComponent<Bomb>().SetUp(true);
        freeSpots.Remove(newPos);
    }
    public void SpawnEnemies()
    {
        List<KeyValuePair<Vector3, SpotData>> freeSpotsList = freeSpots.ToList();
        RandomizeList(freeSpotsList);
        int enemiesSpawned = 0;
        for(int i = 0; i < freeSpotsList.Count; i ++)
        {
            if(enemiesToSpawn > enemiesSpawned && freeSpotsList[i].Value.pos.x >= 1  && freeSpotsList[i].Value.pos.z >= 1)
            {
                Instantiate(enemy, freeSpotsList[i].Value.pos, Quaternion.identity);
                enemiesSpawned++;
            }
        }
    }
    private void RandomizeList(List<KeyValuePair<Vector3, SpotData>> freeSpots)
    {
        for(int i = 0; i < freeSpots.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, freeSpots.Count);
            KeyValuePair <Vector3, SpotData> temp = freeSpots[i];
            freeSpots[i] = freeSpots[randomIndex];
            freeSpots[randomIndex] = temp;
        }
    }
    public void FreeASpot(Vector3 pos)
    {
        Vector3 savePos = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
        freeSpots.Add(savePos, new SpotData
        {
            pos = savePos,
            occupied = false
        });
        OnSpotFreed();
    }
}
