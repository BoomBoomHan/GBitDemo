using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomEventInLevels
{
	private static CustomEventCreator[][] creatorsArray;
	
    static CustomEventInLevels()
    {
	    creatorsArray = new CustomEventCreator[][]
	    {
		    new CustomEventCreator[]
		    {
			    new PulseCameraEventCreator(2, 1.0f, 0.5f),
			    new ChangeBackgroundColorEventCreator(20, 5.0f, 4.0f, Color.magenta),
			    new ShowTextEventCreator(20, 5.0f, 4.0f, Color.red, "警告警告，前方高能！", 0.5f),
			    new RevertEventCreator(29, 1.0f, 79, 5.0f),
		    },
		    new CustomEventCreator[]
		    {
			    //1
			    new ShowTextEventCreator(14, 1.0f, 13.0f, Color.white, "翻开了我，已经褪了色的<color=grey>相册</color>。", 0.5f),
			    new ShowTextEventCreator(15, 7.0f, 15.0f, Color.white, "再也看不到<color=red>彩</color><color=orange>虹</color><color=yellow>的</color><color=green>颜</color><color=purple>色</color>。", 0.5f),
			    new ShowTextEventCreator(17, 7.0f, 15.0f, Color.white, "虽然模糊了你，但我看清了自己。", 0.5f),
			    new ShowTextEventCreator(19, 7.0f, 12.0f, Color.white, "守在诺言里骗着自己...", 0.5f),
			    new ShowTextEventCreator(21, 6.0f, 8.0f, Color.white, "哦！为什么？说不出口！", 0.5f),
			    new ShowTextEventCreator(22, 8.0f, 6.5f, Color.white, "难道<color=green>分手</color>就不能做朋友？", 0.5f),
			    new ShowTextEventCreator(23, 7.0f, 8.0f, Color.white, "为什么，你要远走？", 0.5f),
			    new ShowTextEventCreator(24, 8.0f, 6.5f, Color.white, "难道这就是你<color=green>分手</color>的借口？", 0.5f),
			    new ShowTextEventCreator(25, 7.0f, 15.0f, Color.white, "如果让你重新来过，你会不会<color=#FF4170>爱</color>我？", 0.5f),
			    new ShowTextEventCreator(27, 7.0f, 15.0f, Color.white, "<color=#FF4170>爱情</color>让人拥有<i><color=yellow>快乐</color></i>，也会带来<i><color=#550005>折磨</color></i>。", 0.5f),
			    new ShowTextEventCreator(29, 7.0f, 15.0f, Color.white, "曾经和你一起走过，传说中的<b><color=#FF4170>爱河</color></b>，", 0.5f),
			    new ShowTextEventCreator(31, 7.0f, 15.0f, Color.white, "已经被我泪水淹没，变成<i><color=#550005>痛苦</color></i>的<b><color=#FF4170>爱河</color></b>。", 0.5f),
			    //2
			    new ShowTextEventCreator(41, 7.0f, 15.0f, Color.white, "虽然模糊了你，但我看清了自己。", 0.5f),
			    new ShowTextEventCreator(43, 7.0f, 12.0f, Color.white, "守在诺言里骗着自己...", 0.5f),
			    new ShowTextEventCreator(45, 6.0f, 8.0f, Color.white, "哦！为什么？说不出口！", 0.5f),
			    new ShowTextEventCreator(46, 8.0f, 6.5f, Color.white, "难道<color=green>分手</color>就不能做朋友？", 0.5f),
			    new ShowTextEventCreator(47, 7.0f, 8.0f, Color.white, "为什么，你要远走？", 0.5f),
			    new ShowTextEventCreator(48, 8.0f, 6.5f, Color.white, "难道这就是你<color=green>分手</color>的借口？", 0.5f),
			    new ShowTextEventCreator(49, 7.0f, 15.0f, Color.white, "如果让你重新来过，你会不会<color=#FF4170>爱</color>我？", 0.5f),
			    new ShowTextEventCreator(51, 7.0f, 15.0f, Color.white, "<color=#FF4170>爱情</color>让人拥有<i><color=yellow>快乐</color></i>，也会带来<i><color=#550005>折磨</color></i>。", 0.5f),
			    new ShowTextEventCreator(53, 7.0f, 15.0f, Color.white, "曾经和你一起走过，传说中的<b><color=#FF4170>爱河</color></b>，", 0.5f),
			    new ShowTextEventCreator(55, 7.0f, 15.0f, Color.white, "已经被我泪水淹没，变成痛苦的爱河。\n已经被我泪水淹没，变成痛苦的爱河。\n已经被我泪水淹没，变成痛苦的爱河。", 0.5f),
			    new ShowTextEventCreator(57, 7.0f, 15.0f, Color.white, "如果让你重新来过，你会不会<color=#FF4170>爱</color>我？", 0.5f),
			    new ShowTextEventCreator(59, 7.0f, 15.0f, Color.white, "<color=#FF4170>爱情</color>让人拥有<i><color=yellow>快乐</color></i>，也会带来<i><color=#550005>折磨</color></i>。", 0.5f),
			    new ShowTextEventCreator(61, 7.0f, 15.0f, Color.white, "曾经和你一起走过，传说中的<b><color=#FF4170>爱河</color></b>，", 0.5f),
			    new ShowTextEventCreator(63, 7.0f, 15.0f, Color.white, "已经被我泪水淹没，变成<i><color=#550005>痛苦</color></i>的<b><color=#FF4170>爱河</color></b>。", 0.5f),

			    new ShowShadowEventCreator(24, 8.0f, 2.0f, true),
			    new ShowShadowEventCreator(33, 7.0f, 2.0f, false),
			    new ShowShadowEventCreator(48, 8.0f, 2.0f, true),
			    new ShowShadowEventCreator(65, 7.0f, 2.0f, false),
		    },
	    };
    }

    public static CustomEventCreator[] Get(int index)
    {
	    return creatorsArray[index];
    }
}
