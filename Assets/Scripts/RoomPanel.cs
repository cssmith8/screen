using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomPanel : MonoBehaviour
{
    private TMP_Text playerList;
    private Button startButton;
    private TMP_Text roomCode;
    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();

        if (!pv.IsMine)
        {
            gameObject.SetActive(false);
        }


        playerList = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
        roomCode = transform.GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>();
        startButton = transform.GetChild(0).GetChild(2).gameObject.GetComponent<Button>();

        roomCode.text = "Room Code: " + PhotonNetwork.CurrentRoom.Name;

        InvokeRepeating("UpdatePlayerList", 0, 1);
        InvokeRepeating("ShowStartButton", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePlayerList()
    {
        playerList.text = "";

        foreach (Photon.Realtime.Player pl in PhotonNetwork.PlayerList)
        {
            // body of foreach loop
            playerList.text += pl.NickName;
            playerList.text += "\n";
        }
    }

    private void ShowStartButton()
    {
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartButtonPress()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //var hash = PhotonNetwork.CurrentRoom.CustomProperties;
            //hash["started"] = true;
            //PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

            pv.RPC("EnterGame", RpcTarget.All);
        }
    }

    [PunRPC]
    void EnterGame()
    {
        PhotonNetwork.LoadLevel("Map1");
    }
}
