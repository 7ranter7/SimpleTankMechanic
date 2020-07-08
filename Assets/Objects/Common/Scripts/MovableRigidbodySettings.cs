﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameSettings/Movable/RigidbodyMovable", fileName = "RigidbodyMovable.asset")]
public class MovableRigidbodySettings : ScriptableObject
{
    public float maxGroundAngle;

    public float lerpSpeedMultiplier;
    public float speed;
    public float lerpRotationSpeedMultiplier;
    public float rotationSpeed;

    public float stuckTime;

}
