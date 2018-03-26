using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
	private static int player;
	private static int count;//落子数目
	private int winner;//
	private int[,] chessBoard = new int[3, 3];//3x3棋

	void Start () {
		Restart();
	}

	void OnGUI()
	{
		GUI.Box(new Rect(475, 25, 200, 300), "");
		if (GUI.Button(new Rect(520, 250, 100, 50), "Restart")) 
			Restart();
		if (!GameOver())
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (chessBoard[i, j] == 0 && GUI.Button(new Rect(500 + j * 50, 50 + i * 50, 50, 50), ""))
					{
						chessBoard[i, j] = player;
						player = 3 - player;
						count++;
					}
					else if (chessBoard[i, j] == 1) GUI.Button(new Rect(500 + j * 50, 50 + i * 50, 50, 50), "O");
					else if (chessBoard[i, j] == 2) GUI.Button(new Rect(500 + j * 50, 50 + i * 50, 50, 50), "X");
				}
			}
		}
	}

	void Restart()
	{
		player = 1;//玩家1先下
		winner = 0;
		count = 0;
		for(int i = 0; i < 3; i++)//清空棋
			for(int j = 0; j < 3; j++)
				chessBoard[i, j] = 0;
	}

	bool GameOver() {
		for(int i = 0; i < 3; i++) {
			//列
			if (chessBoard[0, i] != 0 
				&& chessBoard[0, i] == chessBoard[1, i] && chessBoard[0, i] == chessBoard[2, i]) 
				winner = chessBoard[0, i];
			//行
			if (chessBoard[i, 0] != 0 
				&& chessBoard[i, 0] == chessBoard[i, 1] && chessBoard[i, 0] == chessBoard[i, 2]) 
				winner = chessBoard[i, 0];
		}
		//角
		if (chessBoard[0, 0] != 0 && chessBoard[0, 0] == chessBoard[1, 1] && chessBoard[0, 0] == chessBoard[2, 2])
			winner = chessBoard[0, 0];
		if (chessBoard[0, 2] != 0 && chessBoard[0, 2] == chessBoard[1, 1] && chessBoard[0, 2] == chessBoard[2, 0])
			winner = chessBoard[0, 2];

		if (count < 9 && winner == 0) return false;

		//胜者
		if (winner != 0)
			GUI.Box(new Rect(485, 35, 180, 200), "\n\n\nCongratulations!\n Player "+winner+" had won.\nPress Restart to start again.");
		else//平局
			GUI.Box(new Rect(485, 35, 180, 200), "\n\n\nA draw.\nPress Restart to start again.");

		return true;
	}

}