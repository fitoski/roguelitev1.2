using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject stonePrefab;
    public float throwInterval = 1f;
    private float nextThrowTime = 0f;
    private Vector3 _spawnPoint => transform.position + transform.forward * 1f + new Vector3(0, 0.5f, 0);
    public float throwForce = 500f;

    void Update()
    {
        if (Time.time >= nextThrowTime)
        {
            ThrowStone();
            nextThrowTime = Time.time + throwInterval;
        }
    }

    private void ThrowStone()
    {
        GameObject newStone = Instantiate(stonePrefab, _spawnPoint, transform.rotation);
        Rigidbody stoneRigidbody = newStone.GetComponent<Rigidbody>();
        Vector3 force = transform.forward * throwForce;
        force.y = 0;
        stoneRigidbody.AddForce(force);
    }

}