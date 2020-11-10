using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter 
{
    /// <summary>
    /// 事件表
    /// </summary>
    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType,Delegate>();
    
    /// <summary>
    /// 注册事件，添加监听
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    private static void ListenerAdding(EventType eventType, Delegate callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托是{2}.", eventType, d.GetType(), callBack.GetType()));
        }
    }

    /// <summary>
    /// 无参事件监听器
    /// </summary>
    public static void AddListener(EventType eventType,  CallBack callBack)
    {
        ListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] + callBack;
    }

    /// <summary>
    /// 取消事件监听
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    private static void ListenerRemoving(EventType eventType, Delegate callBack)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("移除监听错误，事件{0}没有对应委托", eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}移除不同类型的委托，当前事件所对应的委托是{1}，要添加的委托是{2}.", eventType, d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除监听错误，没有事件码{0}", eventType));
        }
    }
    private static void ListenerRemoved(EventType eventType)
    {
        if(m_EventTable[eventType]==null)
        {
            m_EventTable.Remove(eventType);
        }
    }
    /// <summary>
    /// 移除无参事件监听器
    /// </summary>
    public static void RemoveListener(EventType eventType,CallBack callBack)
    {
        ListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;
        ListenerRemoved(eventType);
    }

    /// <summary>
    /// 无参事件广播器
    /// </summary>
    /// <param name="eventType"></param>
    public static void BroadCast(EventType eventType)
    {
        Delegate d;
        if(m_EventTable.TryGetValue(eventType,out d))
        {
            CallBack callBack = d as CallBack;
            if(callBack!=null)
            {
                callBack();
            }
            else
            {
                throw new Exception(string.Format("广播事件错误，事件{0}对应委托具有不同类型", eventType));
            }
        }
    }
}
