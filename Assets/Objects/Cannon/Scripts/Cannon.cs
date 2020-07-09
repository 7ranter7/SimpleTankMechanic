using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, ICannon
{

    [SerializeField] private CannonSettings _settings;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _effectPrefab;
    private Rigidbody _rigidbody;
    private IMovable _movable;
    private float _targetXRotation;
    private float _targetYRotation;

    private Quaternion _defaultRotation;
    private bool _canReach = false;

    private float _timer = 0;


    public bool CanFire()
    {
        if (_timer >= _settings.cooldown)
        {
            _timer = 0;
            return _canReach && true;
        }
        else return false;
    }

    public bool CanReach()
    {
        return _canReach;
    }
    public void Fire()
    {
        IShell shell;
        var shellObject = Instantiate(_settings.shellPrefab, _firePoint.position, _firePoint.rotation);
        shell = shellObject.GetComponent<IShell>();
        if (shell != null)
        {
            if (_effectPrefab != null)
            {
                Instantiate(_effectPrefab, _firePoint.position, _firePoint.rotation);
            }
            shell.Fire(_settings.shellStartSpeed);
        }
        else
        {
            DestroyImmediate(shellObject);
        }
    }



    public Vector3 Rotate(Vector3 destination)
    {
        Vector3 result = Vector3.zero;

        //Math for calculate cannon angle for shoot 
        float x, y;
        float desc;
        float angle1, angle2;
        float a, b, c;
        x = Vector3.ProjectOnPlane(destination - _firePoint.position, Vector3.up).magnitude;
        y = Vector3.Dot(Vector3.up, destination - _firePoint.position);

        a = (Physics.gravity.magnitude * x) / 2 / _settings.shellStartSpeed / _settings.shellStartSpeed;
        b = -1;
        c = (y / x) + (Physics.gravity.magnitude * x / 2 / _settings.shellStartSpeed / _settings.shellStartSpeed);

        desc = b * b - 4 * a * c;
        if (desc >= 0)
        {
            angle1 = Mathf.Rad2Deg * Mathf.Atan((-b + Mathf.Sqrt(desc)) / (2 * a));
            angle2 = Mathf.Rad2Deg * Mathf.Atan((-b - Mathf.Sqrt(desc)) / (2 * a));


            //Choosing fit max angle 
            if ((angle1 >= _settings.minAngleX && angle1 <= _settings.maxAngleX) || (angle2 >= _settings.minAngleX && angle2 <= _settings.maxAngleX))
            {
                _canReach = true;
            }
            else
            {
                _canReach = false;
            }
            angle1 = Mathf.Clamp(angle1, _settings.minAngleX, _settings.maxAngleX);
            angle2 = Mathf.Clamp(angle2, _settings.minAngleX, _settings.maxAngleX);
            _targetXRotation = Mathf.Max(angle1, angle2);
        }
        else _canReach = false;


        //Y
        _targetYRotation = Vector3.SignedAngle(Vector3.ProjectOnPlane(transform.parent.forward, transform.parent.up).normalized,
                                     Vector3.ProjectOnPlane(destination - transform.position, transform.parent.up).normalized,
                                    transform.parent.up);
        //Overturn
        if (_targetYRotation > _settings.maxAngleY) result.y = 1;
        if (_targetYRotation < _settings.minAngleY) result.y = -1;



        _targetYRotation = Mathf.Clamp(_targetYRotation, _settings.minAngleY, _settings.maxAngleY);
        //Apply rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation,
                                Quaternion.Euler(-_targetXRotation, _targetYRotation, 0),
                                 Time.deltaTime * _settings.lerpRotationMultiplier);
        return result;
    }


    public List<Vector3> GetPath(int pointsCount)
    {
        var result = new List<Vector3>();
        float t = 0;
        for (int i = 0; i < pointsCount; i++)
        {
            t = (float)i / _settings.shellStartSpeed;
            result.Add(_firePoint.position + _firePoint.forward * _settings.shellStartSpeed * t + Physics.gravity * t * t / 2);
        }
        return result;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movable = GetComponentInParent<IMovable>();
        _defaultRotation = transform.localRotation;
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        _timer += Time.deltaTime;

    }

}
