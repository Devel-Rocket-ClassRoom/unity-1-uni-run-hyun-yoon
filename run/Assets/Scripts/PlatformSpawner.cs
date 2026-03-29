using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject bottomPrefab;
    public GameObject topPrefab;
    public int count = 3;

    public Vector2 spawnTimeRange = new Vector2(0.85f, 1.35f); //x:min y:max
    public float timeSpawn;

    private float xPos = 20f;

    private GameObject[] bottoms;
    private GameObject[] tops;
    private int bottomCurrIndex = 0;
    private int topCurrIndex = 0;

    private float bottomLastSpawnTime;
    private float topLastSpawnTime;

    private GameManager gameManager;

    void Awake()
    {
        bottoms = new GameObject[count];
        tops = new GameObject[count];

        for (int i = 0; i < bottoms.Length; i++)
        {
            bottoms[i] = Instantiate(bottomPrefab);
            bottoms[i].SetActive(false);
        }

        for (int i = 0; i < tops.Length; i++)
        {
            tops[i] = Instantiate(topPrefab);
            tops[i].SetActive(false);
        }
    }

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController")?.GetComponent<GameManager>();

        bottomLastSpawnTime = 0f;
        topLastSpawnTime = 0f;
        timeSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }

        if (Time.time > topLastSpawnTime + spawnTimeRange.x)
        {
            topLastSpawnTime = Time.time;

            Vector2 pos;
            pos.x = xPos;
            pos.y = 2.5f;

            tops[topCurrIndex].transform.position = pos;

            tops[topCurrIndex].SetActive(false);
            tops[topCurrIndex].SetActive(true);

            topCurrIndex = (int)Mathf.Repeat(topCurrIndex + 1, tops.Length);
        }

        if (Time.time > bottomLastSpawnTime + timeSpawn)
        {
            bottomLastSpawnTime = Time.time;
            timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

            Vector2 pos;
            pos.x = xPos;
            pos.y = -3.0f;

            bottoms[bottomCurrIndex].transform.position = pos;

            bottoms[bottomCurrIndex].SetActive(false);
            bottoms[bottomCurrIndex].SetActive(true);

            bottomCurrIndex = (int)Mathf.Repeat(bottomCurrIndex + 1, bottoms.Length);
        }
    }
}
