using UnityEngine;
using System.Collections;

public class Map {

    public const int width = 200;
    public const int height = 200;
    public static int[,] map;
    public const int landCount = 3; //地形总数


    public const int mud = 0;
    public const int water = 1;
    public const int grass = 2;

    public const int tree = 100;
    public const int brushwood = 200;
    public const int wood = 300;
    public const int house = 400;
    public const int stone = 500;

    static Map()
    {
        map = new int[width, height];
    }
}
