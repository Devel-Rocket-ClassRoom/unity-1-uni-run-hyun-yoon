using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;

    public float energy = 100f;
    private int coin = 0;

    public bool IsGameOver { get; private set; }
    private void Awake()
    {
        gameOverUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsGameOver && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
    }

    public void AddCoinWithHealth(int add)
    {
        coin += 1;
        energy += add;
        scoreText.text = $"Coin: {coin}";
    }

    public void Hit(int add)
    {

        energy -= add;

        if(energy <= 0) 
        {
            energy = 0;
        }
    }

    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOverUI.SetActive(true);
    }
}
