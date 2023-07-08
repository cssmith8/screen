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
            GetComponent<PlayerFire>().enabled = true;
            transform.GetChild(0).gameObject.GetComponent<CameraControl>().enabled = true;

            transform.position = new Vector3(0f, 2f, 0f);

            InvokeRepeating("SendCameraUpdate", 0, 0.1f);
        }
    }

    void SendCameraUpdate()
    {
        pv.RPC("CameraUpdate", RpcTarget.All, transform.GetChild(0).gameObject.transform.rotation);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void CameraUpdate(Quaternion r)
    {
        if (gameObject != SceneData.Instance.localPlayer)
        {
            //transform.GetChild(0).gameObject.transform.rotation = r;
            StartCoroutine(CameraChange(r));
        }
    }

    IEnumerator CameraChange(Quaternion r)
    {
        Quaternion start = transform.GetChild(0).gameObject.transform.rotation;
        float t = 0;
        while (t < 0.1f)
        {
            transform.GetChild(0).gameObject.transform.rotation = Quaternion.Lerp(start, r, t * 10);
            t += Time.deltaTime;
            yield return null;
        }
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

    [PunRPC]
    void TakeDamage(int damage)
    {
        SceneData.Instance.localPlayer.transform.position += new Vector3(0f, 1f, 0f);
    }
}
