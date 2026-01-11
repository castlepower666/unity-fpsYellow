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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UICon = FindFirstObjectByType<UIController>();
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

}
