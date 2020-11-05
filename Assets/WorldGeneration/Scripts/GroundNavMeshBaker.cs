using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundNavMeshBaker : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;


    private void Start()
    {
        _navMeshSurface.GetComponent<NavMeshSurface>();
        Invoke("BakeNavMesh", 0.5f);  
    }


    private void BakeNavMesh()
    {
        _navMeshSurface.BuildNavMesh();
    }
}
