using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour, IShell
{
    private Rigidbody _rigidbody;

    public void Fire(float startVelocity)
    {
        Debug.Log("Shell fire");
        //UnityEditor.EditorApplication.isPaused = true;
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
        Debug.Log("Destroy");
        //Destroy(gameObject);
    }
}
