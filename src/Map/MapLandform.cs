using UnityEngine;
using System.Collections;
  

public class MapLandform : MonoBehaviour
{
    

    [Range(0, 60)]
    public int randomFillPercent0;

    [Range(60, 100)]
    public int randomFillPercent1;

              

    void Awake()
    {
        GenerateMap();
    }


    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        GenerateMap();
    //    }
    //}

    void GenerateMap()
    {
 //       map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
           SmoothMap();
        }
    }

    void RandomFillMap()
    {

        System.Random pseudoRandom = new System.Random();
        for (int x = 0; x < Map.width; x++)
        {
            for (int y = 0; y < Map.height; y++)
            {
                //暂时先这样，解决总地图四个边不能实现圆滑的问题
                if (x == 0 || x == Map.width - 1 || y == 0 || y == Map.height - 1 || x==Map.width-2||y==Map.height-2)
                {
                    Map.map[x, y] = Map.grass;
                    continue;
                }
                //做判断 随机数在什么区间 
                int p = pseudoRandom.Next(0, 100);
                    if (p <= randomFillPercent0)
                        Map.map[x, y] = Map.water;     //等于水
                    else if (p < randomFillPercent1)
                        Map.map[x, y] = Map.grass;   //等于草地
                    else
                        Map.map[x, y] = Map.mud;  //等于泥地
                }
            }
        }
    

    void SmoothMap()
    {
        for (int x = 0; x < Map.width; x++)
        {
            for (int y = 0; y < Map.height; y++)
            {

                int neighbourStoteTiles = GetSurroundingWallCount(x, y)[0];
                int neighbourCementTiles = GetSurroundingWallCount(x, y)[1];

                if (neighbourStoteTiles >= 4)    
                    Map.map[x, y] = Map.water;
                else if (neighbourCementTiles >= 4)
                    Map.map[x, y] = Map.mud;
                else
                    Map.map[x, y] = Map.grass;

            }
        }
    }

    int[] GetSurroundingWallCount( int gridX, int gridY)
    {
        int[] Count = new int[2];
        Count[0] = Count[1] = 0;

        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < Map.width && neighbourY >= 0 && neighbourY < Map.height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        if (Map.map[neighbourX, neighbourY] == Map.water)
                            Count[0]++;
                        if (Map.map[neighbourX, neighbourY] == Map.mud)
                            Count[1]++;
                    }
                }
            }
        }
        return Count;
    }

        
    //void OnDrawGizmos()
    //{
    //    if (Map.map != null)
    //    {
    //        for (int x = 0; x < Map.width; x++)
    //        {
    //            for (int y = 0; y < Map.height; y++)
    //            {
    //                if (Map.map[x, y] == (int)LandformState.Stone)
    //                    Gizmos.color = Color.blue;
    //                else if (Map.map[x, y] == (int)LandformState.Cement)
    //                    Gizmos.color = Color.gray;
    //                else
    //                    Gizmos.color = Color.green;
    //                Vector3 pos = new Vector3(-Map.width / 2 + x + .5f , -Map.height / 2 + y + .5f,0 );
    //                Gizmos.DrawCube(pos, Vector3.one);
    //            }
    //        }
    //    }
    //}
}