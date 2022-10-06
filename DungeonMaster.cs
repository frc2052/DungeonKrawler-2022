using System;
using System.Collections.Generic;

namespace DungeonKrawler
{
	class DungeonMaster
	{
		private const string Prompt = " -> ";
		private const string WordDelimiter = " ";


		private Dungeon _dungeon = null;
		private bool _initialized = false;
		private Player _player = null;


		private string GetInput()
		{
			string inputOptions = GetInputOptions();

//			Console.Write( inputOptions );
			Console.Write( Prompt );
			string ? inputString = Console.ReadLine();
			while ( string.IsNullOrWhiteSpace( inputString ) )
			{
				Console.WriteLine( "Empty input.  Please enter a command or enter 'h' for help." );
				Console.Write( inputOptions );
				Console.Write( Prompt );

				inputString = Console.ReadLine();
			}

			return inputString;
		}


		public string GetInputOptions()
		{
			return "( n, s, e, w, h, l, x )";
		}


		public bool Initialize( Dungeon dungeon,
				Player player )
		{
			_initialized = false;

			if ( dungeon == null )
			{
				Console.WriteLine( "Unable to initialize DungeonMaster with a Dungeon" );
				return false;
			}
			if ( player == null )
			{
				Console.WriteLine( "Unable to initialize DungeonMaster with a Player" );
				return false;
			}

			_dungeon = dungeon;
			_player = player;

			_dungeon.SetPlayer( player );

			_initialized = true;

			return true;
		}

		private string [] ParseInput( string rawInput )
		{
			if ( string.IsNullOrWhiteSpace( rawInput ) )
			{
				return null;
			}

			string[] splitString = rawInput.Split( WordDelimiter );

			return splitString;
		}


		public void PrintHelp()
		{
			Console.WriteLine( "Enter a command letter and press enter/return.");
			Console.WriteLine( "Available command letters are:");
			Console.WriteLine( "  'n' to move north" );
			Console.WriteLine( "  's' to move south" );
			Console.WriteLine( "  'e' to move east" );
			Console.WriteLine( "  'w' to move west" );
			Console.WriteLine( "  'l' to look at your current surroundings.  Follow 'l' by a space and direction ('n', 's', 'e', 'w')" );
			Console.WriteLine( "      to look in a direction (e.g. 'l w' to look west)." );
			Console.WriteLine( "  'x' to exit" );
		}


		public void Start()
		{
			if ( !_initialized )
			{
				Console.WriteLine( "DungeonMaster is not initialized." );
			}

			PrintHelp();

			string intro = _dungeon.GetIntroduction();
			if ( !string.IsNullOrWhiteSpace( intro ) )
			{
				Console.WriteLine( intro );
			}

			bool exit = false;
			bool displayRoomDescription = true;
			while ( !exit )
			{
				if ( displayRoomDescription )
				{
					string roomDescription = _dungeon.GetCurrentRoomDescription();
					if ( !string.IsNullOrWhiteSpace( roomDescription ) )
					{
						Console.WriteLine( roomDescription );
					}
				}

				string rawInput = string.Empty;
				string [] parsedInput = null;
				bool hasInput = false;
				while ( !hasInput )
				{
					rawInput = GetInput();
					if ( !string.IsNullOrWhiteSpace( rawInput ) )
					{
						parsedInput = ParseInput( rawInput );
						if ( ( parsedInput != null ) && ( parsedInput.Length > 0 ) && ( parsedInput.Length <= 2 ) )
						{
							hasInput = true;
						}
					}
				}

				( string result, bool didMove ) commandResult = ( string.Empty, false );
				switch ( parsedInput[ 0 ] )
				{
					case "e":
						commandResult = _dungeon.Go( DirectionEnum.East );
						break;

					case "h":
						PrintHelp();
						break;

					case "l":
						if ( parsedInput.Length > 1 )
						{
							commandResult.didMove = false;
							switch ( parsedInput[ 1 ] )
							{
								case "e":
									commandResult.result = _dungeon.Look( DirectionEnum.East );
									break;

								case "n":
									commandResult.result = _dungeon.Look( DirectionEnum.North );
									break;

								case "s":
									commandResult.result = _dungeon.Look( DirectionEnum.South );
									break;

								case "w":
									commandResult.result = _dungeon.Look( DirectionEnum.West );
									break;

								default:
									Console.WriteLine( "Invalid look direction.  Enter 'h' for help." );
									break;
							}
						}
						else
						{
							// The user just wants to look around where they are, so we'll fake a move
							commandResult.didMove = true;
						}
						break;

					case "n":
						commandResult = _dungeon.Go( DirectionEnum.North );
						break;

					case "s":
						commandResult = _dungeon.Go( DirectionEnum.South );
						break;

					case "w":
						commandResult = _dungeon.Go( DirectionEnum.West );
						break;

					case "x":
						commandResult.result = "Thanks for playing!";
						exit = true;
						break;

					default:
						Console.WriteLine( "Invalid input.  Enter 'h' for help." );
						break;
				}

				if ( !string.IsNullOrWhiteSpace( commandResult.result ) )
				{
					Console.WriteLine( commandResult.result );
					commandResult.result = string.Empty;
				}
				displayRoomDescription = commandResult.didMove;

				if ( _player.IsDead() )
				{
					exit = true;
				}
				if ( _player.DidWin() )
				{
					exit = true;
				}
			}
		}
	}
}