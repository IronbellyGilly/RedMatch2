using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int FramePerSecond = 60;

    private void Start()
    {
        Application.targetFrameRate = FramePerSecond;
    }
}
