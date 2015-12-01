using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour 
{
    public GameObject[] botoes;

    public GameObject imagemJogo;

    public GameData gameData;


    public GameObject labelResposta;
    public GameObject labelHint;

    public GameObject helpButton;
    public GameObject skipButton;
    public GameObject hintButtonNormal;
    public GameObject hintButtonStrong;
    public GameObject helpBackground;
    public GameObject eraseButton;
    public GameObject facebookButton;
    public GameObject getMoreButton;
    public GameObject hintBackground;

    
    public bool openMenuHelp = false;
    public bool openHints = false;
    //public int lastHint;

    void Start()
    {
        StartCoroutine(Inicializar());   
    }

    void Update()
    {
        if (gameData != null)
        {
            labelResposta.GetComponent<UILabel>().text = gameData.Resposta;

            if (gameData.NumeroLetraAtual == gameData.Resposta.Length)
            {
                if (gameData.ChecarVitoria())
                {
                    Reinicializar();
                }

                ReligarBotao();
            }

        }


    }
    //Método temporário.
    IEnumerator Inicializar()
    {
        yield return new WaitForSeconds(2);
        gameData = new GameData();
        //gameData = new GameData(DataCenter.levels[11].jogos[4]);
       
        //yield return new WaitForSeconds(2);
        imagemJogo.GetComponent<UITexture>().mainTexture = gameData.ImagemDoJogo();

        foreach (GameObject b in botoes)
        {
            b.GetComponentInChildren<UILabel>().text = "";
        }

        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].GetComponentInChildren<UILabel>().text = gameData.LetrasBotoes[i].ToString();
            //botoes[i].GetComponentInChildren<UILabel>().ProcessText();
            //print("Texto label: "+ i+" "+botoes[i].GetComponentInChildren<UILabel>().text);
            botoes[i].GetComponent<ButtonPress>().indice = i;
            //yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].GetComponentInChildren<UILabel>().text = gameData.LetrasBotoes[i].ToString();
            //print("Texto label: " + i + " " + botoes[i].GetComponentInChildren<UILabel>().text);
            botoes[i].GetComponent<ButtonPress>().indice = i;
            //yield return new WaitForSeconds(0.1f);
        }
        print(imagemJogo.GetComponent<UITexture>().mainTexture.ToString());

    }

    public void ReligarBotao(int i)
    {
        botoes[i].SetActive(true);
    }

    public void ReligarBotao()
    {
        foreach (GameObject b in botoes)
        {
            b.SetActive(true);
        }
    }
    public void DesligarBotao()
    {
        foreach (GameObject b in botoes)
        {
            b.SetActive(false);
        }
    }

    public void Reinicializar()
    {
        gameData = new GameData();
        //gameData = new GameData(DataCenter.levels[0].jogos[10]);
        print(gameData.JogoAtual);
        print("Dica 1 " + gameData.Dica(1));
        //yield return new WaitForSeconds(2);

        ReligarBotao();
        imagemJogo.GetComponent<UITexture>().mainTexture = gameData.ImagemDoJogo();

        foreach (GameObject b in botoes)
        {
            b.GetComponentInChildren<UILabel>().text = "";
        }

        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].GetComponentInChildren<UILabel>().text = gameData.LetrasBotoes[i].ToString();
            //botoes[i].GetComponentInChildren<UILabel>().ProcessText();
            //print("Texto label: " + i + " " + botoes[i].GetComponentInChildren<UILabel>().text);
            botoes[i].GetComponent<ButtonPress>().indice = i;
           //yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].GetComponentInChildren<UILabel>().text = gameData.LetrasBotoes[i].ToString();
            //print("Texto label: " + i + " " + botoes[i].GetComponentInChildren<UILabel>().text);
            botoes[i].GetComponent<ButtonPress>().indice = i;
            //yield return new WaitForSeconds(0.1f);
        }
       

        
    }

    

}

