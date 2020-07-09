using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SimpleEffect : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private ParticleSystem _particleSystem;
    private float _timer = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Play();
        Destroy(gameObject, _lifeTime);
    }


}
