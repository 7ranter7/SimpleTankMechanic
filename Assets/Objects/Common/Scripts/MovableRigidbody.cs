using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MovableRigidbody : MonoBehaviour, IMovable
{

    public LayerMask groundColliders;
    public MovableRigidbodySettings settings;


    private Rigidbody _rigidbody;
    private int colissionCounter = 0;
    private bool canMove = false;
    private float stuckTimer = 0;


    public void Move(Vector3 axises)
    {
        if (!canMove) return;
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity,
                                transform.forward * settings.speed * axises.z,
                                Time.deltaTime * settings.lerpSpeedMultiplier);
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation,
                                _rigidbody.rotation * Quaternion.AngleAxis(axises.y * settings.rotationSpeed, transform.up),
                                Time.deltaTime * settings.lerpRotationSpeedMultiplier);
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
        if (((1 >> other.gameObject.layer) & groundColliders) != 0)
        {
            canMove = canMove || Vector3.Angle(other.contacts[0].normal, Vector3.up) < settings.maxGroundAngle;
            colissionCounter++;
        }
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionExit(Collision other)
    {
        if (((1 >> other.gameObject.layer) & groundColliders) != 0)
        {
            colissionCounter--;
            if (colissionCounter <= 0)
            {
                colissionCounter = 0;
                canMove = false;
            }
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        if (colissionCounter == 0 || Vector3.Angle(_rigidbody.transform.up, Vector3.up) > settings.maxGroundAngle)
        {
            stuckTimer += Time.deltaTime;

            if (stuckTimer >= settings.stuckTime)
            {
                Debug.Log("Happened");
                _rigidbody.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
                //_rigidbody.
                //_rigidbody.isKinematic = true;
                //RaycastHit raycastHit;
                // if (Physics.SphereCast(new Ray(transform.position, -Vector3.up), 100, out raycastHit, 100, groundColliders))
                //    _rigidbody.MovePosition(raycastHit.point + Vector3.up * 2);
                stuckTimer = 0;
            }
        }
    }
}


