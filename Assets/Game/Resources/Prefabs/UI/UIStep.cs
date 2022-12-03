using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStep : MonoBehaviour
{
    [SerializeField, DisplayName("Daven头像")]
    public Sprite Daven;
    [SerializeField, DisplayName("Lusii头像")]
    public Sprite Lusii;

    [SerializeField, DisplayName("Daven血量")]
    public Slider DavenHP;
    [SerializeField, DisplayName("Lusii血量")]
    public Slider LusiiHP;


    [SerializeField, DisplayName("剩余步数")]
    public TMP_Text step;



}
