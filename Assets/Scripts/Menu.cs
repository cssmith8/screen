using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviourPunCallbacks
{
    private int creationAttempts = 0;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_InputField usernameField;
    //public string roomName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRoom()
    {
        //RoomOptions roomOptions = new RoomOptions();
        //roomOptions.CustomRoomPropertiesForLobby = new string[] { "started" };
        //roomOptions.IsVisible = false;
        //PhotonNetwork.CreateRoom("test10", roomOptions);
        //roomName = CreateRandomName();
        
        PhotonNetwork.CreateRoom(CreateRandomName());
    }

    public override void OnCreatedRoom()
    {
        var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        hash["started"] = false;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void OnCreateRoomFailed()
    {
        creationAttempts++;
        if (creationAttempts < 10)
        {
            CreateRoom();
        }
        else
        {
            Debug.Log("Failed to create room");
        }
    }

    public void JoinRoom()
    {
        if (usernameField.text == "")
        {
            usernameField.text = "Player";
        }
        PhotonNetwork.NickName = usernameField.text;
        PhotonNetwork.JoinRoom(inputField.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room. Error " + returnCode.ToString() + " message: " + message);
    }

    public override void OnJoinedRoom()
    {
        var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        if ((bool)hash["started"] == false)
        {
            GameObject go = PhotonNetwork.Instantiate(gameCanvas.name, transform.position, transform.rotation);
        } else
        {
            //game in progress
            SceneData.Instance.started = true;
            PhotonNetwork.LoadLevel("Map1");
        }
        
    }

    private string CreateRandomName(int length = 6)
    {
        string name = "";

        for (int counter = 1; counter <= length; ++counter) {
            int rand = Random.Range(97, 123);
            name += (char)rand;
        }

        return name;
    }

    
}
