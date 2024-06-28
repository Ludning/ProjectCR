using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : SingleTon<MessageManager>
{
    private Dictionary<Type, Delegate> _uiDic = new Dictionary<Type, Delegate>();

    /*private class MessageHandler<T> where T : MessageBase
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
    }*/
    public bool RegisterCallback<T>(Action<T> messageCallback) where T : MessageBase
    {
        Type key = typeof(T);

        if (_uiDic.TryGetValue(key, out Delegate existingCallback))
        {
            // 기존 델리게이트가 있는 경우
            if (existingCallback is Action<T> existingAction)
            {
                _uiDic[key] = existingAction + messageCallback;
                return true;
            }
            else
            {
                Debug.LogError($"_uiDic[{key}]는 Action<{key}> 타입이 아닙니다.");
                return false;
            }
        }

        // 기존 델리게이트가 없는 경우
        _uiDic[key] = messageCallback;
        return true;
    }

    public bool UnRegisterCallback<T>(Action<T> messageCallback) where T : MessageBase
    {
        Type key = typeof(T);

        if (!_uiDic.TryGetValue(key, out Delegate existingCallback)) return true;
        
        // 기존 델리게이트가 있는 경우
        if (existingCallback is Action<T> existingAction)
        {
            // 델리게이트에서 messageCallback을 제거
            existingAction -= messageCallback;

            if (existingAction == null)
            {
                // 델리게이트가 null이면 사전에서 키를 삭제
                _uiDic.Remove(key);
            }
            else
            {
                // 사전에 업데이트된 델리게이트 저장
                _uiDic[key] = existingAction;
            }
            return true;
        }
        else
        {
            Debug.LogError($"_uiDic[{key}]는 Action<{key}> 타입이 아닙니다.");
            return false;
        }
    }
    public void InvokeCallback<T>(T message) where T : MessageBase
    {
        if (_uiDic.TryGetValue(typeof(T), out Delegate value))
        {
            ((Action<T>)value).Invoke(message);
        }
    }
}