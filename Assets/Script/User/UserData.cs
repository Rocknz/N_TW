using UnityEngine;
using System.Collections;

public class UserData : MonoBehaviour {
	private int mHp;
	private int mXien;
	private int mHpMax;
	private int mXienMax;

	private int mAtk;
	private int mDef;
	private int mInt;
	private int mCoin;

	private int mHelmetLevel;
	private int mHeadLevel;
	private int mSwordLevel;
	private int mBodyLevel;

	public static int ITEM_SIZE = 4;
	private bool[] mHelmetExists = new bool[ITEM_SIZE];
	private bool[] mHeadExists = new bool[ITEM_SIZE];
	private bool[] mSwordExists = new bool[ITEM_SIZE];
	private bool[] mBodyExists = new bool[ITEM_SIZE];
	
	private int mhaveGameData;
	private TileStatus[,] mTS = new TileStatus[MainLogic.TILE_SIZE,MainLogic.TILE_SIZE];
	private int[] mUpgradeLevel = new int[4];
	private int mTurn;
	private int mDeadEnemyCount;


	// use instance -> UserData.Instance.Hp 
	private static UserData ins;
	public static UserData Instance {
		get { 
			return ins; 
		}
	}
	public int Turn{
		get { return mTurn; }
		set { mTurn = value;}
	}
	public int DeadEnemyCount{
		get { return mDeadEnemyCount; }
		set { mDeadEnemyCount = value;}
	}
	public int haveGameData {
		get { return mhaveGameData; }
		set { mhaveGameData = value;}
	}
	public int[] UpgradeLevel {
		get { return mUpgradeLevel; }
		set { mUpgradeLevel = value;}
	}
	public TileStatus[,] TS{
		get { return mTS; }
		set { mTS = value;}
	}
	public int Hp{
		get { return mHp; }
		set {
			mHp = value;
			if(mHp > mHpMax) {
				mHp = mHpMax;
			}
		}
	}
	public int HpMax{
		get { return mHpMax; }
		set { mHpMax = value; }
	}
	public int Xien{
		get { return mXien; }
		set { 
			mXien = value; 
			if(mXien > mXienMax) {
				mXien = mXienMax;
			}
		}
	}
	public int XienMax{
		get { return mXienMax; }
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
	public bool[] HelmetExists {
		get { return mHelmetExists; }
		set { mHelmetExists = value;}
	}
	public bool[] HeadExists {
		get { return mHeadExists; }
		set { mHeadExists = value;}
	}
	public bool[] SwordExists {
		get { return mSwordExists; }
		set { mSwordExists = value;}
	}
	public bool[] BodyExists {
		get { return mBodyExists; }
		set { mBodyExists = value;}
	}
	public void LoadGameData(){
		ins.haveGameData = PlayerPrefs.GetInt("haveGameData",0);
		if(ins.haveGameData == 1){
			int[] upgrade = new int[4];
			upgrade[0] = PlayerPrefs.GetInt ("Upgrade(0)");
			upgrade[1] = PlayerPrefs.GetInt ("Upgrade(1)");
			upgrade[2] = PlayerPrefs.GetInt ("Upgrade(2)");
			upgrade[3] = PlayerPrefs.GetInt ("Upgrade(3)");

			ins.UpgradeLevel = upgrade;

			ins.HpMax = PlayerPrefs.GetInt ("G_HpMax");
			ins.XienMax = PlayerPrefs.GetInt ("G_XienMax");
			ins.Hp = PlayerPrefs.GetInt ("G_Hp");
			ins.Xien = PlayerPrefs.GetInt ("G_Xien");
			ins.Coin = PlayerPrefs.GetInt ("G_Coin");
			ins.Turn = PlayerPrefs.GetInt ("G_Turn");

			ins.Atk = PlayerPrefs.GetInt ("G_Atk");
			ins.Def = PlayerPrefs.GetInt ("G_Def");
			ins.Int = PlayerPrefs.GetInt ("G_Int");

			ins.DeadEnemyCount = PlayerPrefs.GetInt ("G_DeadEnemyCount");
			ins.Turn = PlayerPrefs.GetInt("G_Turn");
			int i,j;
			string a;
			for(i=0;i<MainLogic.TILE_SIZE;i++){
				for(j=0;j<MainLogic.TILE_SIZE;j++){
					ins.TS[i,j].myAttack = PlayerPrefs.GetInt("G_T_Atk("+i.ToString()+","+j.ToString ()+")");
					ins.TS[i,j].myHp = PlayerPrefs.GetInt("G_T_Hp("+i.ToString()+","+j.ToString ()+")");
					ins.TS[i,j].myTurn = PlayerPrefs.GetInt("G_T_Turn("+i.ToString()+","+j.ToString ()+")");
					ins.TS[i,j].myX = PlayerPrefs.GetInt("G_T_X("+i.ToString()+","+j.ToString ()+")");
					ins.TS[i,j].myY = PlayerPrefs.GetInt("G_T_Y("+i.ToString()+","+j.ToString ()+")");
					
					a = PlayerPrefs.GetString ("G_T_Type("+i.ToString()+","+j.ToString()+")");
					switch(a){
					case "Enemy":	ins.TS[i,j].myType = MainLogic.TILETYPE.Enemy; break;
					case "Coin":	ins.TS[i,j].myType = MainLogic.TILETYPE.Coin; break;
					case "Potion":	ins.TS[i,j].myType = MainLogic.TILETYPE.Potion; break;
					case "Sword":	ins.TS[i,j].myType = MainLogic.TILETYPE.Sword; break;
					case "Wand":	ins.TS[i,j].myType = MainLogic.TILETYPE.Wand; break;
					}
				}
			}
		}
	}
	public void SaveGame(){
		
		PlayerPrefs.SetInt ("haveGameData",ins.haveGameData);
		PlayerPrefs.SetInt ("Upgrade(0)",ins.UpgradeLevel[0]);
		PlayerPrefs.SetInt ("Upgrade(1)",ins.UpgradeLevel[1]);
		PlayerPrefs.SetInt ("Upgrade(2)",ins.UpgradeLevel[2]);
		PlayerPrefs.SetInt ("Upgrade(3)",ins.UpgradeLevel[3]);

		PlayerPrefs.SetInt ("G_Hp",ins.Hp);
		PlayerPrefs.SetInt ("G_Xien",ins.Xien);
		PlayerPrefs.SetInt ("G_Coin",ins.Coin);
		PlayerPrefs.SetInt ("G_Turn",ins.Turn);
		
		PlayerPrefs.SetInt ("G_Atk",ins.Atk);
		PlayerPrefs.SetInt ("G_Def",ins.Def);
		PlayerPrefs.SetInt ("G_Int",ins.Int);
		PlayerPrefs.SetInt ("G_HpMax",ins.HpMax);
		PlayerPrefs.SetInt ("G_XienMax",ins.XienMax);
		
		PlayerPrefs.SetInt ("G_DeadEnemyCount",ins.DeadEnemyCount);
		PlayerPrefs.SetInt("G_Turn",ins.Turn);

		int i,j;
		string a = "";
		for(i=0;i<MainLogic.TILE_SIZE;i++){
			for(j=0;j<MainLogic.TILE_SIZE;j++){
				PlayerPrefs.SetInt("G_T_Atk("+i.ToString()+","+j.ToString ()+")",ins.TS[i,j].myAttack);
				PlayerPrefs.SetInt("G_T_Hp("+i.ToString()+","+j.ToString ()+")",ins.TS[i,j].myHp);
				PlayerPrefs.SetInt("G_T_Turn("+i.ToString()+","+j.ToString ()+")",ins.TS[i,j].myTurn);
				PlayerPrefs.SetInt("G_T_X("+i.ToString()+","+j.ToString ()+")",ins.TS[i,j].myX);
				PlayerPrefs.SetInt("G_T_Y("+i.ToString()+","+j.ToString ()+")",ins.TS[i,j].myY);

				if(ins.TS[i,j].myType == MainLogic.TILETYPE.Enemy) a = "Enemy";
				else if(ins.TS[i,j].myType == MainLogic.TILETYPE.Coin) a = "Coin";
				else if(ins.TS[i,j].myType == MainLogic.TILETYPE.Potion) a = "Potion";
				else if(ins.TS[i,j].myType == MainLogic.TILETYPE.Sword) a = "Sword";
				else if(ins.TS[i,j].myType == MainLogic.TILETYPE.Wand) a = "Wand";

				PlayerPrefs.SetString ("G_T_Type("+i.ToString()+","+j.ToString()+")",a);
			}
		}
	}
	public void NewStat(){
		//Calculate Stats about item;

		int[] upgrade = new int[4];
		upgrade[0] = 1;
		upgrade[1] = 1;
		upgrade[2] = 1;
		upgrade[3] = 1;

		ins.UpgradeLevel = upgrade;

		ins.Atk = 10;
		ins.Def = 10;
		ins.Int = 10;
		ins.HpMax = 50;
		ins.Hp = 50;
		ins.XienMax = 50;
		ins.Xien = 0;
		ins.Turn = 0;
		ins.DeadEnemyCount = 0;
		ins.haveGameData = 1;


//		ins.Atk = PlayerPrefs.GetInt ("Atk");
//		ins.Def = PlayerPrefs.GetInt ("Def");
//		ins.Int = PlayerPrefs.GetInt ("Int");
//		ins.HpMax = PlayerPrefs.GetInt ("HpMax");
//		ins.XienMax = PlayerPrefs.GetInt ("XienMax");
	}
	void Start () {
		// DataLoad
		ins = new UserData();
		// Load Game Data;

		//PlayerPrefs -> DataLoad and DataSave

		ins.HelmetLevel = PlayerPrefs.GetInt ("HelmetLevel");
		ins.HeadLevel = PlayerPrefs.GetInt ("HeadLevel");
		ins.SwordLevel = PlayerPrefs.GetInt ("SwordLevel");
		ins.BodyLevel = PlayerPrefs.GetInt ("BodyLevel");

		PlayerPrefs.SetInt ("HelmetLevel", 0);
		PlayerPrefs.SetInt ("HeadLevel", 0);
		PlayerPrefs.SetInt ("SwordLevel", 0);
		PlayerPrefs.SetInt ("BodyLevel", 0);

		int i,j;
		for(i=0;i<MainLogic.TILE_SIZE;i++){
			for(j=0;j<MainLogic.TILE_SIZE;j++){
				ins.TS[i,j] = new TileStatus(i,j,0);
			}
		}
		for ( i = 0 ; i < ITEM_SIZE ; i++ ) {
			ins.HelmetExists[i] = i==0?true:false;
			ins.HeadExists[i] = i==0?true:false;
			ins.SwordExists[i] = i==0?true:false;
			ins.BodyExists[i] = i==0?true:false;
		}

		DontDestroyOnLoad(this);	
	}

	void Update () {
	
	}
}
