using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public PlayerController player;
    private int starCount;
   // private int enemyCount;
    public enum GameState {Lost, InProgress, Won };
    public GameState currentState;

    //A Class for loading between scenes and managing certain game aspects (Scene transitions and Game states)
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            starCount = GameObject.FindGameObjectsWithTag("Star").Length-1;
            //enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void StarLit()
    {
        starCount--;
        if (starCount < 0) 
        {
            Win();
        }
    }

    private void Win()
    {
       currentState = GameState.Won;
    }

    private void Lose()
    {
        currentState = GameState.Lost;
    }

}
