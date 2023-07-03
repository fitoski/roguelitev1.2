using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    [SerializeField] private float disposeTime = 15f;
    void Start()
    {
        Destroy(gameObject, disposeTime);
    }
}
