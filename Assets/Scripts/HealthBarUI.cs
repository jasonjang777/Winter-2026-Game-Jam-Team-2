using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class HealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, Width, Height;
    [SerializeField] private RectTransform healthBar;

    public void setMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
        healthBar.gameObject.GetComponent<Image>().color = Color.yellow;
    }
    
    public void setHealth(float health)
    {
        if (health <= Health - 5)
        {
            StartCoroutine(flashRed());
        }
        Health = health;
        float newWidth = (Health / MaxHealth) * Width;
        // Debug.Log(newWidth);
        healthBar.sizeDelta = new Vector2(newWidth, Height);
    }

    IEnumerator flashRed() 
    {
        // Color originalColor = healthBar.gameObject.GetComponent<Image>().color;
        healthBar.gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.25f);
        healthBar.gameObject.GetComponent<Image>().color = Color.yellow;
        yield break;
    }
}
