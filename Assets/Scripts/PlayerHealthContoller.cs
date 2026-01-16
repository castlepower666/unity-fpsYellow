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
        UIController.Instance.UpdateHealthText(currentHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerController.Instance.isDead = true;

            UIController.Instance.ShowDeadScreen();
        }

        UIController.Instance.UpdateHealthText(currentHealth);
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.Instance.UpdateHealthText(currentHealth);
    }
}
