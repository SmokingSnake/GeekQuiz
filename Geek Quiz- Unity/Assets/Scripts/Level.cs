using UnityEngine;
using System.Collections;

public class Level
{

	public string nome;
	
	public Jogo[] jogos;
	public bool[] jogosCleared;
	
	
	public void StartJogos(int length)
	{
		jogos = new Jogo[length];
		jogosCleared = new bool[length];
	}
}
