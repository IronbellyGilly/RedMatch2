using System.Collections;
using TMPro;
using UnityEngine;

public class RaycastDamage : MonoBehaviour
{
    public float damageAmount = 10f; // количество урона
    public float range = 100f;       // длина луча
    public LayerMask enemyLayer;     // слой врагов
    public float _cooldown;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _sniperClip;
    [SerializeField] private AudioClip _reloadClip;
    [SerializeField] private int _bullet;
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _bulletText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _gilzaPrefab;
    [SerializeField] private Transform _gilzaSpawner;
    public float _timer;

    private void Start()
    {
        _timer = _cooldown;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && _bullet > 0 && _timer >= _cooldown) // например, при нажатии левой кнопки мыши
        {
            Shoot();
            _audioSource.PlayOneShot(_sniperClip);
            _bullet--;
            _bulletText.text = _bullet.ToString();
            _timer = 0;
            StartCoroutine(ReloadTimer());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _bullet = 9;
            _bulletText.text = _bullet.ToString();
            _audioSource.PlayOneShot(_reloadClip);
        }
    }

    void Shoot()
    {
        GameObject gilza = Instantiate(_gilzaPrefab, _gilzaSpawner.position, transform.rotation);
        Rigidbody rb = gilza.GetComponent<Rigidbody>();
        rb.AddForce(_gilzaSpawner.transform.up * 1.50f);
        rb.AddTorque(transform.forward * 100000);
        rb.AddTorque(transform.right * 100000);
        Destroy(gilza, 2);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, enemyLayer))
        {
            // Проверяем, есть ли у объекта метод для получения урона
            var enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
                Debug.Log("Враг получил урон");
                _score++;
                _scoreText.text = _score.ToString();
            }
        }
    }

    private IEnumerator ReloadTimer()
    {
        while (_timer < _cooldown)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
    }
}