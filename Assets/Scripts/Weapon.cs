using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range;
    public GameObject muzzleFlare;

    public float flareDisplayTime;

    public bool canAutoShoot;
    public float timeBewteenShots;

    public int currentAmmos;
    public int clipSize;
    public int remainingAmmos;

    public int pickUpAmount;

    public float damageAmount = 15f;
}
