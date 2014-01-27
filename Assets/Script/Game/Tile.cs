using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public GameObject myTile;
	public TileStatus myStatus;
	public static tk2dSpriteCollectionData datas = (tk2dSpriteCollectionData)Resources.Load("Tiles Data/Tiles",typeof(tk2dSpriteCollectionData));
	public static tk2dSpriteCollectionData poison = (tk2dSpriteCollectionData)Resources.Load("Tiles Data/Poison",typeof(tk2dSpriteCollectionData));
	public static tk2dSpriteAnimation poisona = (tk2dSpriteAnimation)Resources.Load("Tiles Data/PoisonAni",typeof(tk2dSpriteAnimation));
	public static Vector3 Position(int y,int x){
		return (new Vector3((float)(x*3.5-10.5f),(float)(y*3.5-10.5f),(float)(0.0f)));
	}

	public Tile(int y,int x,GameObject l){
		myTile = new GameObject("Tile ("+y+","+x+")");
		myTile.AddComponent<tk2dSprite>();
		myTile.AddComponent<tk2dSpriteAnimator>();
		myTile.GetComponent<tk2dSpriteAnimator>().Library = poisona;
		myTile.gameObject.transform.parent = l.transform;
		myStatus = new TileStatus(y,x);
		SetTileByStatus();
	}

	public void SetTileByStatus(){
		SetPosition (myStatus.myY,myStatus.myX);
		SetImage (myStatus.myType);
		SetScale (0.9f);
	}
	private void SetImage(MainLogic.TILETYPE myType){
		tk2dSprite sprite = myTile.GetComponent<tk2dSprite>();
		tk2dSpriteAnimator ani = myTile.GetComponent<tk2dSpriteAnimator>();
		switch(myType){
		case MainLogic.TILETYPE.Enemy:
			ani.playAutomatically = true;
			ani.Play ();
			//sprite.SetSprite(datas,"Enemy");
			break;
		case MainLogic.TILETYPE.Sword:
			ani.Stop();
			sprite.SetSprite(datas,"Sword");
			break;
		case MainLogic.TILETYPE.Wand:
			ani.Stop ();
			sprite.SetSprite(datas,"Wand");
			break;
		case MainLogic.TILETYPE.Coin:
			ani.Stop ();
			sprite.SetSprite(datas,"Coin");
			break;
		case MainLogic.TILETYPE.Potion:
			ani.Stop ();
			sprite.SetSprite(datas,"Potion");
			break;
		}
	}
	public void SetScale(float x){
		if(myStatus.myType == MainLogic.TILETYPE.Enemy){
			x *= 1.5f;
		}
		myTile.transform.localScale = new Vector3(x,x,1);
	}
	public void SetPosition(int y,int x){
		myTile.transform.localPosition = Position (y,x);
	}
}
