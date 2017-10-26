using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPanel : BasePanel {
    public override void OnEnter()
    {
        base.OnEnter();
        Button logingButton = transform.Find("LoginBtn").GetComponent<Button>();
        logingButton.onClick.AddListener(OnLogionBtnClick);
    }

    public void OnLogionBtnClick()
    {
          _uiMgr.PushPanel(UIPanelType.Login);
    }
}
