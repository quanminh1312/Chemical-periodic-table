using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
        Move();
        Zoom();
    }

    void Move()
    {
        var y = transform.position.y  + (Input.GetAxis("Vertical") * Time.deltaTime * 10);

        y = Mathf.Clamp(y, -2, 2);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    void Zoom()
    {
        var value = transform.position.z + (Input.GetAxis("Mouse ScrollWheel") * 10);
        value = Mathf.Clamp(value, -20, -2.5f);
    }
}