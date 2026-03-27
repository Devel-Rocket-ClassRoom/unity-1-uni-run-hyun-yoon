using UnityEngine;

public class Platform : MonoBehaviour
{

    public GameObject[] objects;
    private float objRatio = 0.3f;
    private bool isSetpped;

    private GameManager manager;

    private void OnEnable()
    {
        foreach (var obj in objects)
        {
            obj.SetActive(Random.value < objRatio);
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
