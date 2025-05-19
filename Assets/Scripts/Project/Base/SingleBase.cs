using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBase<T> : MonoBehaviour where T : SingleBase<T>
{
    public static T INSTANCE;

    protected virtual void Awake()
    {
        if (INSTANCE != null) 
        {
            Debug.LogError(this + "不符合单例模式");
        }
        INSTANCE = (T)this;
    }

    protected virtual void OnDestroy()
    {
        Destroy();         
    }


    public void Destroy()
    {
        INSTANCE = null;
    }
}