using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFire : MonoBehaviour
{

    private float fireTime = 0.1f;
    private float fireTimer = 0f;

    void Start()
    {
        fireTimer = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeSinceLevelLoad - fireTimer > fireTime)
        {
            fireTimer = Time.timeSinceLevelLoad;
            Fire();
        }
    }

    private void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.GetChild(0).gameObject.transform.position, transform.GetChild(0).gameObject.transform.forward, out hit, 100f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Photon.Realtime.Player p = hit.collider.gameObject.GetComponent<PhotonView>().Owner;
                GetComponent<PhotonView>().RPC("TakeDamage", p, 1);
            }
        }
    }
}
