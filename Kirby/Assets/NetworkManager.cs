using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField NickNameInput;
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;

    private void Awake() //모든 플레이어가 실행되고 숫자 높으면 딜레이가 줄어든다함
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings(); //서버접속

    public override void OnConnectedToMaster()  //서버접속 콜백
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; //닉네임 적기
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 5 }, null); //방만들기
    }

    public override void OnJoinedRoom() //방만들면 콜백이되서 이름 입력란(DisconnectPanel) 비활성화
    {
        DisconnectPanel.SetActive(false);
        Spawn();
    }

    void Update() {  if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) PhotonNetwork.Disconnect(); } // { (ESC를 누르고 && 네트워크에 연결되어 있다면) 연결 끊음) }

    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        RespawnPanel.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause) //네트워크 비활성화시 콜백
    {
        DisconnectPanel.SetActive(true); //이름입력란 true
        RespawnPanel.SetActive(false); //리스폰패널 false
    }

}
