using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridSystem level;

    private GridSystem levelInstance;


    private void Start()
    {
        BeginGame();
    }

    public void BeginGame()
    {
        levelInstance = Instantiate(level);
    }

    public void DestroyLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}