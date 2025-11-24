using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public GameObject projectilePrefab;

    public GameObject[] projectiles;


    public void CreateProjectile(Vector2 location, float angle, float power)
    {
        GameObject obj = Instantiate(projectilePrefab, location, UnityEngine.Quaternion.identity);
        Projectile p = obj.GetComponent<Projectile>();
        p.angle = Mathf.Deg2Rad*(angle);
        p.location = location;
        p.power = 1 + power;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
