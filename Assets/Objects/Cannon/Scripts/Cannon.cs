using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, ICannon
{

    public CannonSettings settings;
    public Transform firePoint;
    private Rigidbody _rigidbody;
    private IMovable _movable;
    private float _targetXRotation;
    private float _targetYRotation;

    private Quaternion _defaultRotation;
    private bool canFire = false;

    public bool CanFire(Vector3 destination)
    {

        return canFire;
    }
    public void Fire(Vector3 destination)
    {
        IShell shell;
        Debug.Log("Cannon fire");
        var shellObject = Instantiate(settings.shellPrefab, firePoint.position, firePoint.rotation);
        shell = shellObject.GetComponent<IShell>();
        if (shell != null)
        {
            shell.Fire(settings.shellStartSpeed);
        }
        else
        {
            DestroyImmediate(shellObject);
        }
    }
    public Vector3 Rotate(Vector3 destination)
    {
        Vector3 result = Vector3.zero;

        var direction = destination - firePoint.position;
        var planeForward = Vector3.ProjectOnPlane(direction, Vector3.up);
        float sin2a = 0;
        float arcsin2a = 0;

        sin2a = planeForward.magnitude * Physics.gravity.magnitude / settings.shellStartSpeed / settings.shellStartSpeed;
        arcsin2a = Mathf.Rad2Deg * Mathf.Asin(sin2a);
        if (sin2a >= 0 && sin2a <= 1)
        {
            canFire = true;
            _targetXRotation = arcsin2a / 2;
        }
        else canFire = false;
        _targetYRotation = Vector3.SignedAngle(Vector3.ProjectOnPlane(firePoint.forward, Vector3.up), planeForward.normalized, Vector3.up);


        if (_targetYRotation > settings.maxAngleY) result.y = 1;
        if (_targetYRotation < settings.minAngleY) result.y = -1;

        _targetXRotation = Mathf.Clamp(_targetXRotation, settings.minAngleX, settings.maxAngleX);
        _targetYRotation = Mathf.Clamp(_targetYRotation, settings.minAngleY, settings.maxAngleY);


        transform.localRotation = Quaternion.Lerp(transform.localRotation,
                                    _defaultRotation * Quaternion.Euler(-_targetXRotation, _targetYRotation, 0),
                                     Time.deltaTime * settings.lerpRotationMultiplier);
        return result;
    }


    public List<Vector3> GetPath(int pointsCount)
    {
        return new List<Vector3>();
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movable = GetComponentInParent<IMovable>();
        _defaultRotation = transform.localRotation;
        // _shell = GetComponent<IShell>();
    }

}
