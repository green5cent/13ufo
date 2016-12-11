using UnityEngine;
using System.Collections;

public class MapContent : MonoBehaviour {



    [Range(0.1f, 1f)]
    public float RandomFillContent; //是否填充

    void Start () {
        GenerateMapContent();
        GameObject.Find("GameObject").AddComponent<DrawMap>();
    }

    void GenerateMapContent()
    {
        RandomFillMapContent();
    }

    void RandomFillMapContent()
    {
        for (int x = 0; x < Map.width; x++)
        {
            for (int y = 0; y < Map.height; y++)
            {
                float p = Random.Range(0.1f, 1f);
                if (p < RandomFillContent)
                {
                    int p2 = (int)Random.Range(1f, 4f);
                    if ( p2 == 1 && Map.map[x, y] == Map.mud)
                    {
                        Map.map[x, y] += Map.house;
                        continue;
                    }
                    if (p2 == 2 && Map.map[x, y] == Map.mud || p2 == 2 && Map.map[x, y] == Map.water  )
                    {
                        Map.map[x, y] += Map.stone;
                        continue;
                    }
                    if (p2 == 3 && Map.map[x, y] == Map.grass)
                    {

                        int p3 = (int)Random.Range(1f, 4f);
                        switch (p3)
                        {
                            case 1:
                                Map.map[x, y] += Map.tree;
                                break;
                            case 2:
                                Map.map[x, y] += Map.brushwood;
                                break;
                            case 3:
                                Map.map[x, y] += Map.wood;
                                break;
                        }
                        continue;
                    }
                }
            }
        }
    }

}
