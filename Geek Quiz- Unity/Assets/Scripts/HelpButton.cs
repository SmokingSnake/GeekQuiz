using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour {

    GameInit gi;
	void Start () 
    {
        gi = GameObject.Find("GameInitializer").GetComponent<GameInit>();
	}
	
    void OnClick()
    {    
        gi.openMenuHelp = !gi.openMenuHelp;
        OpenHelp();
    }

    public void OpenHelp()
    {
        
        if (gi.openMenuHelp)
        {
            gi.skipButton.SetActive(true);
            gi.hintButtonNormal.SetActive(true);
            gi.hintButtonStrong.SetActive(true);
            gi.helpBackground.SetActive(true);
            gi.facebookButton.SetActive(true);
            gi.getMoreButton.SetActive(true);
            
            gi.eraseButton.SetActive(false);
            gi.labelResposta.SetActive(false);

            //gi.DesligarBotao();
        }
        else
        {
            gi.openHints = false;
            gi.hintBackground.SetActive(false);
            gi.skipButton.SetActive(false);
            gi.hintButtonNormal.SetActive(false);
            gi.hintButtonStrong.SetActive(false);
            gi.helpBackground.SetActive(false);
            gi.facebookButton.SetActive(false);
            gi.getMoreButton.SetActive(false);
            gi.hintBackground.SetActive(false);

            gi.labelHint.GetComponent<UILabel>().text = " ";
                
            gi.eraseButton.SetActive(true);
            gi.labelResposta.SetActive(true);

            
            //gi.ReligarBotao();
        }

    }
}
