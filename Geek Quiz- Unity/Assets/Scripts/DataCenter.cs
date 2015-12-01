using UnityEngine;
using System.Collections;

public class DataCenter : MonoBehaviour 
{
	//static public Jogo[] jogos;
	static public Level [] levels;
	static public int[] numeroJogosLevel;
	static public int numeroLevels;

	static public AssetBundle imagensBundle;

	static public Idiomas idioma;

	public enum Idiomas {pt, en}

	static public int hints;


	// Use this for initialization
	void Start () 
	{
		idioma = Idiomas.en;
		LerXML ();
		StartCoroutine (LoadAssetBundle());
	}



	//ler arquivo XML localmente e retornar o array de levels com um array de jogos em cada level
	void LerXML()
	{
		numeroLevels = 0;
		
		TextAsset XML = Resources.Load("ListaDeLevels") as TextAsset;
		
		TinyXmlReader reader = new TinyXmlReader(XML.text);
		
		while (reader.Read())
		{
			if(reader.tagName.Contains("level") && reader.isOpeningTag)
			{
				numeroLevels++;
			}
		}
		
		Debug.Log (numeroLevels);
		levels = new Level[numeroLevels];
		numeroJogosLevel = new int[numeroLevels];
		
		reader = new TinyXmlReader(XML.text);
		
		int numeroLevel = 0;
		
		while (reader.Read ()) 
		{
			
			if(reader.tagName.Contains("level") && reader.isOpeningTag)
			{
				string tagName = reader.tagName;
				Level level = new Level();
				bool firstName = true;
				int numeroJogos = 0;
				
				while(reader.Read(tagName))
				{
					
					if(reader.tagName == "nome" && reader.isOpeningTag)
					{
						if(firstName)
						{
							level.nome = reader.content;
							firstName = false;
						}
					}
					
					if(reader.tagName == "jogo" && reader.isOpeningTag)
					{
						numeroJogos++;
					}
				}
				
				Debug.Log ("Numero jogos  level " + numeroLevel + ": " + numeroJogos);
				numeroJogosLevel[numeroLevel] = numeroJogos; 
				levels[numeroLevel] = level;
				numeroLevel++;
			}
		}
		
		reader = new TinyXmlReader(XML.text);
		
		numeroLevel = 0;
		while (reader.Read ()) 
		{
			
			
			if(reader.tagName.Contains("level") && reader.isOpeningTag)
			{
				string tagName = reader.tagName;
				levels[numeroLevel].StartJogos(numeroJogosLevel[numeroLevel]);
				int numeroJogos = 0;
				
				while(reader.Read (tagName))
				{
					if(reader.tagName == "jogo" && reader.isOpeningTag)
					{
						Jogo jogo = new Jogo();
						
						while(reader.Read ("jogo"))
						{
							if(reader.tagName == "nome") jogo.nome = reader.content;
							if(reader.tagName == "plataforma") jogo.plataforma = reader.content;
							if(reader.tagName == "genero") jogo.genero = reader.content;
							if(reader.tagName == "ano") jogo.ano = reader.content;
							if(reader.tagName == "dificuldade") jogo.dificuldade = int.Parse(reader.content);
							if(reader.tagName == "resposta") jogo.resposta = reader.content;
							if(reader.tagName == "dica1") jogo.dica1 = reader.content;
							if(reader.tagName == "dica2") jogo.dica2 = reader.content;
							if(reader.tagName == "linkimagem") jogo.linkImagem = reader.content;
							
							//Debug.Log(reader.tagName + " | " + reader.content);
						}
						
						levels[numeroLevel].jogos[numeroJogos] = jogo;
						numeroJogos++;
					}
				}
				
				numeroLevel++;
			}
		}
	}




	//carregar asset bundle de imagens localmente
	IEnumerator LoadAssetBundle()
	{
		#if UNITY_EDITOR
		
		string filePath = "file://" + Application.dataPath + "/StreamingAssets/Texturas2.unity3d";
		
		#elif UNITY_ANDROID
		
		string filePath = "jar:file://" + Application.dataPath + "!/assets/Texturas2.unity3d";

        #elif UNITY_IOS

		string filePath = Application.dataPath + "/Raw/Texturas2.unity3d";;
		
#endif

        WWW dwld = new WWW (filePath);
		yield return dwld;
		imagensBundle = dwld.assetBundle;
	}


}
