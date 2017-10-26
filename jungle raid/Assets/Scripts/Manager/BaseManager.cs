using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager  {

	public virtual  void OnInit() { }
    public virtual  void OnDestroy() { }
    protected GameFacade facade;

    public BaseManager(GameFacade facade)
    {
        this.facade = facade;
    }
}
