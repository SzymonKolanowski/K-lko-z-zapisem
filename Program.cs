using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CircleandCross2
{
	class Program
	{

		// program ma zapisywać wszystko co jest na konsoli także do pliku. Zastąpić wszystkie console.Writeline i console.write odpowiednimi metodami.

		static void WritetoConsolAndTxt(string text)
		{


			FileInfo fi = new FileInfo("D:\\Pliki tekstowe z konsoli\\Kółko i krzyżyk.txt");

			using (StreamWriter sw = fi.AppendText())
			{				
				Console.WriteLine(text);
				sw.WriteLine(text);
			}
		}

		//static void ReadToConsoleandText(string text)
		//{
		//	FileInfo fi = new FileInfo("D:\\Pliki tekstowe z konsoli\\Kółko i krzyżyk.txt");

		//	using (StreamWriter sw = fi.AppendText())
		//	{
				
		//			string text2 = Console.ReadLine();
		//			sw.WriteLine(text2);				
		//	}
		//}

		static void WriteArraytoConsoleandText(string[,] board)
		{
			FileInfo fi = new FileInfo("D:\\Pliki tekstowe z konsoli\\Kółko i krzyżyk.txt");

			using (StreamWriter sw = fi.AppendText())
			{
				for (int i = 0; i < board.GetLength(0); i++)
				{
					for (int j = 0; j < board.GetLength(1); j++)
					{
						string arr = board[i, j];
						Console.Write(arr);
						sw.Write(arr);

					}
					Console.WriteLine();
					sw.WriteLine();
				}
			}
		}


		//static void ShowGameBoard(string[,] board)
		//{



		//		for (int i = 0; i < board.GetLength(0); i++)
		//		{
		//			for (int j = 0; j < board.GetLength(1); j++)
		//			{
		//				string arr = board[i, j];
		//				WriteArraytoConsoleandText(arr);						
		//			}
		//			Console.WriteLine();				
		//		}


		//}
		static bool CheckWinCondition(string[,] board, string PlayerToken)
		{
			for (int columns = 0; columns < 3; columns++)
			{
				if (board[columns, 0] == PlayerToken && board[columns, 1] == PlayerToken && board[columns, 2] == PlayerToken)

				{
					return true;
				}
			}

			for (int rows = 0; rows < 3; rows++)
			{
				if (board[0, rows] == PlayerToken && board[1, rows] == PlayerToken && board[2, rows] == PlayerToken)

				{
					return true;
				}

			}
			if (board[0, 0] == PlayerToken && board[1, 1] == PlayerToken && board[2, 2] == PlayerToken
				|| board[0, 2] == PlayerToken && board[1, 1] == PlayerToken && board[2, 0] == PlayerToken)
			{
				return true;
			}
			return false;
		}

		static Coordinates ReturnCordinate(int ArraySize, string PlayerCoordinate)
		{
			WritetoConsolAndTxt("podaj pozycję " + PlayerCoordinate + " współrzędnej");

			FileInfo fi = new FileInfo("D:\\Pliki tekstowe z konsoli\\Kółko i krzyżyk.txt");

			using (StreamWriter sw = fi.AppendText())
			{

				string text = Console.ReadLine();
				sw.WriteLine(text);

				int cordinate;
				bool isCordinates = int.TryParse(text, out cordinate);
				while (isCordinates != true || cordinate >= ArraySize)
				{
					Console.WriteLine("wprowadziłeś złą wartość");
					sw.WriteLine("wprowadziłeś złą wartość");

					Console.WriteLine("podaj poprawną wartość " + PlayerCoordinate);
					sw.WriteLine("podaj poprawną wartość " + PlayerCoordinate);

					text = Console.ReadLine();
					sw.WriteLine(text);



					isCordinates = int.TryParse(text, out cordinate);
				}

				Coordinates c1 = new Coordinates(cordinate);
				return c1;
			}
		}
		
		
		
		static Position ReturnPosition(string[,] array, string PlayerToken)
		{


			var cordinate = ReturnCordinate(array.GetLength(0), PlayerToken);

			var cordinate2 = ReturnCordinate(array.GetLength(1), PlayerToken);

			while (array[cordinate.PlayerCordinate, cordinate2.PlayerCordinate] != "[ ]")
			{
				WritetoConsolAndTxt("Pozycja została już wcześniej zajeta");

				cordinate = ReturnCordinate(array.GetLength(0), PlayerToken);
				cordinate2 = ReturnCordinate(array.GetLength(1), PlayerToken);
			}

			Position p1 = new Position(cordinate.PlayerCordinate, cordinate2.PlayerCordinate);
			return p1;
		

		}
		static void Main(string[] args)
		{
			FileInfo fi = new FileInfo("D:\\Pliki tekstowe z konsoli\\Kółko i krzyżyk.txt");

			using (StreamWriter sw = fi.AppendText())
			{
				Console.WriteLine("Kółko i krzyżyk");
				sw.WriteLine("Kólko i krzyżyk");
			}


				string[,] array = new string[3, 3] { { "[ ]", "[ ]", "[ ]" }, { "[ ]", "[ ]", "[ ]" }, { "[ ]", "[ ]", "[ ]" } };
			WriteArraytoConsoleandText(array);
			bool HasAnyoneWon = false;
			for (int GameRound = 1; GameRound <= array.Length; GameRound++)
			{
				var PlayerToken = GameRound % 2 == 0 ? "[x]" : "[o]";
				var position = ReturnPosition(array, PlayerToken);
				array[position.PositionRow, position.Positioncolumn] = PlayerToken;

				WriteArraytoConsoleandText(array);
				HasAnyoneWon = CheckWinCondition(array, PlayerToken);
				if (HasAnyoneWon)
				{
					WritetoConsolAndTxt(PlayerToken + " won");

					break;
				}

			}
			if (!HasAnyoneWon)
			{
				WritetoConsolAndTxt("remis");

			}

		}
	}
}

