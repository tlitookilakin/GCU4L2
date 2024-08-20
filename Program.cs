namespace MovieDB;

class Program
{
	static readonly List<Movie> movies = 
	[
		new("Wolfwalkers", "Adventure"), new("Secret of Kells", "Adventure"), new("Everything Everywhere All at Once", "Action"),
		new("Sahara", "Action"), new("National Treasure", "Action"), new("Planet Earth", "Documentary"),
		new("Crouching Tiger, Hidden Dragon", "Kung-Fu"), new("Dead Again", "Thriller"), new("The Prestige", "Thriller"),
		new("Citizen Kane", "Classic")
	];

	static void Main(string[] args)
	{
		Console.WriteLine("Welcome to the movie database!");
		Console.WriteLine($"There are {movies.Count} movies available");
		Console.WriteLine();

		PrintMovies(movies);

		HashSet<string> genres = new(movies.Select(x => x.Category), StringComparer.OrdinalIgnoreCase);

		for (bool flag = true; flag; flag = Utility.PromptYesNo(true, "Would you like to do another search?"))
		{
			PrintMenu(genres, genres.Count);
			if (GetGenreSelection(genres) is not string Genre)
				break;

			Console.WriteLine(" ");

			var compare = StringComparer.CurrentCulture;
			List<Movie> selection = movies.Where(m => m.Category.Equals(Genre, StringComparison.OrdinalIgnoreCase)).ToList();
			selection.Sort((a, b) => compare.Compare(a.Name, b.Name));

			PrintMovies(selection);
		}

		Utility.PromptExit("Thanks for joining us!");
	}

	static void PrintMenu(IEnumerable<string> genres, int count)
	{
		if (count <= 10)
		{
			int i = 1;
			foreach (string genre in genres)
			{
				Console.WriteLine($"{i}. {genre}");
				i = (i + 1) % 10;
			}
		}
		else
		{
			foreach (string genre in genres)
				Console.WriteLine(genre);
		}

		Console.WriteLine();
	}

	static void PrintMovies(IEnumerable<Movie> selection)
	{
		string[] headers = ["Title", "Genre"];

		string format = Utility.CreateColumnsFor(
			true, headers,
			selection.Select(m => m.Name),
			selection.Select(m => m.Category)
		);

		string header = string.Format(format, headers);

		Console.WriteLine(header);
		Console.WriteLine(new string('\x2500', header.Length));

		foreach (Movie movie in selection)
			Console.WriteLine(string.Format(format, 
				movie.Name, movie.Category
			));

		Console.WriteLine();
	}

	static string? GetGenreSelection(HashSet<string> genres)
	{
		if (genres.Count <= 10)
		{
			Console.WriteLine("Please press the genre number to select");
			int index = GetNumberKey(genres.Count + 1) - 1;
			if (index < 0)
				index = 9;
			return genres.ElementAt(index);
		}

		Console.WriteLine("Please enter the genre name to select");

		string? name;
		while ((name = Console.ReadLine()?.Trim()) != null && !genres.Contains(name))
			Console.WriteLine("Not a valid genre name, please re-enter");

		return name;
	}

	static int GetNumberKey(int max)
	{
		while (true)
		{
			char key = Console.ReadKey().KeyChar;

			// deletes echoed keystroke from output
			Console.Write("\b\\\b");

			if (key is >= '0' and <= '9')
			{
				int index = key - '0';
				if (index < max)
					return index;
			}
		}
	}
}
