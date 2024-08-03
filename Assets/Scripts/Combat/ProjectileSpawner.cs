using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    /*
    Designates a zone/area that the projectile will spawn from.
    
    The zone is usually a child Object rather than the Game Object itself.
    */
    public Transform firePoint;
    public GameObject projectilePrefab;

    /* 
    Used if the projectile has a set scale within the unity inspector
    Will affect project speed due to setting up projectile location relative to local scale values.
    */
    public float projectileSize = 0.25f;

    //Create a projectile at the Ranged Attack child object position
    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);

        //Change the direction of the projectile depending on the direction the player object is facing.
        Vector3 spawnDirection = projectile.transform.localScale;

        //Issue with spawning projectile transforming scale of object itself, reseting scale from object size to 1.
        projectile.transform.localScale = new Vector3(
            spawnDirection.x * transform.localScale.x > 0 ? projectileSize : -projectileSize,
            spawnDirection.y,
            spawnDirection.z
            );
    }
}
