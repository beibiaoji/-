using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Common;
using GameServer.Server;
using UnityEngine;

public class ClientManager : BaseManager
{
    private const string IP = "127.0.0.1";
    private const int PORT = 6666;
    private Socket clientSocket;
    private  Message msg=new Message();
    public override void OnInit()
    {
        base.OnInit();
        Socket clientSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
        }
        catch (Exception e)
        {
            Debug.Log("无法连接到服务器"+e);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("无法关闭与服务器连接"+e);
           
        }
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }

    private void Start()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback,null);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count,OnProcessDataCallback);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
    }
    /// <summary>
    /// 处理服务器返回信息
    /// </summary>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <param name="arg3"></param>
    private void OnProcessDataCallback(RequestCode requestCode, string data)
    {
        facade.HandleResponse(requestCode,data);
    }

    public ClientManager(GameFacade facade) : base(facade)
    {
    }
}
