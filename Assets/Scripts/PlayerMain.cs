using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMain : MonoBehaviour
{
    public PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().AddPlayer(transform.GetChild(0).gameObject.GetComponent<Camera>());
        }
    }

    IEnumerator killme()
    {
        for (int i = 0; i < 16; i++)
               {
            yield return new WaitForSeconds(3);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
