using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchmakingController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject connectButton;
    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject mainPanel;

    public InputField nameInput;

    private string roomName;
    private int roomSize;

    private List<RoomInfo> roomListings;
    [SerializeField]
    private Transform roomsContainer;
    [SerializeField]
    private GameObject roomListingsPrefab;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        connectButton.SetActive(true);
        roomListings = new List<RoomInfo>();

        if(PlayerPrefs.HasKey("NickName"))
        {
            if(PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player" + Random.Range(0,1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player " + Random.Range(0,1000);
        }
        nameInput.text = PhotonNetwork.NickName;
    }

    public void PlayerNameUpdate(string Input)
    {
        PhotonNetwork.NickName = Input;
        PlayerPrefs.SetString("NickName", Input);
        nameInput.text = Input;
    }

    public void JoinLobbyOnClick()
    {
        mainPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int temp;
        foreach(RoomInfo room in roomList)
        {
            if(roomListings != null)
            {
                temp = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                temp = -1;
            }
            if(temp != -1)
            {
                roomListings.RemoveAt(temp);
            }
            if(room.PlayerCount > 0)
            {
                roomListings.Add(room);
                ListRoom(room);
            }
        }
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate(RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingsPrefab, roomsContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name,room.MaxPlayers, room.PlayerCount);
        }
    }

    public void OnRoomNameChange(string nameIn)
    {
        roomName = nameIn;
    }
    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
    }

    public void CreateRoom()
    {
        Debug.Log("Creating room now...");
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create new room.");
    }

    public void MatchmakingCancel()
    {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }


  
}
