using System;

namespace nrunrl
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			Console.CursorVisible = false;
			//Draw bounds
			/*
			Screen.fillRect (0, 80, 0, 24, '.');
			Screen.fillRect (0, 80, 0, 1, '-');
			Screen.fillRect (0, 80, 23, 24, '-');
			Screen.fillRect (0, 1, 0, 24, '|');
			Screen.fillRect (79, 80, 0, 24, '|');
			*/
			Game.map = new char[80, 24];
			for (int x = 0; x < 80; x++) {
				for (int y = 0; y < 24; y++) {
					Game.map [x, y] = '.';
				}
			}

			for (int i = 0; i < 80; i++) {
				Game.map [i, 0] = '#';
				Game.map [i, 23] = '#';
			}
			
			for (int i = 0; i < 24; i++) {
				Game.map [0, i] = '#';
				Game.map [79, i] = '#';
			}

			Screen.printOut (0, 0, Game.map);

			Player.moveTo (40, 10);

			ConsoleKeyInfo input;
			do {
				input = Console.ReadKey (true);
				Controls.processInput(input.KeyChar);
			} while (input.Key != ConsoleKey.X && ((input.Modifiers & ConsoleModifiers.Control) != ConsoleModifiers.Control));

			Console.SetCursorPosition (0, 25);
		}

	}

	class Game {
		public static int width = 80;
		public static int height = 24;
		public static char[,] map;
	}

	class Player {
		static int px;
		static int py;

		public static void moveBy (int x, int y){
			Screen.draw (Player.px, Player.py, Game.map [Player.px, Player.py]);
			if (Player.px + x > 0 && Player.px + x < Game.width) {
				Player.px += x;
			}
			if (Player.py + y > 0 && Player.py + y < Game.height) {
				Player.py += y;
			}
			Screen.draw (Player.px, Player.py, '@');
		}

		public static void moveTo (int x, int y){
			Screen.draw (Player.px, Player.py, Game.map [Player.px, Player.py]);
			Player.px = x;
			Player.py = y;
			Screen.draw (Player.px, Player.py, '@');
		}
	}

	class Screen {
		public static void fillRect (int x0, int x1, int y0, int y1, char fill) {
			for (int x = x0; x < x1; x++) {
				for (int y = y0; y < y1; y++) {
					Console.SetCursorPosition (x, y);
					Console.Write (fill);
				}
			}
		}

		public static void draw (int x, int y, char fill) {
			Console.SetCursorPosition (x, y);
			Console.Write (fill);
		}

		public static void printOut (int x0, int y0, char[,] fill) {
			for (int x = 0; x < fill.GetLength(0); x++) {
				for (int y = 0; y < fill.GetLength(1); y++) {
					Console.SetCursorPosition (x0 + x, y0 + y);
					Console.Write (fill[x,y]);
				}
			}
		}

		public static void message ( string message) {
			Console.SetCursorPosition (0, 24);
			Console.Write (message);
		}

		public static void clearMessages ( ) {
			Screen.fillRect (0, 80, 24, 26, ' ');
		}

		public static void redrawScreen () {
			Screen.printOut (0, 0, Game.map);
			Screen.draw (Player.px, Player.py, '@');
		}
	}

	class Controls {
		public static void processInput (char input) {
			Screen.clearMessages();
			switch (input) {
			case 'y':
				Player.moveBy (-1, -1);
				break;
			case 'u':
				Player.moveBy (1, -1);
				break;
			case 'h':
				Player.moveBy (-1, 0);
				break;
			case 'j':
				Player.moveBy (0, 1);
				break;
			case 'k':
				Player.moveBy (0, -1);
				break;
			case 'l':
				Player.moveBy (1, 0);
				break;
			case 'b':
				Player.moveBy (-1, 1);
				break;
			case 'n':
				Player.moveBy (1, 1);
				break;
			case 'i':
				Screen.message ("No inventory yet, sorry!");
				break;
			case 'e':
				Screen.message ("No equipment yet, sorry!");
				break;
			}
		}
	}
}
