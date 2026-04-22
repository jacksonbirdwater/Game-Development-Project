using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestCharController : MonoBehaviour
{
    public float movementSpeed= 10f;
    public SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal") * movementSpeed / 2;
        float vMovement = Input.GetAxis("Vertical") * movementSpeed;
        
        transform.Translate(new Vector3(hMovement, 0, vMovement) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }
}
