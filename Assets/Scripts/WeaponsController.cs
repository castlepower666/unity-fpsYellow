using System;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{

    public float range;
    public Transform cam;
    public LayerMask validLayers;
    public GameObject impactEffect;
    public GameObject damageEffect;
    public GameObject muzzleFlare;
    public float flareDisplayTime;
    private float flareCounter;
    public bool canAutoShoot;
    public float timeBewteenShots;
    private float shotCounter;
    public int currentAmmos;
    public int clipSize;
    public int remainingAmmos;
    private UIController UICon;
    public int pickUpAmount;
    public float damageAmount;
    public Weapon[] weapons;
    private int currentWeaponIndex = 0;
    private int priorWeaponIndex;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UICon = FindFirstObjectByType<UIController>();
        SetWeapon(currentWeaponIndex);
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(cam.position, cam.position + cam.forward * range, Color.green);

        if (flareCounter > 0)
        {
            flareCounter -= Time.deltaTime;
            if (flareCounter <= 0)
            {
                muzzleFlare.SetActive(false);
            }
        }
    }

    public void Shoot()
    {
        if (currentAmmos > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, range, validLayers))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Instantiate(damageEffect, hit.point, Quaternion.identity);
                    hit.transform.GetComponent<EnemyController>().TakeDamage(damageAmount);
                }
                else
                {
                    Instantiate(impactEffect, hit.point, Quaternion.identity);
                }
            }

            muzzleFlare.SetActive(true);
            flareCounter = flareDisplayTime;

            shotCounter = timeBewteenShots;

            currentAmmos--;

            UICon.UpdateAmmosText(currentAmmos, remainingAmmos);
        }
    }

    public void ShootHeld()
    {
        if (canAutoShoot)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Shoot();
            }
        }
    }

    public void Reload()
    {
        remainingAmmos += currentAmmos;

        if (remainingAmmos >= clipSize)
        {
            currentAmmos = clipSize;
            remainingAmmos -= clipSize;
        }
        else
        {
            currentAmmos = remainingAmmos;
            remainingAmmos = 0;
        }
        UICon.UpdateAmmosText(currentAmmos, remainingAmmos);
    }

    public void GetAmmo()
    {
        remainingAmmos += pickUpAmount;
        UICon.UpdateAmmosText(currentAmmos, remainingAmmos);
    }


    public void SetWeapon(int weaponIndex)
    {
        if (priorWeaponIndex != currentWeaponIndex)
        {
            weapons[priorWeaponIndex].remainingAmmos = remainingAmmos;
            weapons[priorWeaponIndex].currentAmmos = currentAmmos;
            weapons[priorWeaponIndex].muzzleFlare.SetActive(false);
        }


        range = weapons[weaponIndex].range;
        muzzleFlare = weapons[weaponIndex].muzzleFlare;
        flareDisplayTime = weapons[weaponIndex].flareDisplayTime;
        canAutoShoot = weapons[weaponIndex].canAutoShoot;
        timeBewteenShots = weapons[weaponIndex].timeBewteenShots;
        currentAmmos = weapons[weaponIndex].currentAmmos;
        clipSize = weapons[weaponIndex].clipSize;
        remainingAmmos = weapons[weaponIndex].remainingAmmos;
        pickUpAmount = weapons[weaponIndex].pickUpAmount;
        damageAmount = weapons[weaponIndex].damageAmount;

        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        weapons[weaponIndex].gameObject.SetActive(true);

        UICon.UpdateAmmosText(currentAmmos, remainingAmmos);
    }

    public void NextWeapon()
    {
        priorWeaponIndex = currentWeaponIndex;
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        SetWeapon(currentWeaponIndex);
    }

    public void PreviousWeapon()
    {
        priorWeaponIndex = currentWeaponIndex;
        currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
        SetWeapon(currentWeaponIndex);

    }


}
