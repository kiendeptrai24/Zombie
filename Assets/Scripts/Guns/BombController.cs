using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float explosionRadius = 4f;
    [SerializeField] private float explosionForce = 100f;
    [SerializeField] private float upwardForce = 2f;
    [SerializeField] private LayerMask zombieLayer;
    [SerializeField] private GameObject explosionEffect;
    private bool hasLanded;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasLanded)
            return;

        if (!collision.gameObject.CompareTag("Ground"))
            return;

        hasLanded = true;

        // Dừng bom
        rb.isKinematic = true;
        rb.useGravity = false;
        Invoke(nameof(Explode), .7f);
    }
    public void ResetBomb()
    {
        hasLanded = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
    private void Explode()
    {
        var exEffect = ObjectPool.Instance.GetObject(explosionEffect, transform.position, Quaternion.identity);
        Collider[] zombies = Physics.OverlapSphere(
            transform.position,
            explosionRadius,
            zombieLayer
        );

        foreach (Collider zombie in zombies)
        {
            Vector3 direction =
                zombie.transform.position - transform.position;
            zombie.GetComponent<ZombieController>().Knockback(direction.normalized * explosionForce, upwardForce);

        }
        SFXManager.Instance.PlayOneShot("bomb");
        ObjectPool.Instance.ReturnObject(exEffect, 1);
        ObjectPool.Instance.ReturnObject(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}