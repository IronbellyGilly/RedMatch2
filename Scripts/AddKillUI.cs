using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddKillUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _textPrefab;
    [SerializeField] private Transform _scrollContent; // содержимое ScrollRect
    [SerializeField] private ScrollRect _scrollRect; // ссылка на ScrollRect

    // Метод для добавления элемента и прокрутки
    public void AddNewElement()
    {
        StartCoroutine(AddAndScroll());
    }

    private IEnumerator AddAndScroll()
    {
        // Создаем объект внутри контента
        GameObject newText = Instantiate(_textPrefab[Random.Range(0, _textPrefab.Length)], _scrollContent);
        // Не задаем localPosition вручную, пусть Layout Group управляет расположением

        // Ждем короткое время для обновления UI (если нужно)
        yield return new WaitForEndOfFrame();

        // Прокрутка вниз
        yield return StartCoroutine(ScrollToBottom());
    }

    private IEnumerator ScrollToBottom()
    {
        float duration = 0.5f; // Время анимации прокрутки
        float elapsed = 0f;
        float startPos = _scrollRect.verticalNormalizedPosition;
        float endPos = 0f; // Вниз — позиция 0

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _scrollRect.verticalNormalizedPosition = Mathf.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }
        _scrollRect.verticalNormalizedPosition = endPos; // Гарантированно внизу
    }
}