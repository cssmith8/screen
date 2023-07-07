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
            SceneData.Instance.localPlayer = gameObject;
            pv.RPC("AddRenderTexture", RpcTarget.All, null);

            if (SceneData.Instance.started == true)
            {
                pv.RPC("RequestCamera", RpcTarget.All, PhotonNetwork.LocalPlayer);
            }


            GetComponent<PlayerMovement>().enabled = true;
            transform.GetChild(0).gameObject.GetComponent<CameraControl>().enabled = true;

            transform.position = new Vector3(0f, 2f, 0f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void AddRenderTexture()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().AddPlayer(transform.GetChild(0).gameObject.GetComponent<Camera>(), GetComponent<PhotonView>().Owner);
    }

    [PunRPC]
    void RequestCamera(Photon.Realtime.Player p)
    {
        if (p != PhotonNetwork.LocalPlayer)
        SceneData.Instance.localPlayer.GetComponent<PhotonView>().RPC("AddRenderTexture", p, null);
    }
}
