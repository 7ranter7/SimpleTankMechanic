using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameSettings/Cannon/Cannon", fileName = "Cannon.asset")]
public class CannonSettings : ScriptableObject
{
    public float cooldown;
    [Header("Shell")]
    public GameObject shellPrefab;
    public float shellStartSpeed;
    [Header("Movement")]
    public float lerpRotationMultiplier;
    [Range(-180, 180)]
    public float minAngleX;
    [Range(-180, 180)]
    public float maxAngleX;
    [Range(0, 90)]
    public float minAngleY;
    [Range(0, 90)]
    public float maxAngleY;
}
