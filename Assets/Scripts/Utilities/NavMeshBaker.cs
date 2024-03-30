using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface Surface2D;

	void Start()
	{
        Door.LevelSwitched += Rebuild;
	}

    void OnDestroy()
    {
        Door.LevelSwitched -= Rebuild;
    }

    private void Rebuild(Room r1, Room r2) => Rebuild();

    private void Rebuild()
    {
		Surface2D.BuildNavMeshAsync();
    }
}
