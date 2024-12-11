using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastgun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float fireRate = 0.1f; // Time between shots

    private bool isFiring = false;

    private void Update()
    {
        // Start firing when spacebar is held down
        if (Input.GetKey(KeyCode.Space) && !isFiring)
        {
            StartCoroutine(FireGun());
        }
    }

    private IEnumerator FireGun()
    {
        isFiring = true;

        // Create and fire a bullet
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

        // Wait for the next shot based on fireRate
        yield return new WaitForSeconds(fireRate);

        isFiring = false;
    }
}
