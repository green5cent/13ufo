using UnityEngine;
using System.Collections;

public class DrawMap : MonoBehaviour {

    // Use this for initialization
    Transform bg;
    Transform land;
    Transform content;
    Transform enemys;

    void Start() {
        bg = GameObject.Find("Mud").GetComponent<Transform>();
        land = GameObject.Find("Land").GetComponent<Transform>();
        content = GameObject.Find("Content").GetComponent<Transform>();
        enemys = GameObject.Find("Enemys").GetComponent<Transform>();
        DrawLandform();

    }

    // Update is called once per frame
    void Update() {

    }

    void DrawLandform()
    {
        DrawContent();
        DrawLand();
        GameObject obj = GameObject.Find("Enemys");
        obj.AddComponent<EnemysController>();
    }

    int SelectMarginDraw(int item, int gridX, int gridY)
    {
        if (gridX == 0 || gridX == Map.width - 1 || gridY == 0 || gridY == Map.height - 1)
            return 2;

        if (gridX - 1 >= 0 && gridX + 1 < Map.width && gridY - 1 >= 0 && gridY + 1 < Map.height)
        {
            bool left = GetLand(item, gridX - 1, gridY);
            bool right = GetLand(item, gridX + 1, gridY);
            bool down = GetLand(item, gridX, gridY - 1);
            bool up = GetLand(item, gridX, gridY + 1);


            //暂时这样，之后试试递归
            if (!left && !up && right && down) return 5;
            if (left && !up && !right && down) return 6;
            if (!left && up && right && !down) return 7;
            if (left && up && !right && !down) return 8;
            if (left && !up && right && down) return 9;
            if (!left && up && right && down) return 10;
            if (left && up && right && !down) return 11;
            if (left && up && !right && down) return 12;
            if (!left && !up && !right && down) return 13;
            if (!left && !up && right && !down) return 14;
            if (!left && up && !right && !down) return 15;
            if (left && !up && !right && !down) return 16;
            if (!left && !right && !up && !down) return 19;

            return 2;
        }
        else
        {
            return 0;
        }

    }

    void DrawLand()
    {
        for (int x = 0; x < Map.width; x++)
        {
            for (int y = 0; y < Map.height; y++)
            {
                string mud = "mud4";

                DrawOne(mud, x, y, bg);

            }
        }
    }

    string SelectBaseDraw(int count, string name)
    {
        int i = (int)Random.Range(1f, (float)count);

        return name + i.ToString();
    }



    void DrawOne(string name, int x, int y, Transform parent)
    {
        Transform item;
        item = Instantiate(Resources.Load<Transform>("ItemLandform"));
        item.SetParent(parent);
        item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(name) as Sprite;

        if (x == 0 || x == Map.width || y == 0 || y == Map.height)
        {
            item.gameObject.AddComponent<BoxCollider2D>();
        }

        item.position = new Vector3(x * 0.4f, y * 0.4f, 0);
    }

    void DrawOne(string name, int x, int y, Transform parent,string prefab)
    {
        Transform item;
        item = Instantiate(Resources.Load<Transform>(prefab));
        item.SetParent(parent);
        item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(name) as Sprite;

        item.position = new Vector3(x * 0.4f, y * 0.4f, -2); //content的z轴设为在其它东西的上边
    }


    //判断是否旁边的东西和它的类型是一样的
    bool GetLand(int item, int neighbourX, int neighbourY)
    {
        int t = 0;
        if(Map.map[neighbourX, neighbourY]==item)
        {
            return true;
        }
        for (int i = 0; i < Map.landCount; i++)
        {
            t = 0;
            t = Map.map[neighbourX,neighbourY] - i;
            if (t == Map.tree || t == Map.brushwood || t == Map.stone || t == Map.house || t == Map.wood||t==Map.monsterStone) //判断是否有他本身
            {
              
                if (i == item )
                    return true;
            }
        }
        return false;
    }
        void DrawContent()
    {
        int t=0;
        int a=0;
         for (int y = 0; y < Map.height; y++)
            {
            for (int x = 0; x < Map.width; x++)
            {
                t = 0;
                 t = Map.map[x, y];
                DrawLandOther(t, x, y);
                for (int i = 0; i < Map.landCount; i++) //找到内容
                {
                    a = 0;
                    a = t - i;
                    if(a == Map.tree)
                    {

                        DrawOne(MapItemName.tree, x, y,content,"Itemtree");
                        DrawLandOther(i, x, y);
                        break;
                    }
                    if(a == Map.brushwood)
                    {

                        DrawOne(MapItemName.brushwood, x, y, content,"Itembrushwood");
                        DrawLandOther(i, x, y);
                        break;
                    }
                    if(a == Map.wood)
                    {

                        DrawOne(MapItemName.wood, x, y, content,"Itemwood");
                        DrawLandOther(i, x, y);
                        break;
                    }
                    if(a == Map.house)
                    {

                        DrawOne(MapItemName.house, x, y, content,"Itemhouse");
                        DrawLandOther(i, x, y);
                        break;
                    }
                    if(a == Map.stone)
                    {

                        DrawOne(MapItemName.stone, x, y, content,"Itemstone");
                        DrawLandOther(i, x, y);
                        break;
                    }
                    if (a == Map.monsterStone)
                    {

                        DrawOne(MapItemName.monsterstone, x, y, enemys, "enemy");
                        DrawLandOther(i, x, y);
                        break;
                    }
                }
            }
        }
    }


    void DrawLandOther(int item, int x, int y)
    {
        if (item == Map.grass)
        {
            int i = SelectMarginDraw(Map.grass, x, y);
            if (i != 0)
            {
                string grass = MapItemName.grass + i.ToString();
                DrawOne(grass, x, y, land);
            }
        }
        if (item == Map.water)
        {

            int i = SelectMarginDraw(Map.water, x, y);
            if (i != 0)
            {
                string water = MapItemName.water + i.ToString();
                DrawOne(water, x, y, land,"Itemwater");
            }

        }
    }

    }
