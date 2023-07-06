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
    [SerializeField]
    private GameObject gameCanvas;
    [SerializeField]
    private TMP_InputField inputField;
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
        PhotonNetwork.JoinRoom(inputField.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room. Error " + returnCode.ToString() + " message: " + message);
    }

    public override void OnJoinedRoom()
    {
        GameObject go = PhotonNetwork.Instantiate(gameCanvas.name, transform.position, transform.rotation);
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
