using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtleManager : MonoBehaviour {

    public static ButtleManager _Instance;

    public List<GameObject> m_list;
    //字典
   // public Dictionary<string, List<GameObject>> m_dic;

    void Awake()
    {
        _Instance = this;
      //  m_dic = new Dictionary<string, List<GameObject>>();
    }

    void Start()
    {
        //1.初始化子弹的表
        Debug.Log("初始化 子弹");
        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/g_Buttle")) as GameObject;
        Init(go);

    }

    //初始化对象池里面的某个对象
    public void Init(GameObject go)
    {
        m_list = new List<GameObject>();
        m_list.Add(go);
        //m_dic.Add(_name, m_list);
        go.SetActive(false);
    }

    //得到对象
    public GameObject GetGameObjectInDic(string _name)
    {
        string str = "Prefabs/" + _name;
        //如果存在
        if (m_list.Count>0)
        {
            for (int i = 0; i < m_list.Count; ++i)
            {
                if (!m_list[i].activeSelf)
                {
                    m_list[i].SetActive(true);
                    return m_list[i];
                }
            }

            GameObject go1 = GameObject.Instantiate(Resources.Load(str)) as GameObject;
            m_list.Add(go1);
            return go1;

        }

        GameObject go2 = GameObject.Instantiate(Resources.Load(str)) as GameObject;
        Init(go2);
        return go2;
    }
}
