using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        ObjectPool.Instance.ReturnObject(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageTarget = other.GetComponent<IDamageable>();

        if (damageTarget != null)
        {
            damageTarget.TakeDamage(damage);
        }
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}