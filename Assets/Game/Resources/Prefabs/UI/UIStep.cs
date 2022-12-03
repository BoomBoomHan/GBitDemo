using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStep : MonoBehaviour
{
    [SerializeField, DisplayName("角色血量")]
    public Slider HP;

    [SerializeField, DisplayName("剩余步数")]
    public TMP_Text step;



}
