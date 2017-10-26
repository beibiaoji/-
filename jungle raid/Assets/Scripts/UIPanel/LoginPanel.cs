using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class LoginPanel : BasePanel
{

    private Button _closseBtn;
    private Button _registerBtn;
    private Button _loginBtn;
    private InputField _usernameInput;
    private InputField _passwordInput;
    public override void OnEnter()
    {
        base.OnEnter();
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one,0.5f);
    }
}
