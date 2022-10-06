using System;

namespace DungeonKrawler
{
	class Program
	{
		static void Main( string [] args )
		{
			DungeonMaster dm = new DungeonMaster();
			Dungeon dungeon = new Dungeon();
			Player player = new Player();

			dm.Initialize( dungeon, player );
			dm.Start();
		}
	}
}
