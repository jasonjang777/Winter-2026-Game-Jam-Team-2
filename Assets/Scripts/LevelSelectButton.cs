using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string sceneName;
    [SerializeField] string levelName;
    [SerializeField] private float roatationSpeed;
    [SerializeField] private float speedUp;
    [SerializeField] private TextMeshProUGUI titlePanel;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (titlePanel != null)
        {
            titlePanel.text = levelName; 
        }
        roatationSpeed+= speedUp;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (titlePanel != null)
        {
            titlePanel.text = "";
        }
        roatationSpeed -= speedUp;
    }
}
