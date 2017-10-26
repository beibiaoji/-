using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private static GameFacade _Instance;
    public  static GameFacade Instance { get { return _Instance; } }
    private UIManager _uiMgr;

    private AudioManager _audioMgr;

    private PlayerManager _playerMgr;

    private CameraManager _cameraMgr;

    private RequestManager _requestMgr;

    private ClientManager _clientMgr;
    

    void Awake()
    {
        if (_Instance != null)
        {
            Destroy(this.gameObject);
        }
        _Instance = this;
        InitManager();
    }

    private void InitManager()
    {
        _uiMgr=new UIManager(this);
        _audioMgr=new AudioManager(this);
        _playerMgr=new PlayerManager(this);
        _cameraMgr=new CameraManager(this);
        _requestMgr=new RequestManager(this);
        _clientMgr = new ClientManager(this);

        _uiMgr.OnInit();
        _audioMgr.OnInit();
        _playerMgr.OnInit();
        _cameraMgr.OnInit();
        _requestMgr.OnInit();
        _clientMgr.OnInit();
    }

    public void OnDestroy()
    {
        _uiMgr.OnDestroy();
        _audioMgr.OnDestroy();
        _playerMgr.OnDestroy();
        _cameraMgr.OnDestroy();
        _requestMgr.OnDestroy();
        _clientMgr.OnDestroy();
    }

    public void AddRequest(RequestCode requestCode, BaseRequest request)
    {
        _requestMgr.AddRequest(requestCode,request);
    }

    public void RemoveRqquest(RequestCode requestCode)
    {
        _requestMgr.RemoveRequest(requestCode);
    }

    public void HandleResponse(RequestCode requestCode, string data)
    {
        _requestMgr.HandleResponse(requestCode,data);
    }

    public void ShowMessage(string msg)
    {
        _uiMgr.ShowMessage(msg);
    }
}
