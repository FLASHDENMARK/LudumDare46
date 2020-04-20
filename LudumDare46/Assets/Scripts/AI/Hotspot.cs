using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Hotspot : MonoBehaviour
{
    private List<Tuple<Vector3, bool>> internalPositions;

    void Awake()
    {
        internalPositions = new List<Tuple<Vector3, bool>>();

        foreach (Transform child in transform) {
            internalPositions.Add(new Tuple<Vector3, bool>(child.position, false));
        }
    }

    public bool TakePosition(out Vector3 position) {
        List<Tuple<Vector3, bool>> freePositions = internalPositions.Where(x => x.Item2 == false).ToList();
        bool anyFreePositions = freePositions.Count > 0;

        if (anyFreePositions) {
            Tuple<Vector3, bool> firstFreePosition = freePositions[0];
            int index = internalPositions.FindIndex(x => x.Item1 == firstFreePosition.Item1);
            internalPositions[index] = new Tuple<Vector3, bool>(firstFreePosition.Item1, true);
            position = firstFreePosition.Item1;
        } else {
            position = Vector3.zero;
        }

        return anyFreePositions;
    }

    public void LeavePosition(Vector3 position) {
        int index = internalPositions.FindIndex(x => x.Item1 == position);
        internalPositions[index] = new Tuple<Vector3, bool>(position, false);
    }
}
