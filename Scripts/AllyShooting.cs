using UnityEngine;

public class AllyShooting : MonoBehaviour
{
    [SerializeField] private bool _isEnemy;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask allyLayer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int damageAmount;

    private float nextFireTime = 0f;

    void Update()
    {
        TryShoot();
    }

    void TryShoot()
    {
        if (Time.time >= nextFireTime)
        {
            RaycastHit hit;

            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
            {
                if (!_isEnemy && hit.collider.gameObject.CompareTag("Enemy"))
                {
                    var enemy = hit.collider.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damageAmount);
                        _audioSource.Play();
                    }
                }
                if (_isEnemy && hit.collider.gameObject.CompareTag("Ally"))
                {
                    var enemy = hit.collider.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damageAmount);
                        _audioSource.Play();
                    }
                }
                if (_isEnemy && hit.collider.gameObject.CompareTag("Player"))
                {
                    var enemy = hit.collider.GetComponent<Health>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damageAmount);
                        _audioSource.Play();
                    }
                }
            }
            nextFireTime = Time.time + fireRate;
        }
    }
}