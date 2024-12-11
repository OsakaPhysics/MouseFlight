using UnityEngine;

public class HevBulletScript : MonoBehaviour
{
    // Public variables
    public float ExplosionTime = 5f; // Time to wait before the explosion
    public GameObject EffectExplosion; // The explosion effect prefab

    private void Start()
    {
        // Call the Explode function after a delay of ExplosionTime
        Invoke("Explode", ExplosionTime);
    }

    // Function to play the explosion effect and destroy the object
    private void Explode()
    {
        // Instantiate the explosion effect at the current object's position
        if (EffectExplosion != null)
        {
            Instantiate(EffectExplosion, transform.position, transform.rotation);
        }

        // Destroy the current object after the explosion
        Destroy(gameObject);
    }
}


//HevBulletScript