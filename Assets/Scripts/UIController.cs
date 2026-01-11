using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Text ammosText;
    public TMP_Text remainingAmmosText;



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

}
