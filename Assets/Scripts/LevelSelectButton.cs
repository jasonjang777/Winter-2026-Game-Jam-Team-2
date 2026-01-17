using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] private float roatationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        this.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, roatationSpeed * Time.deltaTime);
    }
    public void OpenScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
