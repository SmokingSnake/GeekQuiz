using UnityEngine;
using System.Collections;

public class GameData 
{
    private Jogo jogoAtual;

    //resposta que o jogo vai aceitar
    private string resposta;

    //array de letras que vao aparecer nos botoes
    private char[] letrasBotoes;

    //numero da letra que o jogador vai preencher ao apertar o prox botao
    private int numeroLetraAtual;

    //array de letras que vai ser mostrado na barra de resposta
    private char[] respostaArray;

    //array dos botoes que ja foram chutados (true = botao desligado) (false = botao ligado)
    private bool[] botoesChutados;

    //construtores
    public GameData(Jogo jogo)
    {
        jogoAtual = jogo;
        Inicializar();
    }

    public GameData()
    {
        EscolherJogoAleatorio();
        Inicializar();
    }

    //propriedades
    public string Resposta
    {
        get { return new string(respostaArray); }

    }

    public int NumeroLetraAtual
    {
        get { return numeroLetraAtual; }

    }

    public char[] LetrasBotoes
    {
        get { return letrasBotoes; }
    }

    public string JogoAtual
    {
        get { return jogoAtual.nome; }
    }



    //métodos
    void EscolherJogoAleatorio()
    {
        int numeroLevel = Random.Range(0, DataCenter.levels.Length);
        Level lvl = DataCenter.levels[numeroLevel];

        int numeroJogo = Random.Range(0, lvl.jogos.Length);

        jogoAtual = lvl.jogos[numeroJogo];
        //imagemJogoAtual = Resources.Load ("Images/" + jogoAtual.linkImagem) as Texture2D;
    }

    public Texture2D ImagemDoJogo()
    {
        //Debug.Log("Contem imagem : " + DataCenter.imagensBundle.Contains(jogoAtual.linkImagem));
        return DataCenter.imagensBundle.Load(jogoAtual.linkImagem) as Texture2D;
    }


    void Inicializar()
    {
        resposta = jogoAtual.resposta.ToUpper();
        ResetarRespostaArray();

        Debug.Log("Jogo : " + jogoAtual.nome);

        letrasBotoes = resposta.Replace(" ", "").ToCharArray();


        //preenche o array letrasBotoes 
        while (letrasBotoes.Length < 21)
        {
            char[] letrasProv2 = new char[letrasBotoes.Length + 1];

            for (int i = 0; i < letrasBotoes.Length; i++)
            {
                letrasProv2[i] = letrasBotoes[i];
            }

            letrasProv2[letrasBotoes.Length] = EscolherLetraAleatoria();

            letrasBotoes = letrasProv2;
        }

        //randomiza o array letrasBotoes
        for (int i = 0; i < letrasBotoes.Length; i++)
        {
            int a = Random.Range(i, letrasBotoes.Length);

            if (a != i)
            {
                char prov = letrasBotoes[i]; 
                letrasBotoes[i] = letrasBotoes[a]; 
                letrasBotoes[a] = prov;
            }
        }
    }

    char EscolherLetraAleatoria()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456";
        int rnd = Random.Range(0, chars.Length);
        return chars[rnd];
    }

    void ResetarRespostaArray()
    {
        respostaArray = resposta.ToCharArray();
        Debug.Log(respostaArray.Length);

        for (int i = 0; i < respostaArray.Length; i++)
        {
            if (respostaArray[i] != ' ')
            {
                respostaArray[i] = '_';
            }
            else
            {
                respostaArray[i] = ' ';
            }
        }

        numeroLetraAtual = 0;
        botoesChutados = new bool[21];
    }

    public void AdicionarResposta(char c, int i)
    {
        botoesChutados[i] = true;

        if (respostaArray[numeroLetraAtual] != ' ')
        {
            respostaArray[numeroLetraAtual] = c;
        }
        else
        {
            numeroLetraAtual++;
            respostaArray[numeroLetraAtual] = c;

        }

        numeroLetraAtual++;

        //if (numeroLetraAtual == respostaArray.Length)
        //{
        //    if (ChecarVitoria())
        //    {
                
        //        Debug.Log("Venceu");
        //    }
        //    else
        //    {
        //        //derrota
        //        ResetarRespostaArray();
        //        GameObject.Find("GameInitializer").GetComponent<GameInit>().ReligarBotao();
        //        Debug.Log("Errou");
        //    }
        //}

    }

    public bool ChecarVitoria()
    {
        if (new string(respostaArray) == resposta)
        {
            return true;
        }
        else
        {
            ResetarRespostaArray();
            return false;

        }
    }

    public void DeletarUltimaLetra()
    {
        if (numeroLetraAtual > 0)
        {
            char letra = ' ';

            if (respostaArray[numeroLetraAtual - 1] == ' ')
            {
                numeroLetraAtual -= 2;
            }
            else
            {
                numeroLetraAtual--;
            }


            letra = respostaArray[numeroLetraAtual];
            respostaArray[numeroLetraAtual] = '_';

            for (int i = 0; i < letrasBotoes.Length; i++)
            {
                if (letrasBotoes[i] == letra && botoesChutados[i])
                {
                    //religar botao de numero i
                    GameObject.Find("GameInitializer").GetComponent<GameInit>().ReligarBotao(i);
                    botoesChutados[i] = false;
                    break;
                }
            }
        }
    }

    public string Dica(int i)
    {
        if (i == 1)
        {
            if (DataCenter.idioma == DataCenter.Idiomas.en)
            {
                return jogoAtual.dica1;
            }

            else if (DataCenter.idioma == DataCenter.Idiomas.pt)
            {
                return jogoAtual.dica1br;
            }
            else
            {
                return null;
            }
        }

        else if (i == 2)
        {
            if (DataCenter.idioma == DataCenter.Idiomas.en)
            {
                return jogoAtual.dica2;
            }

            else if (DataCenter.idioma == DataCenter.Idiomas.pt)
            {
                return jogoAtual.dica2br;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void Skip()
    {
        EscolherJogoAleatorio();
        Inicializar();
        //gi.inicializar;
    }


}
