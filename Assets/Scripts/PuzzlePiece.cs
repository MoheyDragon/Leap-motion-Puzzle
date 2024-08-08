using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] MeshFilter meshFilter;
    public void SetMesh(Mesh mesh)
    {
        meshFilter.mesh = mesh;
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
