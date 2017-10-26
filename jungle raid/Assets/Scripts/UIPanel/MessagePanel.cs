using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
public class MessagePanel : BasePanel
{
    private Text _text;
    private float _showTime = 1f;
    public override void OnEnter()
    {
        base.OnEnter();
        _text = GetComponent<Text>();
        _text.enabled = false;
        _uiMgr.InjectMsgPanel(this);
    }

    public void ShowMessage(string msg)
    {
        _text.CrossFadeAlpha(1,1,false);
        _text.text = msg;
        _text.enabled = true;
        Invoke("Hide",_showTime);
    }

    public void Hide()
    {
        _text.CrossFadeAlpha(0,1,false);
    }
}
