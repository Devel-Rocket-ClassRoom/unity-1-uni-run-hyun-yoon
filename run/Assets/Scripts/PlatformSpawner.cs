using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int count = 3;

    public Vector2 spawnTimeRange = new Vector2(0.85f, 1.35f); //x:min y:max
    public float timeSpawn;

    private float xPos = 20f;

    private GameObject[] platforms;
    private int currentIndex = 0;

    private float lastSpawnTime;

    private GameManager gameManager;

    void Awake()
    {
        platforms = new GameObject[count];

        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = Instantiate(platformPrefab);
            platforms[i].SetActive(false);
        }
    }

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController")?.GetComponent<GameManager>();

        lastSpawnTime = 0f;
        timeSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }

        if (Time.time > lastSpawnTime + timeSpawn)
        {
            lastSpawnTime = Time.time;
            timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

            Vector2 pos;
            pos.x = xPos;
            pos.y = -3.0f;

            platforms[currentIndex].transform.position = pos;

            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            currentIndex = (int)Mathf.Repeat(currentIndex + 1, platforms.Length);
        }
    }
}
