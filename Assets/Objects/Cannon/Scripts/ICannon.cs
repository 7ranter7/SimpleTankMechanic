using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICannon
{
    bool CanFire();
    bool CanReach();
    void Fire();
    Vector3 Rotate(Vector3 destination);
    List<Vector3> GetPath(int pointCount);
}
