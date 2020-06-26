using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float spinSpeed;

    void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }

    public void ChangeRotation()
    {
        spinSpeed = -spinSpeed;
    }
}
