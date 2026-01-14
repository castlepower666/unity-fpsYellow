using UnityEngine;

public class PlayerHealthContoller : MonoBehaviour
{
    public static PlayerHealthContoller Instance;
    public float maxHealth = 100f;
    private float currentHealth;

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player has died.");
        }
    }
}
