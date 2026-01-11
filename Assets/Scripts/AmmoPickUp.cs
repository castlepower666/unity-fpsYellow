using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<WeaponsController>()?.GetAmmo();

            Destroy(gameObject);
        }
    }
}
