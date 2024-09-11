using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Matrix 
{
   public static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
    public static Vector3 ToIso(this Vector3 moveDireciton) => _isoMatrix.MultiplyPoint3x4(moveDireciton);
}
