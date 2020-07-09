using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _targetTransfrom;
    [SerializeField] private float _lerpPositionMultiplier;
    [SerializeField] private float _lerpRotationMultiplier;



    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetTransfrom.position, Time.deltaTime * _lerpPositionMultiplier);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetTransfrom.rotation, Time.deltaTime * _lerpRotationMultiplier);
    }
}
