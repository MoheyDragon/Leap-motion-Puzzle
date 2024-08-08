using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDebugger : MonoBehaviour
{
    public void OnGrasp(GameObject graspedObject)
    {
        print(graspedObject.name);
    }
}
