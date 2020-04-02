using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runing : MonoBehaviour
{
    public BehaviorTree m_bt;
    void Start()
    {
        // 动态添加行为树
        var bt = gameObject.AddComponent<BehaviorTree>();
        // 加载行为树资源
        var extBt = Resources.Load<ExternalBehaviorTree>("Behavior");
        bt.StartWhenEnabled = false;
        // 设置行为树
        bt.ExternalBehavior = extBt;
        bt.EnableBehavior();
        bt.RestartWhenComplete = true;
        // 把行为树对象缓存起来，后面需要通过它来设置变量和发送时间
        m_bt = bt;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 吃红烧牛肉面
            m_bt.SetVariableValue("food", "红烧牛肉面");
            m_bt.SendEvent("Eat");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            // 睡10000秒
            m_bt.SetVariableValue("sleeptime", 10f);
            m_bt.SendEvent("Sleep");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            // 地震啦
            m_bt.SendEvent("EarthQuake");
        }
    }

}
