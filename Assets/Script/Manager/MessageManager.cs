using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    #region SingleTon
    private static MessageManager _instance;
    public static MessageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("UIManager");
                if (go == null)
                {
                    go = new GameObject("UIManager");
                    _instance = go.AddComponent<MessageManager>();
                }
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }
    #endregion

    private Dictionary<Type, Delegate> _uiDic = new Dictionary<Type, Delegate>();

    private class MessageHandler<T> where T : MessageBase
    {
        private readonly Action<T> _callback;

        public MessageHandler(Action<T> callback)
        {
            _callback = callback;
        }

        public void Handle(MessageBase message)
        {
            _callback((T)message);
        }
    }
    
    public bool RegisterCallback<T>(Action<T> messageCallback) where T : MessageBase
    {
        if (_uiDic.ContainsKey(typeof(T)))
            return false;

        _uiDic[typeof(T)] = messageCallback;
        return true;
    }
    public bool UnRegisterCallback<T>() where T : MessageBase
    {
        return _uiDic.Remove(typeof(T));
    }
    
    public void InvokeCallback<T>(T message) where T : MessageBase
    {
        if (_uiDic.TryGetValue(typeof(T), out Delegate value))
        {
            ((Action<T>)value).Invoke(message);
        }
    }
}