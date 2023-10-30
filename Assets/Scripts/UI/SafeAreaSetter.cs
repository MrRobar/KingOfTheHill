using UnityEngine;

public class SafeAreaSetter : MonoBehaviour
{
    [SerializeField] private RectTransform _zone;

    private void Awake()
    {
        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        _zone.anchorMin = anchorMin;
        _zone.anchorMax = anchorMax;
    }
}