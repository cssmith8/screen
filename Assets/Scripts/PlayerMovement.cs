using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [HideInInspector] private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float ymotion = 0;
        float xmotion = 0;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ymotion = 1;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ymotion = -1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) xmotion = -1;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) xmotion = 1;

        if (xmotion * ymotion != 0f)
        {
            xmotion /= Mathf.Sqrt(2);
            ymotion /= Mathf.Sqrt(2);
        }

        transform.position += ymotion * transform.forward * moveSpeed * Time.fixedDeltaTime;
        transform.position += xmotion * transform.right * moveSpeed * Time.fixedDeltaTime;

        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);

    }
}
