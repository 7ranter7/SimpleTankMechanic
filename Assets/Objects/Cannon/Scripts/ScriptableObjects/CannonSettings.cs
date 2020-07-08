using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameSettings/Cannon/Cannon", fileName = "Cannon.asset")]
public class CannonSettings : ScriptableObject
{
    [Header("Shell")]
    public GameObject shellPrefab;
    public float shellStartSpeed;
    [Header("Movement")]
    public float lerpRotationMultiplier;
    public float minAngleX;
    public float maxAngleX;
    public float minAngleY;
    public float maxAngleY;
}
