using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleTank : MonoBehaviour
{
    [SerializeField] private LayerMask _targetsColliders;
    [SerializeField] private Cursor _cursor;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _lineRendererPoints;


    private ICannon _cannon;
    private IMovable _movable;
    private Vector3 _axises;
    private Camera _camera;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _movable = GetComponent<IMovable>();
        _cannon = GetComponentInChildren<ICannon>();
        if (_lineRenderer != null) _lineRenderer.positionCount = _lineRendererPoints;
        _camera = Camera.main;
    }




    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        _axises = Vector3.zero;
        if (_cannon != null && _camera != null)
        {
            RaycastHit hit;
            Vector2 screenPos = Input.mousePosition;
            bool canReach = false;
            var ray = _camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            if (Physics.Raycast(ray, out hit, 1000, _targetsColliders))
            {
                _axises = _cannon.Rotate(hit.point);
                canReach = _cannon.CanReach();
            }

            if (_cursor != null)
            {
                _cursor.SetPosition(screenPos);
                if (canReach) _cursor.SetActiveColor();
                else _cursor.SetInactiveColor();
            }
            if (_lineRenderer != null)
            {
                var points = _cannon.GetPath(_lineRendererPoints);
                for (int i = 0; i < points.Count; i++)
                {
                    _lineRenderer.SetPosition(i, points[i]);
                }


            }


            if (Input.GetMouseButton(1))
            {
                if (_cannon.CanFire())
                {
                    _cannon.Fire();
                }
            }
        }
        if (_movable != null)
        {
            if (Input.GetKey(KeyCode.W))
                _axises.z = 1;
            if (Input.GetKey(KeyCode.S))
                _axises.z = -1;
            if (Input.GetKey(KeyCode.D))
                _axises.y = 1;
            if (Input.GetKey(KeyCode.A))
                _axises.y = -1;
            _movable.Move(_axises);
        }

    }
}
