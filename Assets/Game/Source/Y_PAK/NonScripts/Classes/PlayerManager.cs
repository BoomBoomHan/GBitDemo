using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public class PlayerManager:MonoBehaviour
{
    [SerializeField, DisplayName("������ɫ")]
    public GameObject Blue_GameObject;
    [SerializeField, DisplayName("����Ѫ��")]
    public float Blue_Blood;


    [SerializeField, DisplayName("�췽��ɫ")]
    public GameObject Red_GameObject;
    [SerializeField, DisplayName("�췽Ѫ��")]
    public float Red_Blood;





    public PlayerManager()
    {
        
    }
}
