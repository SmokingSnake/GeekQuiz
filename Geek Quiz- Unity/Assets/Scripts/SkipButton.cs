using UnityEngine;
using System.Collections;

public class SkipButton : MonoBehaviour 
{

    
	void Start () 
    {
        
	}

    void OnClick()
    {
        GameInit gi =  GameObject.Find("GameInitializer").GetComponent<GameInit>();
        gi.Reinicializar();
        gi.openHints = false;
        gi.hintBackground.SetActive(false);
        //gd = gi.gameData;
        //gd.Skip();
    }
}
