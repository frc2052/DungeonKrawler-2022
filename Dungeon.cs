using System.Collections.Generic;

namespace DungeonKrawler
{
	//
	// Dungeon Map
	//

	enum DirectionEnum
	{
		East,
		North,
		South,
		West
	}


	class Dungeon
	{
		private class Room
		{
			public int Index = -1;
			public string Description = string.Empty;
			public string LookDescription = string.Empty;
			public bool TakesDamage = false;
			public bool Wins = false;

			public int EastRoom = -1;
			public int NorthRoom = -1;
			public int SouthRoom = -1;
			public int WestRoom = -1;


			public Room( int index,
					string description,
					string lookDescription,
					bool takesDamage,
					bool wins,
					int northRoom,
					int southRoom,
					int eastRoom,
					int westRoom )
			{
				Description = string.IsNullOrWhiteSpace( description ) ? string.Empty : description;
				EastRoom = eastRoom;
				Index = index;
				LookDescription = string.IsNullOrWhiteSpace( lookDescription ) ? string.Empty : lookDescription;
				NorthRoom = northRoom;
				SouthRoom = southRoom;
				TakesDamage = takesDamage;
				WestRoom = westRoom;
				Wins = wins;
			}
		}


		private List< Room > _rooms = new List< Room >()
		{
			new Room( 0, "Room 0", "Room 0 Look",
					false, false, 1, -1, -1, -1 ),
			new Room( 1, "Room 1", "Room 1 Look",
					false, false, 3, 0, 2, 4 ),
			new Room( 2, "Room 2", "Room 2 Look",
					false, false, -1, -1, -1, 1 ),
			new Room( 3, "Room 3", "Room 3 Look",
					false, false, -1, 1, -1, -1 ),
			new Room( 4, "Room 4", "Room 4 Look",
					false, false, 5, -1, 1, -1 ),
			new Room( 5, "Room 5", "Room 5 Look",
					false, false, 6, 4, -1, 7 ),
			new Room( 6, "Room 6", "Room 6 Look",
					false, false, -1, 5, -1, -1 ),
			new Room( 7, "Room 7", "Room 7 Look",
					false, false, -1, -1, 5, -1 ),
		};


		private int _currentRoom = 0;
		private Player _player = null;


		public Dungeon()
		{

		}

		public string GetIntroduction()
		{
			return "Introduction";
		}


		public string GetCurrentRoomDescription()
		{
			Room room = GetRoom( _currentRoom );
			return room.Description;
		}


		private Room GetRoom( int index )
		{
			foreach ( Room which in _rooms )
			{
				if ( which.Index == index )
				{
					return which;
				}
			}

			return null;
		}


		public ( string result, bool didMove ) Go( DirectionEnum direction )
		{
			Room room = GetRoom( _currentRoom );
			if ( room == null )
			{
				return ( $"Error access room index [{_currentRoom}]: Not found", false );
			}

			string result = string.Empty;
			bool didMove = false;
			switch ( direction )
			{
				case DirectionEnum.East:
					if ( room.EastRoom >= 0 )
					{
						_currentRoom = room.EastRoom;
						result = "You walk east.";
						didMove = true;
					}
					else
					{
						result = "You bump you nose on the wall.  'East' is not a valid direction.";
					}
					break;

				case DirectionEnum.North:
					if ( room.NorthRoom >= 0 )
					{
						_currentRoom = room.NorthRoom;
						result = "You walk north.";
						didMove = true;
					}
					else
					{
						result = "You bump you nose on the wall.  'North' is not a valid direction.";
					}
					break;

				case DirectionEnum.South:
					if ( room.SouthRoom >= 0 )
					{
						_currentRoom = room.SouthRoom;
						result = "You walk south.";
						didMove = true;
					}
					else
					{
						result = "You bump you nose on the wall.  'South' is not a valid direction.";
					}
					break;

				case DirectionEnum.West:
					if ( room.WestRoom >= 0 )
					{
						_currentRoom = room.WestRoom;
						result = "You walk west.";
						didMove = true;
					}
					else
					{
						result = "You bump you nose on the wall.  'West' is not a valid direction.";
					}
					break;
			}

			return ( result, didMove );
		}


		public string Look( DirectionEnum direction )
		{
			Room room = GetRoom( _currentRoom );
			if ( room == null )
			{
				return $"Error access room index [{_currentRoom}]: Not found";
			}

			string resultString = string.Empty;
			Room lookAtRoom = null;
			switch ( direction )
			{
				case DirectionEnum.East:
					resultString = "You look east.\n";
					if ( room.EastRoom >= 0 )
					{
						lookAtRoom = _rooms[ room.EastRoom ];
						resultString += lookAtRoom.LookDescription;
					}
					else
					{
						resultString += "You see a wall.";
					}
					break;

				case DirectionEnum.North:
					resultString = "You look north.\n";
					if ( room.NorthRoom >= 0 )
					{
						lookAtRoom = _rooms[ room.NorthRoom ];
						resultString += lookAtRoom.LookDescription;
					}
					else
					{
						resultString += "You see a wall.";
					}
					break;

				case DirectionEnum.South:
					resultString = "You look south.\n ";
					if ( room.SouthRoom >= 0 )
					{
						lookAtRoom = _rooms[ room.SouthRoom ];
						resultString += lookAtRoom.LookDescription;
					}
					else
					{
						resultString += "You see a wall.";
					}
					break;

				case DirectionEnum.West:
					resultString = "You look west.\n";
					if ( room.WestRoom >= 0 )
					{
						lookAtRoom = _rooms[ room.WestRoom ];
						resultString += lookAtRoom.LookDescription;
					}
					else
					{
						resultString += "You see a wall.";
					}
					break;
			}

			return resultString;
		}


		public void SetPlayer(Player player)
		{
			_player = player;
		}
	}
}
