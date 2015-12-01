using UnityEngine;
using System.Collections;

public class criaBotão : MonoBehaviour 
{
    private const float tamanhoLetraYZ = 0.004662005F;
    private const float tamanhoLetraX = 0.001398601F;
    private const float posisaoLetraX = -96.8585F;
    private const float posisaoLetraY = -54.03744F;

    public GameObject botao11;

    public GameObject ssAngryBirds;
    public GameObject ssMegaManX;

    public GameObject referencia;

    public GameObject[] botoes;
    public char[] letras = new char[21];

	// Use this for initialization
	void Start () 
    {
        botoes = new GameObject[21];

        for (int i = 0; i < botoes.Length; i++)
        {
            GameObject go = Instantiate(botao11, new Vector3(posisaoLetraX, posisaoLetraY, 0), Quaternion.identity) as GameObject;
            go.transform.parent = referencia.transform;
            go.transform.localScale = new Vector3(tamanhoLetraX, tamanhoLetraYZ, tamanhoLetraYZ);

            botoes[i] = go;
        }

       

        //Instantiate(ssAngryBirds, new Vector3(-0.5F, 140F, 0), Quaternion.identity);

        //Instantiate(ssMegaManX, new Vector3(-0.5F, 140F, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < botoes.Length; i++)
        {
            if (botoes[i] != null)
            {
                /*if (i >= 0 && i < 7)
                {
                    botoes[i].GetComponentInChildren<UILabel>().text = "l";
                    botoes[i].GetComponent<UIAnchor>().relativeOffset.x = 0.135f * (i + 1);
                    botoes[i].GetComponent<UIAnchor>().relativeOffset.y = -0.175f;
                }
                else if (i >= 7 && i < 14)
                {
                    botoes[i].GetComponentInChildren<UILabel>().text = "6";
                    botoes[i].GetComponent<UIAnchor>().relativeOffset.x = 0.135f * (i + 1 - 7);
                    botoes[i].GetComponent<UIAnchor>().relativeOffset.y = -0.245f;
                }
                /*else 
                {
                    botoes[i].GetComponentInChildren<UILabel>().text = "!";
                    botoes[i].GetComponent<UIAnchor>().relativeOffset.x = 0.135f * (i + 1 - 14);
                    botoes[i].GetComponent<UIAnchor>().relativeOffset.y = -0.315f;
                }*/

            }
        }
	}
}
