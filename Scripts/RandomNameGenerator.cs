using TMPro;
using UnityEngine;

public class RandomNameGenerator : MonoBehaviour
{
    public string[] prefixes = { "Аль", "Бе", "Кри", "Лю", "Ми", "Ни", "Ол", "Пи", "Ро", "Са", "Иль"};
    public string[] suffixes = { "дар", "вин", "рик", "лия", "тон", "лия", "мар", "нек", "сия", "ра" };
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private RectTransform _rectTransform;

    public string GenerateRandomName()
    {
        string prefix = prefixes[Random.Range(0, prefixes.Length)];
        string suffix = suffixes[Random.Range(0, suffixes.Length)];
        return prefix + suffix;
    }

    void Start()
    {
        string randomName = GenerateRandomName();
        _nameText.text = randomName;
    }

    private void LateUpdate()
    {
        if(Camera.main != null)
        _rectTransform.rotation = Camera.main.transform.rotation;
    }
}