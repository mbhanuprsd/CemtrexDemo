using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int gunDamage = 100;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private LineRenderer laserLine;
    private float nextFire;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

        fpsCam = Camera.main;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            ShootBullet(true);
        }
        else if (Input.GetMouseButtonDown(1) && Time.time > nextFire)
        {
            ShootBullet(false);
        }
    }

    private void ShootBullet(bool blueBullet)
    {
        nextFire = Time.time + fireRate;

        StartCoroutine(ShotEffect());

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);
        laserLine.material.color = blueBullet ? Color.blue : Color.red;

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            laserLine.SetPosition(1, hit.point);
            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
            if (health != null && ((health.gameObject.tag == "BlueEnemy" && blueBullet)
                                   || (health.gameObject.tag == "RedEnemy" && !blueBullet)))
            {
                health.TakeDamage(gunDamage);
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }
}
