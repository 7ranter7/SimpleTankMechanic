using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MovableRigidbody : MonoBehaviour, IMovable
{

    [SerializeField] private LayerMask _groundColliders;
    [SerializeField] private MovableRigidbodySettings _settings;


    private Rigidbody _rigidbody;
    private int _colissionCounter = 0;
    private bool _canMove = false;
    //private float _stuckTimer = 0;


    public void Move(Vector3 axises)
    {
        if (!_canMove) return;
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity,
                                transform.forward * _settings.speed * axises.z,
                                Time.deltaTime * _settings.lerpSpeedMultiplier);
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation,
                                _rigidbody.rotation * Quaternion.AngleAxis(axises.y * _settings.rotationSpeed, transform.up),
                                Time.deltaTime * _settings.lerpRotationSpeedMultiplier);
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (((1 << other.gameObject.layer) & _groundColliders) != 0)
        {
            _canMove = _canMove || Vector3.Angle(other.contacts[0].normal, Vector3.up) < _settings.maxGroundAngle;
            _colissionCounter++;
        }
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionExit(Collision other)
    {
        if (((1 << other.gameObject.layer) & _groundColliders) != 0)
        {
            _colissionCounter--;
            if (_colissionCounter <= 0)
            {
                _colissionCounter = 0;
                _canMove = false;
            }
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        /*if (_colissionCounter == 0 || Vector3.Angle(_rigidbody.transform.up, Vector3.up) > _settings.maxGroundAngle)
        {
            _stuckTimer += Time.deltaTime;

            if (_stuckTimer >= _settings.stuckTime)
            {
                Debug.Log("Happened");
                _rigidbody.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
                //_rigidbody.
                //_rigidbody.isKinematic = true;
                //RaycastHit raycastHit;
                // if (Physics.SphereCast(new Ray(transform.position, -Vector3.up), 100, out raycastHit, 100, _groundColliders))
                //    _rigidbody.MovePosition(raycastHit.point + Vector3.up * 2);
                _stuckTimer = 0;
            }
        }*/
    }
}


