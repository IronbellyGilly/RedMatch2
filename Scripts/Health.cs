using System.Collections;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Camera _dieCamera;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _bloodPrefab;
    [SerializeField] private GameObject _damageUI;

    public void TakeDamage(int amount)
    {
        _health -= amount;
        _healthText.text = _health.ToString();
        StartCoroutine(DamageUI());
        if( _health <= 0 )
        {
            Destroy(Instantiate(_bloodPrefab, transform.position, Quaternion.identity), 2);
            StartCoroutine(DieCamera());
        }
    }

    private IEnumerator DieCamera()
    {
        _dieCamera.enabled = true;
        _mainCamera.enabled = false;
        yield return new WaitForSeconds(2);
        _dieCamera.enabled = false;
        _mainCamera.enabled = true;
        transform.position = _spawnPoint.position;
        _health = 100;
        _healthText.text = _health.ToString();
    }

    private IEnumerator DamageUI()
    {
        _damageUI.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _damageUI.SetActive(false);
    }
}
