using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour, IShell
{
    [SerializeField] private LayerMask _groundColliders;
    [SerializeField] private GameObject _effectPrefab;
    private Rigidbody _rigidbody;

    public void Fire(float startVelocity)
    {
        _rigidbody.velocity = transform.forward * startVelocity;
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
        if (((1 << other.gameObject.layer) & _groundColliders.value) != 0)
        {
            if (_effectPrefab != null)
            {
                Instantiate(_effectPrefab, other.contacts[0].point, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
