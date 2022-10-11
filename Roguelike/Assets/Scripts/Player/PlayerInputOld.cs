using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputOld : MonoBehaviour
{

    private void Update()
    {
        
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += transform.forward;
        if (Input.GetKey(KeyCode.S))
            direction -= transform.forward;
        if (Input.GetKey(KeyCode.D))
            direction += transform.right;
        if (Input.GetKey(KeyCode.A))
            direction -= transform.right;

        return direction;
    }
}
