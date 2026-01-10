using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public PlayerController player;
    private int starCount;

    //A Class for loading between scenes and managing certain game aspects (Scene transitions and Game states)
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void starLit()
    {
        starCount--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
