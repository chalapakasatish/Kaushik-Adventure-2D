using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    public GameObject bulletPrefab;
    public int initialPoolSize = 10;

    public Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count == 0)
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            return newBullet;
        }

        GameObject bullet = bulletPool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.GetComponent<BulletController>().ReturnToPool();
        bulletPool.Enqueue(bullet);
    }
}
