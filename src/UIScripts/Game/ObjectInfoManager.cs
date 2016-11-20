using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using Excel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;


public class ObjectInfoManager : MonoBehaviour {
    private static ObjectInfoManager _instance;
    public static ObjectInfoManager Instance {
        get {
            if (_instance==null) {
                _instance = GameObject.FindGameObjectWithTag(Tags.game).GetComponent<ObjectInfoManager>();
            }
            return _instance;
        }
    }

    private ObjectInfoManager() { }


    public TextAsset objectInfoText;
    private string objectInfoStr;

    private Dictionary<int, ObjectInfo> objectInfoDic = new Dictionary<int, ObjectInfo>();



    void Awake()
    {
        objectInfoStr = objectInfoText.ToString();
        SetObjectInfo();
    //Debug.Log(objectInfoDic.Keys.Count);
    }


    public ObjectInfo GetObjectInfo(int id) {
        ObjectInfo info = null;

        objectInfoDic.TryGetValue(id, out info);
       
        return info;
    }

    void SetObjectInfo()
    {
        string[] rowStrs = objectInfoStr.Split('\n');
        foreach (var item in rowStrs)
        {
            string[] cloumnStrs = item.Split(',');
            ObjectInfo info = new ObjectInfo();

            info.Id = int.Parse(cloumnStrs[0]);
            info.Name = cloumnStrs[1];
            info.IconName = cloumnStrs[2];

            switch (cloumnStrs[3])
            {
                case "Equip":
                    info._ObjectType = ObjectType.Equip;
                    break;
                case "Drug":
                    info._ObjectType = ObjectType.Drug;
                    break;
            }

            if (info._ObjectType == ObjectType.Equip)
            {
                switch (cloumnStrs[4]) {
                    case "WuQi":
                        info._EquipType = EquipType.WuQi;
                        break;
                    case "ShouSi":
                        info._EquipType = EquipType.ShouSi;
                        break;
                    case "TouKui":
                        info._EquipType = EquipType.TouKui;
                        break;
                    case "YiFu":
                        info._EquipType = EquipType.YiFu;
                        break;
                    case "XieZi":
                        info._EquipType = EquipType.XieZi;
                        break;
                }
                info.Price = int.Parse(cloumnStrs[5]);
                info.Buy = int.Parse(cloumnStrs[6]);
                info.Hp = int.Parse(cloumnStrs[7]);
                info.Mp = int.Parse(cloumnStrs[8]);
                info.power = int.Parse(cloumnStrs[9]);
                info.dam = int.Parse(cloumnStrs[10]);
                info.des = cloumnStrs[11];


            }
            else if (info._ObjectType == ObjectType.Drug)
            {
                switch (cloumnStrs[4]) {
                    case "Hp":
                        info._drugType = DrugType.Hp;
                        break;
                    case "Mp":
                        info._drugType = DrugType.Mp;
                        break;
                }

                info.Price = int.Parse(cloumnStrs[5]);
                info.Buy = int.Parse(cloumnStrs[6]);
                info.Hp = int.Parse(cloumnStrs[7]);
                info.Mp = int.Parse(cloumnStrs[8]);
                info.des = cloumnStrs[9];

            }

            objectInfoDic.Add(info.Id,info);
        }
    }

}
	//void SetExcel(){

	//	FileStream fileStream = File.Open (Application.dataPath+("/ObjectInfo.xlsx"),FileMode.Open,FileAccess.Read);

	//	IExcelDataReader dataReader = ExcelReaderFactory.CreateOpenXmlReader (fileStream);

	//	DataSet dataSet = dataReader.AsDataSet ();//得到文当的信息

	//	DataTableCollection tabelCollection = dataSet.Tables;//得到所有表格

	//	DataTable dataTabel = tabelCollection [0];//得到单个表格

	//	DataRowCollection rowCollection = dataTabel.Rows;

 //       //foreach (DataRow row in rowCollection) {

 //       //	for (int i = 0; i < 12; i++) {

 //       //	}
 //       //}

 //       for (int i =14; i < 17; i++)
 //       {
 //           DataRow row = rowCollection[i];

 //           for (int j= 0; j < 12; j++)
 //           {
 //               Debug.Log(row[j]);
 //           }
 //       }

	//}







public enum ObjectType{
	Equip,
	Drug

}

public enum EquipType{
	WuQi,
	TouKui,
	ShouSi,
	YiFu,
	XieZi,
	XiangLian
}
public enum DrugType{
	Hp,Mp
}

public class ObjectInfo{
	
	public int Id {
		get;
		set;
	}

	public string Name {
		get;
		set;
	}
	public string IconName {
		get;
		set;
	}
	public ObjectType _ObjectType{
		get;
		set;
	}
	public EquipType _EquipType {
		get;
		set;

	}

	public int Price {
		get;
		set;
	}

	public int Buy {
		get;
		set;
	}
	public int Hp {
		get;
		set;
	}
	public int Mp {
		get;
		set;
	}
	public int power{
		get;
		set;
	}
	public int dam {
		get;
		set;
	}
	public string des {
		get;
		set;
	}

	public DrugType _drugType {
		get;
		set;

	}
}
