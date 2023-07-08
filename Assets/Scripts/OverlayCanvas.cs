using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class OverlayCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text = "Room Code: " + PhotonNetwork.CurrentRoom.Name;
    }

    
}
