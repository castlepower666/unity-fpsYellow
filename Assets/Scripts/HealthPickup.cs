using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthContoller.Instance.Heal(healAmount);
            Destroy(gameObject);
        }
    }

}
