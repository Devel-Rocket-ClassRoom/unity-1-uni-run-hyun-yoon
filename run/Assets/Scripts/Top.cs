using UnityEngine;

public class Top : MonoBehaviour
{

    public GameObject[] hits;
    public GameObject[] coins;
    private float objRatio = 0.3f;
    private bool isSetpped;

    private GameManager manager;

    private void OnEnable()
    {
        int randNum = Random.Range(0, hits.Length);

        for (int i = 0; i < hits.Length; i++)
        {
            bool hitsActive = (randNum == i);

            hits[i].SetActive(hitsActive);
            coins[i].SetActive(!hitsActive);
        }

        isSetpped = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isSetpped)
        {
            isSetpped = true;
        }
    }
}
