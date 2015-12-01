using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {

    GameData gd;
    public int indice;


    void Start()
    {
       
    }

    void OnClick()
    {
        GameInit gi = GameObject.Find("GameInitializer").GetComponent<GameInit>();
        if (!gi.openMenuHelp)
        {
            gd = GameObject.Find("GameInitializer").GetComponent<GameInit>().gameData;
            gd.AdicionarResposta(gameObject.GetComponentInChildren<UILabel>().text.ToCharArray()[0], indice);
            gameObject.SetActive(false);
        }
    }
}
