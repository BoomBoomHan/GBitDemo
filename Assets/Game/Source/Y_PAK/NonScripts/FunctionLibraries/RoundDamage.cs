using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public static class RoundDamage
{
    public static int damage;

    private static int damage_tiles = 0;//岩浆，伤害为0
    private static int prop_tiles = 0;//道具地砖，伤害为0
    private static int normal_tiles = 0;
    public static int x = 0;

    public static int[,] blue;
    public static int[,] red;

    private static void NormalTile()
    {

    }

    private static void AddNumber()//先计算3*3中的数
    {

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                x = blue[i, j] + blue[damage_tiles, damage_tiles] + blue[prop_tiles, prop_tiles];
            }
        }
        
    } 

    private static void AddMatrix(int[] Blue)//计算2*2的矩阵
    {
        AddNumber();
        for (int i = 0; i < Blue.Length-1; i++)
        {
            int y = Blue[0] * Blue[3] - Blue[1] * Blue[2];
        }
    }
}
