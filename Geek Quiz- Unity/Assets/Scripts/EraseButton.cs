using UnityEngine;
using System.Collections;

public class EraseButton : MonoBehaviour {

    GameData gd;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnClick () 
    {
        GameInit gi = GameObject.Find("GameInitializer").GetComponent<GameInit>();

        gd = gi.gameData;

        gd.DeletarUltimaLetra();
        
	}
}
