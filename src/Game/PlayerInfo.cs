using UnityEngine;
using System.Collections;

public enum InfoType{
	playerName,headName,Hp,Coin,Dem,Exp,Power,
	Level,Def,TotalHp,
	All
}

public class PlayerInfo : MonoBehaviour {
	private static PlayerInfo _instance;

	public static PlayerInfo Instance{
		get{
			if (_instance == null) {
				_instance = GameObject.FindGameObjectWithTag(Tags.game).GetComponent<PlayerInfo> ();
			}
			return _instance;
		}
	}

	private PlayerInfo(){
	}

	private string playerName;
	private int hp = 0;
	private int coin = 0;
	private int dem = 0;
	private int exp = 0;
	private int power = 0;
	private int level = 0;
	private int def = 0;
	private int totalHp = 0;
    private int totaExp = 0;

	public string Name{
		get{
			return playerName;
		}
		set{
			playerName = value;
		}
	}
    

	public int Hp{
		get{
			return hp;
		}
		set{
			hp = value;
		}
	}

	public int Coin{
		get{
			return coin;
		}
		set{
			coin = value;
		}
	}

	public int Dem{
		get{
			return dem;
		}
		set{
			dem = value;
		}
	}

	public int Exp{
		get{
			return exp;
		}
		set{
			exp = value;
		}
	}

    public int TotalExp
    {
        get
        {
            return totaExp;
        }
        set
        {
            totaExp = value;
        }
    }

    public int Power{
		get{
			return power;
		}
		set{
			power = value;
		}
	}

	public int Level{
		get{
			return level;
		}
		set{
			level = value;
		}
	}

	public int Def{
		get{
			return def;
		}
		set{
			def = value;
		}
	}

	public int TotalHp{
		get{
			return totalHp;
		}
		set{
			totalHp = value;
		}
	}

	public delegate void OnInfoChanged(InfoType type);
	public static event OnInfoChanged OnInfoChangedEvent;

	void Start(){
		InitPlayerInfo ();
	}

	void Update(){
	}

	void InitPlayerInfo(){
		this.playerName = "HTY";
		this.hp = 1000;
		this.TotalHp = this.hp;
		this.coin = 10000;
		this.dem = 10;
		this.level = 1;
		this.def = 5;
		this.totaExp = 100 + this.level * 100;//200
        this.exp = 0;

		OnInfoChangedEvent (InfoType.All);
	}

	public void SetName(string newName){
		this.name = newName;
		OnInfoChangedEvent (InfoType.playerName);
	}

	public void CoinJ(int num){
		if (this.coin < num) {
			return;
		}
		coin -= num;
		OnInfoChangedEvent (InfoType.Coin);
	}

	public void AddHp(int value){
		hp += value;
        if (hp > totalHp)
            hp = totalHp;
		OnInfoChangedEvent(InfoType.Hp);
	}

    public void ReduceHp(int value)
    {
        if (hp > value)
            hp -= value;
        else
        {
            //通知死亡；
            hp = 0;
        }
        OnInfoChangedEvent(InfoType.Hp);
    }


    public void AddExp(int count){
		exp = count * 20;//100
        if (exp >= totaExp)
        {
            totaExp += 100 + this.level * 100;
            AddLevel();
        }
        OnInfoChangedEvent(InfoType.Exp);
    }

    public void AddLevel()
    {
        level++;
        OnInfoChangedEvent(InfoType.Level);
    }

	public void AddCoin(int num){
		coin += num;
		OnInfoChangedEvent (InfoType.Coin);
	}

    public void AddDem(int count)
    {
        dem += count;
        OnInfoChangedEvent(InfoType.Dem);
    }

    public void AddDef(int count)
    {
        def += count;
        OnInfoChangedEvent(InfoType.Def);
    }

    public void DesDem(int count)
    {
        dem -= count;
        OnInfoChangedEvent(InfoType.Dem);
    }

    public void DesDef(int count)
    {
        def = count;
        OnInfoChangedEvent(InfoType.Def);
    }

}
