using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public GameObject myTile;
	public GameObject myHp;
	public GameObject myAttack;
	public GameObject AttackEffect;

	public TileStatus myStatus;
	public static tk2dSpriteCollectionData datas = (tk2dSpriteCollectionData)Resources.Load("Tiles Data/Tiles",typeof(tk2dSpriteCollectionData));
	public static tk2dSpriteCollectionData poison = (tk2dSpriteCollectionData)Resources.Load("Tiles Data/Poison",typeof(tk2dSpriteCollectionData));
	public static tk2dSpriteAnimation poisona = (tk2dSpriteAnimation)Resources.Load("Tiles Data/PoisonAni",typeof(tk2dSpriteAnimation));

	public static Vector3 Position(int y,int x,int z){
		return (new Vector3((float)(x*350-1050.0f),(float)(y*350-1050f),(float)(z)));
	}

	public Tile(int y,int x,GameObject l){
		myTile = new GameObject("Tile ("+y+","+x+")");
		myTile.AddComponent<tk2dSprite>();
		myTile.AddComponent<tk2dSpriteAnimator>();
		myTile.GetComponent<tk2dSpriteAnimator>().Library = poisona;
		myTile.gameObject.transform.parent = l.transform;
		
		myStatus = new TileStatus(y,x,0);


		myHp = (GameObject)Instantiate(GameObject.Find ("Hp Label"));
		myHp.name = "Hp";
		myHp.transform.parent = myTile.transform;
		myHp.transform.localPosition = new Vector3(150,100,-1);
		myHp.GetComponent<UILabel>().color = new Color(255,0,0);

		myAttack = (GameObject)Instantiate (GameObject.Find ("Hp Label"));
		myAttack.name = "Atk";
		myAttack.transform.parent = myTile.transform;
		myAttack.transform.localPosition = new Vector3(150,-150,-1);
		
		AttackEffect = (GameObject)Instantiate(GameObject.Find ("Hp sample"));
		AttackEffect.name = "Atk_Eft";
		AttackEffect.transform.parent = myTile.transform;
		AttackEffect.transform.localPosition = new Vector3(0,0,-2);
		AttackEffect.transform.localScale = new Vector3(0,0,0);
		tk2dTextMesh tm = AttackEffect.GetComponent<tk2dTextMesh>();
		tm.color = new Color(255,0,0);
		tm.anchor = TextAnchor.MiddleCenter;
		tm.text = "0";
		tm.Commit ();

		SetHp();
		SetAtk();
		SetTileByStatus();
	}
	public void SetHp(){
		UILabel label = myHp.GetComponent<UILabel>();
		label.text = myStatus.myHp.ToString();
	}
	public void SetAtk(){
		UILabel label = myAttack.GetComponent<UILabel>();
		label.text = myStatus.myAttack.ToString();
	}
	public void BeAttacked(int damage){
		myStatus.myHp -= damage;
		if(myStatus.myHp < 0) myStatus.myHp = 0;
		SetHp ();
	}
	public void SetTileByStatus(){
		int z = 2;
		if(myStatus.myType == MainLogic.TILETYPE.Enemy){
			z = 1;
		}
		SetPosition (myStatus.myY,myStatus.myX,z);
		SetImage (myStatus.myType);
		SetScale (1.0f);
	}
	private void SettingEnemy(){
		tk2dSpriteAnimator ani = myTile.GetComponent<tk2dSpriteAnimator>();
		ani.playAutomatically = true;
		ani.Play ();
		myTile.GetComponent<tk2dSprite>().scale = new Vector3(135.0f,135.0f,0.0f);
		SetHp ();
		SetAtk ();
		myHp.GetComponent<UILabel>().fontSize = 100;
		myAttack.GetComponent<UILabel>().fontSize = 100;
	}
	private void SettingNotEnemy(){
		tk2dSpriteAnimator ani = myTile.GetComponent<tk2dSpriteAnimator>();
		ani.playAutomatically = false;
		ani.Stop();
		myTile.GetComponent<tk2dSprite>().scale = new Vector3(90.0f,90.0f,0.0f);
		SetHp ();
		SetAtk ();
		myHp.GetComponent<UILabel>().fontSize = 0;
		myAttack.GetComponent<UILabel>().fontSize = 0;
	}
	private void SetImage(MainLogic.TILETYPE myType){
		tk2dSprite sprite = myTile.GetComponent<tk2dSprite>();
		switch(myType){
		case MainLogic.TILETYPE.Enemy:
			SettingEnemy();
			//sprite.SetSprite(datas,"Enemy");
			break;
		case MainLogic.TILETYPE.Sword:
			sprite.SetSprite(datas,"Sword");
			SettingNotEnemy();
			break;
		case MainLogic.TILETYPE.Wand:
			sprite.SetSprite(datas,"Wand");
			SettingNotEnemy();
			break;
		case MainLogic.TILETYPE.Coin:
			sprite.SetSprite(datas,"Coin");
			SettingNotEnemy();
			break;
		case MainLogic.TILETYPE.Potion:
			sprite.SetSprite(datas,"Potion");
			SettingNotEnemy();
			break;
		}
	}
	public void SetScale(float x){
		myTile.transform.localScale = new Vector3(x,x,1);
	}
	public void SetPosition(int y,int x,int z){
		myTile.transform.localPosition = Position (y,x,z);
	}
}
