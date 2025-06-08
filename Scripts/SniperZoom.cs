using UnityEngine;

public class SniperZoom : MonoBehaviour
{
    [SerializeField] private Zoom zoom;
    [SerializeField] private GameObject _sniperZoom;
    [SerializeField] private GameObject _target;

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            zoom.currentZoom = 1;
            _sniperZoom.SetActive(true);
            _target.SetActive(false);
        }
        else
        {
            zoom.currentZoom = 0;
            _sniperZoom.SetActive(false);
            _target.SetActive(true);
        }
    }
}
