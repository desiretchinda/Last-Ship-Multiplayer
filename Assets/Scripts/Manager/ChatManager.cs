using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using TMPro;

public class ChatManager : MonoBehaviour, IChatClientListener
{

    ChatClient chatClient;

    public TextMeshProUGUI messageContent;

    public Transform contentChat;

    public TMP_InputField inputMessage;

    public GameObject chat;

    string channetl = "room";

    ChatChannel chatChannel;

    List<TextMeshProUGUI> messageList = new List<TextMeshProUGUI>();

    int lastMessageNumber;

    private void Awake()
    {
        chat.SetActive(false);
    }

    private void Start()
    {
        chatClient = new ChatClient(this);
        ChatAppSettings chatSettings = new ChatAppSettings();
        chatSettings.AppIdChat = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat;
        chatSettings.AppVersion = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
        chatSettings.EnableProtocolFallback = PhotonNetwork.PhotonServerSettings.AppSettings.EnableProtocolFallback;
        chatSettings.FixedRegion = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;
        chatSettings.NetworkLogging = PhotonNetwork.PhotonServerSettings.AppSettings.NetworkLogging;
        chatSettings.Port = (ushort)PhotonNetwork.PhotonServerSettings.AppSettings.Port;
        chatSettings.Protocol = PhotonNetwork.PhotonServerSettings.AppSettings.Protocol;
        chatSettings.Server = PhotonNetwork.PhotonServerSettings.AppSettings.IsDefaultNameServer ? null : PhotonNetwork.PhotonServerSettings.AppSettings.Server;
        chatSettings.ProxyServer = PhotonNetwork.PhotonServerSettings.AppSettings.ProxyServer;
        this.chatClient.AuthValues = new AuthenticationValues(PhotonNetwork.NickName);
        this.chatClient.ConnectUsingSettings(chatSettings);
    }

    private void Update()
    {
        chatClient.Service();
    }

    #region IChatClientListener implementation

    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    public void OnDisconnected()
    {

    }

    public void OnConnected()
    {
        chatClient.Subscribe(channetl);
        Debug.Log("Connect to chat server");
    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        DisplayMessage();
        //if (channelName.Equals(channetl))
        //{
        //    for (int i = 0, length = senders.Length; i < length; i++)
        //    {
        //        Debug.Log(senders[i] + " - " + messages[i]);
        //    }
        //}
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {

    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void DisplayMessage()
    {
        if (!chatClient.PublicChannels.ContainsKey(channetl))
            return;

        chatChannel = chatClient.PublicChannels[channetl];
        int nbMessage = chatChannel.Messages.Count;
        int nextIndex = 0;
        int nbSlot = messageList.Count;
        for (int i = 0, length = messageList.Count; i < length; i++)
        {
            if (nbMessage >= messageList.Count)
            {
                messageList[i].gameObject.SetActive(true);
                messageList[i].text = chatChannel.Senders[i] + "-" + chatChannel.Messages[i];
                nextIndex = i + 1;
                continue;
            }
            messageList[i].gameObject.SetActive(false);
        }

        for (int i = nextIndex, length = nbMessage; i < length; i++)
        {
            //Debug.Log(chatChannel.Senders[i] + " - " + chatChannel.Messages[i]);
            TextMeshProUGUI txt = Instantiate(messageContent, contentChat);
            txt.text = chatChannel.Senders[i] + "-" + chatChannel.Messages[i];
            messageList.Add(txt);
        }
    }

    public void OpenChat()
    {
        DisplayMessage();
        chat.SetActive(true);
    }

    public void OnClickSend()
    {
        if (inputMessage && !string.IsNullOrEmpty(inputMessage.text))
        {
            chatClient.PublishMessage(channetl, inputMessage.text);
            inputMessage.text = "";
        }
    }

    public void OnClose()
    {
        chat.SetActive(false);
    }

    #endregion
}
