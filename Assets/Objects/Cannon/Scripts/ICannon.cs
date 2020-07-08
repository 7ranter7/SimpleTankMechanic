using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICannon
{
    bool CanFire(Vector3 destination);
    void Fire(Vector3 destination);
    Vector3 Rotate(Vector3 destination);
    List<Vector3> GetPath(int pointCount);
}
