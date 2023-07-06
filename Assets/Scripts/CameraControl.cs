using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(-1 * Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 500f, 0, 0);
        transform.parent.eulerAngles += new Vector3(0f, Input.GetAxisRaw("Mouse X") * Time.deltaTime * 500f, 0f);
    }
}
