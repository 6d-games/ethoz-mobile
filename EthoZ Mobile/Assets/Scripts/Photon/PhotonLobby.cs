using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject quickJoinButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this; //Creates the singleton, lives within the main menu scene
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Master photon server
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        quickJoinButton.SetActive(true);
    }

    public void OnQuickJoinButtonClicked()
    {
        Debug.Log("Quick Join Button Clicked");
        quickJoinButton.SetActive(false);
        PhotonNetwork.JoinRandomRoom(); //Trying to join a random room.
        cancelButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message) //When JoinRandomRoom fails, this function will be called.
    {
        Debug.Log("Tried to join a random game but failed. There must be no open games available");
        CreateRoom();
    }

    void CreateRoom() //Trying to create a room that does not already exist.
    {
        Debug.Log("Trying to create a new Room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 20 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
        CreateRoom(); //Retrying to create a room that does not already exist.
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel button has been clicked");
        cancelButton.SetActive(false);
        quickJoinButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("Player has left room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We are now in a room");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
