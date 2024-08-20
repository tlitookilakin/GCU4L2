namespace MovieDB
{
	public class Movie
	{
		public string Name { get; set; }
		public string Category { get; set; }
		public int Year { get; set; }
		public int Duration { get; set; }

		public Movie(string name, string category, int year, int duration)
		{
			Name = name;
			Category = category;
			Duration = duration;
			Year = year;
		}
	}
}
