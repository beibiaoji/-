﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Common;
public class BaseRequest : MonoBehaviour {

   private RequestCode requestCode=RequestCode.None;

   public virtual void Awake()
    {
        GameFacade.Instance.AddRequest(requestCode,this);
    }

    public virtual void SendRequest()
    {
        
    }

    public virtual void OnResponse(string data)
    {
        
    }

    public virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRqquest(requestCode);
    }
}