using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TMP_Text ammosText;
    public TMP_Text remainingAmmosText;
    public TMP_Text healthText;
    public static UIController Instance;

    public GameObject deadScreen;

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAmmosText(int currentAmmos, int remainingAmmos)
    {
        ammosText.text = currentAmmos.ToString();
        remainingAmmosText.text = "/" + remainingAmmos.ToString();
    }

    public void UpdateHealthText(float currentHealth)
    {
        healthText.text = "Health: " + Mathf.RoundToInt(currentHealth).ToString();
    }

    public void ShowDeadScreen()
    {
        deadScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
