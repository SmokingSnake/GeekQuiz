using UnityEngine;
using System.Collections;

public class SelectGame : MonoBehaviour 
{
    public int nLevel;

    public GameObject preFabBotao;
    public UIDraggablePanel panel;
    public GameObject grid;
	// Use this for initialization
	void Start () 
    {
       	Inicializar();


	}

 
    void Inicializar() 
    {
        //yield return new WaitForSeconds(4);
        for (int i = 0; i < DataCenter.levels[nLevel].jogos.Length; i++)
        {
            GameObject go = Instantiate(preFabBotao, new Vector3 (0,0,0) , this.transform.rotation) as GameObject;
            go.transform.parent = grid.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<UIDragPanelContents>().draggablePanel = panel;
            go.GetComponentInChildren<UITexture>().mainTexture = DataCenter.imagensBundle.Load(DataCenter.levels[nLevel].jogos[i].linkImagem) as Texture2D;
            grid.GetComponent<UIGrid>().repositionNow = true;
        }
    }

}
