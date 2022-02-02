using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPosition : MonoBehaviour
{
    public Transform tran = null;
    public bool gotPos = false;
    public void GetPosition(Transform transform)
    {
         tran = transform;
        gotPos = true;
        Debug.Log(tran.position);
    }
}
