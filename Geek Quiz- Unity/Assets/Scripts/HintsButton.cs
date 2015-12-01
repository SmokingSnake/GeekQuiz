using UnityEngine;
using System.Collections;

public class HintsButton : MonoBehaviour 
{

    public int hintType;
    GameInit gi;
    GameData gd;

    public void Start()
    {
        
        gi = GameObject.Find("GameInitializer").GetComponent<GameInit>();
        //hintType = 0;
    }

    void OnClick()
    {
        //gd.Dica(hintType);
        

        AbreDicas();

    }
    void AbreDicas()
    {
        gd = GameObject.Find("GameInitializer").GetComponent<GameInit>().gameData;
        gi.openHints = !gi.openHints;

        
        if (gi.openHints)
        {
            gi.labelHint.GetComponent<UILabel>().text = gd.Dica(hintType);
          
            gi.hintBackground.SetActive(true);
        }
        else
        {
            gi.labelHint.GetComponent<UILabel>().text = " ";
            gi.hintBackground.SetActive(false);
        }











        //if (hintType == 1) gi.lastHint = 1;
        //else if (hintType == 2) gi.lastHint = 2;
        //else if (hintType == 0) gi.lastHint = 0;

        //if (hintType != 0)
        //{
        //    if (!gi.openHints)
        //    {
        //        if (hintType == gi.lastHint)
        //        {
        //            gi.hintBackground.SetActive(true);
        //        }
        //    }
        //    else
        //    {
        //        if (hintType == gi.lastHint)
        //        {
        //            gi.hintBackground.SetActive(false);
        //        }
        //        else
        //        {
        //            gi.hintBackground.SetActive(true);
        //        }
        //    }

        //}
        //else
        //{
        //    gi.hintBackground.SetActive(false);
        //}
        //print(hintType);
        
    }
}
