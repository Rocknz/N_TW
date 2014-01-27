using UnityEngine;
using System.Collections;

public class UserData : MonoBehaviour {
	private int mHp;
	private int mHpMax;

	private int mXien;
	private int mXienMax;

	private int mAtk;
	private int mDef;
	private int mInt;
	private int mCoin;
	
	private int mHelmetLevel;
	private int mHeadLevel;
	private int mSwordLevel;
	private int mBodyLevel;

	// use instance -> UserData.Instance.Hp 

	private static UserData ins;
	public static UserData Instance {
		get { 
			return ins; 
		}
	}
	public int Hp{
		get { return ins.mHp; }
		set { mHp = value; }
	}
	public int HpMax{
		get { return ins.mHpMax; }
		set { mHpMax = value; }
	}
	public int Xien{
		get { return ins.mXien; }
		set { mXien = value; }
	}
	public int XienMax{
		get { return ins.mXienMax; }
		set { mXienMax = value; }
	}
	public int Atk{
		get { return mAtk; }
		set { mAtk = value; }
	}
	public int Def{
		get { return mDef; }
		set { mDef = value; }
	}
	public int Int{
		get { return mInt; }
		set { mInt = value; }
	}
	public int Coin{
		get { return mCoin; }
		set { mCoin = value; }
	}
	public int HelmetLevel{
		get { return mHelmetLevel; }
		set { mHelmetLevel = value;}
	}
	public int HeadLevel {
		get { return mHeadLevel; }
		set { mHeadLevel = value;}
	}
	public int SwordLevel {
		get { return mSwordLevel; }
		set { mSwordLevel = value;}
	}
	public int BodyLevel {
		get { return mBodyLevel; }
		set { mBodyLevel = value;}
	}
	void Start () {
		// DataLoad
		ins = new UserData();

		//PlayerPrefs -> DataLoad and DataSave

		ins.Hp = PlayerPrefs.GetInt ("Hp");
		ins.HpMax = PlayerPrefs.GetInt ("HpMax");
		ins.Xien = PlayerPrefs.GetInt ("Xien");
		ins.XienMax = PlayerPrefs.GetInt ("XienMax");
		ins.Atk = PlayerPrefs.GetInt ("Atk");
		ins.Def = PlayerPrefs.GetInt ("Def");
		ins.Int = PlayerPrefs.GetInt ("Int");
		ins.Coin = PlayerPrefs.GetInt ("Coin");
		ins.HelmetLevel = PlayerPrefs.GetInt ("HelmetLevel");
		ins.HeadLevel = PlayerPrefs.GetInt ("HeadLevel");
		ins.SwordLevel = PlayerPrefs.GetInt ("SwordLevel");
		ins.BodyLevel = PlayerPrefs.GetInt ("BodyLevel");

		
		PlayerPrefs.SetInt("Hp",20);
		PlayerPrefs.SetInt("HpMax",20);
		PlayerPrefs.SetInt("Xien",0);
		PlayerPrefs.SetInt("XienMax",20);
		PlayerPrefs.SetInt("Atk",10);
		PlayerPrefs.SetInt("Def",10);
		PlayerPrefs.SetInt("Int",10);
		PlayerPrefs.SetInt("Coin",10000);
		PlayerPrefs.SetInt ("HelmetLevel", 0);
		PlayerPrefs.SetInt ("HeadLevel", 0);
		PlayerPrefs.SetInt ("SwordLevel", 0);
		PlayerPrefs.SetInt ("BodyLevel", 0);

		DontDestroyOnLoad(this);	
	}

	void Update () {
	
	}
}
