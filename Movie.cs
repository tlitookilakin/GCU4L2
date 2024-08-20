namespace MovieDB
{
	public class Movie
	{
		public string Name { get; set; }
		public string Category { get; set; }

		public Movie(string name, string category)
		{
			Name = name;
			Category = category;
		}
	}
}
