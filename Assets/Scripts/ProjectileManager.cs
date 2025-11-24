using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public GameObject projectilePrefab;
    public GameObject flyPrefab;

    public GameObject[] projectiles;


    public void CreateFly(Vector2 location, Vector2 destination, float speed)
    {
        GameObject obj = Instantiate(flyPrefab, location, UnityEngine.Quaternion.identity);
        Fly f = obj.GetComponent<Fly>();
        f.angle = Mathf.Deg2Rad*Vector2.SignedAngle(Vector2.right, destination - location);
        f.location = location;
        f.power = speed;
        f.first = false;
    }

    public void CreateProjectile(Vector2 location, float angle, float power)
    {
        GameObject obj = Instantiate(projectilePrefab, location, UnityEngine.Quaternion.identity);
        Projectile p = obj.GetComponent<Projectile>();
        p.angle = Mathf.Deg2Rad*(angle);
        p.location = location;
        p.power = 1 + power;
        p.first = false;
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
