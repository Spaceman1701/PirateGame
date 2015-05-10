using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public interface MeshData
{
    Vector3[] Verticies
    {
        get;
    }

    Vector2[] Normals
    {
        get;
    }

    Vector2[] TextureCoords
    {
        get;
    }
}
