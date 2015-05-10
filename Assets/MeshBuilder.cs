using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface MeshBuilder
{
    MeshData BuildMesh(Vector3[] outsidePoints, Vector3[] insidePoints);
}
