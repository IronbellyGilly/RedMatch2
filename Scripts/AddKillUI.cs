using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddKillUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _textPrefab;
    [SerializeField] private Transform _scrollContent; // ���������� ScrollRect
    [SerializeField] private ScrollRect _scrollRect; // ������ �� ScrollRect

    // ����� ��� ���������� �������� � ���������
    public void AddNewElement()
    {
        StartCoroutine(AddAndScroll());
    }

    private IEnumerator AddAndScroll()
    {
        // ������� ������ ������ ��������
        GameObject newText = Instantiate(_textPrefab[Random.Range(0, _textPrefab.Length)], _scrollContent);
        // �� ������ localPosition �������, ����� Layout Group ��������� �������������

        // ���� �������� ����� ��� ���������� UI (���� �����)
        yield return new WaitForEndOfFrame();

        // ��������� ����
        yield return StartCoroutine(ScrollToBottom());
    }

    private IEnumerator ScrollToBottom()
    {
        float duration = 0.5f; // ����� �������� ���������
        float elapsed = 0f;
        float startPos = _scrollRect.verticalNormalizedPosition;
        float endPos = 0f; // ���� � ������� 0

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _scrollRect.verticalNormalizedPosition = Mathf.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }
        _scrollRect.verticalNormalizedPosition = endPos; // �������������� �����
    }
}