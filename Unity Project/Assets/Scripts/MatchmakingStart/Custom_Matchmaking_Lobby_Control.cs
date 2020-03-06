using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Custom_Matchmaking_Lobby_Control : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject lobbyConnectButton;

    [SerializeField] private GameObject lobbyPanel;

    [SerializeField] private GameObject mainPanel;

    public InputField PlayerNameInput;

    private string roomName;

    private int roomSize = 2;

    private List<RoomInfo> roomListings;

    [SerializeField] private Transform roomContainer;

    [SerializeField] private GameObject roomListingPrefab;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        lobbyConnectButton.SetActive(true);
        roomListings = new List<RoomInfo>();
        if (PlayerPrefs.HasKey("NickName"))
        {

            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
        }
        else
        {
            PhotonNetwork.NickName = "player" + Random.Range(0, 1000);
        }

        PlayerNameInput.text = PhotonNetwork.NickName;
    }

    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public void JoinLobbyOnClick()
    {
        mainPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if (roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }

            if (tempIndex != -1)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomContainer.GetChild(tempIndex).gameObject);
            }

            if (room.PlayerCount > 0)
            {
                roomListings.Add(room);
                ListRoom(room);
            }
        }
    }

    private void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate(RoomInfo room) { return room.Name == name; };
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
    }

    public void CreateRoom()
    {
        Debug.Log("Ça crée une room donc tu fais ce que tu veux");
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("on chié frere");
    }

    public void MatchmackingCancel()
    {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
}
