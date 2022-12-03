using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public class PlayerManager:MonoBehaviour
{
    [SerializeField, DisplayName("蓝方角色")]
    public GameObject Blue_GameObject;
    [SerializeField, DisplayName("蓝方血量")]
    public float Blue_Blood;


    [SerializeField, DisplayName("红方角色")]
    public GameObject Red_GameObject;
    [SerializeField, DisplayName("红方血量")]
    public float Red_Blood;





    public PlayerManager()
    {
        
    }
}
