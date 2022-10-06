namespace DungeonKrawler
{
	public class Player
	{
		public enum StatusEnum
		{
			Alive,
			Dead,
			Won
		}

		public StatusEnum Status
		{
			get; set;
		}

		private StatusEnum _status;

		public Player()
		{
			Status = StatusEnum.Alive;
		}


		public bool IsDead()
		{
			return ( Status == StatusEnum.Dead );
		}


		public bool DidWin()
		{
			return ( Status == StatusEnum.Won );
		}
	}
}
