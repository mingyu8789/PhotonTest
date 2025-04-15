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

    private void Awake() //��� �÷��̾ ����ǰ� ���� ������ �����̰� �پ�����
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings(); //��������

    public override void OnConnectedToMaster()  //�������� �ݹ�
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; //�г��� ����
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 5 }, null); //�游���
    }

    public override void OnJoinedRoom() //�游��� �ݹ��̵Ǽ� �̸� �Է¶�(DisconnectPanel) ��Ȱ��ȭ
    {
        DisconnectPanel.SetActive(false);
        Spawn();
    }

    void Update() {  if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) PhotonNetwork.Disconnect(); } // { (ESC�� ������ && ��Ʈ��ũ�� ����Ǿ� �ִٸ�) ���� ����) }

    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        RespawnPanel.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause) //��Ʈ��ũ ��Ȱ��ȭ�� �ݹ�
    {
        DisconnectPanel.SetActive(true); //�̸��Է¶� true
        RespawnPanel.SetActive(false); //�������г� false
    }

}
