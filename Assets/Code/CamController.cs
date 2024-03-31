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
        //var x = transform.position.x + (Input.GetAxis("Horizontal") * Time.deltaTime * 10);
        var y = transform.position.y  + (Input.GetAxis("Vertical") * Time.deltaTime * 10);

        //x = Mathf.Clamp(x, -27, 1);
        y = Mathf.Clamp(y, -2, 2);
        //transform.position = new Vector3(x, y, transform.position.z);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    void Zoom()
    {
        var value = transform.position.z + (Input.GetAxis("Mouse ScrollWheel") * 10);
        value = Mathf.Clamp(value, -20, -2.5f);
        //if (value != 0) gameObject.GetComponent<Camera>().se += value;
    }
}