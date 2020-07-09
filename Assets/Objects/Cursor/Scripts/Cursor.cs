using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Cursor : MonoBehaviour
{

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private List<Image> _images;
    private RectTransform _rectTransform;

    public void SetPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = position;
    }

    public void SetActiveColor()
    {
        foreach (var i in _images)
        {
            i.color = _activeColor;
        }
    }

    public void SetInactiveColor()
    {
        foreach (var i in _images)
        {
            i.color = _inactiveColor;
        }
    }


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _images = new List<Image>(GetComponentsInChildren<Image>());
        _rectTransform = transform as RectTransform;
    }
}
