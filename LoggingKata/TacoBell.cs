using System;
namespace LoggingKata
{
	public class TacoBell: ITrackable
	{
		public TacoBell()
		{
		}
        public Point Location { get; set; }

        public string Name { get; set; }
    }
}

