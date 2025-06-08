using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] objects; // ������ ��������
    private int currentIndex = 0; // ������ �������� ��������� �������
    [SerializeField] private RaycastDamage raycastDamage;

    void Start()
    {
        // ���������� ������� ������ ������ ������
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            // ��������� ����� - ��������� ������
            ShowNextObject();
        }
        else if (scroll < 0f)
        {
            // ��������� ���� - ���������� ������
            ShowPreviousObject();
        }
        
    }

    void ShowNextObject()
    {
        int previousIndex = currentIndex;
        currentIndex++;
        if (currentIndex >= objects.Length)
            currentIndex = 0; // �������������

        UpdateObjects(previousIndex, currentIndex);
    }

    void ShowPreviousObject()
    {
        int previousIndex = currentIndex;
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = objects.Length - 1; // �������������

        UpdateObjects(previousIndex, currentIndex);
    }

    void UpdateObjects(int previous, int next)
    {
        objects[previous].SetActive(false);
        objects[next].SetActive(true);
        if (objects[0].activeSelf)
        {
            raycastDamage._cooldown = 2f;
            raycastDamage._timer = 3;
        }
        else
        {
            raycastDamage._cooldown = 0.3f;
            raycastDamage._timer = 3;
        }
    }
}