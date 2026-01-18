using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Experimental.Video;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEngine.SceneManagement.Scene nextScene;
    public string thisSceneName;
    [HideInInspector] public PlayerController player;
    public int starCount;
    public int enemyCount;
    public enum GameState {Lost, InProgress, Won };
    public GameState currentState;
    
    //A Class for loading between scenes and managing certain game aspects (Scene transitions and Game states)
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            
            //enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;
        }
        else
        {

            GameManager.Instance.starCount = GameObject.FindGameObjectsWithTag("Star").Length - 1;
            GameManager.Instance.enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;
            GameManager.Instance.player = FindAnyObjectByType<PlayerController>();
            GameManager.Instance.currentState = GameState.InProgress;
            GameManager.Instance.thisSceneName = SceneManager.GetActiveScene().name;
            Destroy(gameObject);
        }
        starCount = GameObject.FindGameObjectsWithTag("Star").Length - 1;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;
        player = FindAnyObjectByType<PlayerController>();
        currentState = GameState.InProgress;
        thisSceneName = SceneManager.GetActiveScene().name;
    }

    public void StarLit()
    {
        starCount--;
        if (starCount < 0 & enemyCount < 0) 
        {
            Win();
        }
    }
    public void EnemyKilled()
    {
        enemyCount--;
        if (starCount < 0 & enemyCount < 0)
        {
            Win();
        }
    }

    private void Win()
    {
       currentState = GameState.Won;
        //SceneManager.LoadScene("Enemies");

    }

    public void Lose()
    {
        currentState = GameState.Lost;
        SceneManager.LoadScene(thisSceneName);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
    }
}
