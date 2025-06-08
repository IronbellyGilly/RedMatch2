using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] objects; // Массив объектов
    private int currentIndex = 0; // Индекс текущего активного объекта
    [SerializeField] private RaycastDamage raycastDamage;

    void Start()
    {
        // Изначально активен только первый объект
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
            // Прокрутка вверх - следующий объект
            ShowNextObject();
        }
        else if (scroll < 0f)
        {
            // Прокрутка вниз - предыдущий объект
            ShowPreviousObject();
        }
        
    }

    void ShowNextObject()
    {
        int previousIndex = currentIndex;
        currentIndex++;
        if (currentIndex >= objects.Length)
            currentIndex = 0; // Зацикливаемся

        UpdateObjects(previousIndex, currentIndex);
    }

    void ShowPreviousObject()
    {
        int previousIndex = currentIndex;
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = objects.Length - 1; // Зацикливаемся

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