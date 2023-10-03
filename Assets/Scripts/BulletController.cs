using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public GameObject explosion;
    private void Start()
    {
        Invoke("DeactivateBullet", 2f);
    }

    public void DeactivateBullet()
    {
        Instantiate(explosion,transform.position,transform.rotation);
        ObjectPoolManager.Instance.ReturnBulletToPool(gameObject);
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(Vector2.right * -speed * Time.deltaTime);
    }
    
    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.ReturnBulletToPool(gameObject);
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(1);
        }
        ObjectPoolManager.Instance.ReturnBulletToPool(gameObject);
    }
}
