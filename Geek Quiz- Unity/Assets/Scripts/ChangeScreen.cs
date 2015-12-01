using UnityEngine;
using System.Collections;

public class ChangeScreen : MonoBehaviour {


    SwitchScreen ss;

    public int nButton;
	
    public void Start()
    {
        ss = GameObject.Find("DataCenter").GetComponent<SwitchScreen>();
    }

    public void OnClick()
    {
        OnOff();
    }

    void OnOff()
    {
        if (nButton == 1)
        {
            ss.selectLevelScreen.SetActive(true);
            if (ss.selectLevelScreen.active == true)
            {
                ss.menuScreen.SetActive(false);
            }
            
            
        }
        else if (nButton == 2)
        {
            ss.selectLevelScreen.SetActive(false);   
            ss.menuScreen.SetActive(true);
                 
        }
		else if(nButton == 3)
		{
			ss.menuScreen.SetActive(false);
			ss.dataInitializer.SetActive(true);
			ss.gameScreen.SetActive(true);
		}
		else if(nButton == 4)
		{
			ss.gameScreen.SetActive(false);
			ss.dataInitializer.SetActive(false);
			ss.menuScreen.SetActive(true);
		}
		else if(nButton == 5)
		{
			ss.selectLevelScreen.SetActive(false);
			ss.selectGameScreen.SetActive(true);
		}
    }

    
    
}
