using System.Text; 
 
namespace MovieDB; 
﻿public static class Utility
{
	/// <summary>Give the user a yes/no prompt, and wait for them to answer</summary>
	/// <param name="allowEscape">Whether escape should be treated as "no" or ignored</param>
	/// <param name="message">The message prompt to display.</param>
	/// <returns>True if the user selected yes, false if the user selected no.</returns>
	public static bool PromptYesNo(bool allowEscape, string message = "Would you like to continue?")
	{
		Console.WriteLine(message + " [Y/N]");

		while (true)
		{
			char key = Console.ReadKey().KeyChar;

			// deletes echoed keystroke from output
			Console.Write("\b\\\b");

			switch (key)
			{
				case 'y':
				case 'Y':
					Console.Clear();
					return true;

				case 'n':
				case 'N':
					return false;

				// escape key
				case '\x1b':
					if (allowEscape)
						return false;
					break;
			}
		}
	}

	/// <summary>Gets an int from console input</summary>
	/// <param name="prompt">The prompt to display when an invalid value is entered</param>
	public static int GetInt(string prompt = "Please enter a valid number", int min = int.MinValue, int max = int.MaxValue)
	{
		int result;
		while(!int.TryParse(Console.ReadLine(), out result) || result < min || result > max)
			Console.WriteLine(prompt);
		return result;
	}

	/// <summary>Gets an decimal from console input</summary>
	/// <param name="prompt">The prompt to display when an invalid value is entered</param>
	public static decimal GetDecimal(string prompt = "Please enter a valid number", decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
	{
		decimal result;
		while(!decimal.TryParse(Console.ReadLine(), out result) || result < min || result > max)
			Console.WriteLine(prompt);
		return result;
	}

	/// <summary>Gets an double from console input</summary>
	/// <param name="prompt">The prompt to display when an invalid value is entered</param>
	public static double GetDouble(string prompt = "Please enter a valid number", double min = double.NegativeInfinity, double max = double.PositiveInfinity)
	{
		double result;
		while(!double.TryParse(Console.ReadLine(), out result) || double.IsNaN(result) || result < min || result > max)
			Console.WriteLine(prompt);
		return result;
	}

	/// <summary>Creates a format string for automatically-sized columns in a table</summary>
	/// <param name="alignLeft">Whether entries should be left or right aligned</param>
	/// <param name="suffixes">Format suffixes for specific entries</param>
	/// <param name="headers">Column headers</param>
	/// <param name="columns">Column contents</param>
	public static string CreateColumnsFor(bool alignLeft, string[] suffixes, string[] headers, params IEnumerable<string>[] columns)
	{
		StringBuilder sb = new();
		int count = Math.Max(headers.Length, columns.Length);
		for (int i = 0; i < count; i++)
		{
			int width = 0;
			if (headers.Length > i)
				if (columns.Length > i)
					width = Math.Max(headers[i].Length, columns[i].Max(static s => s.Length));
				else
					width = headers[i].Length;
			else
				width = columns[i].Max(static s => s.Length);

			sb.Append('{').Append(i).Append(',');
			if (alignLeft)
				sb.Append('-');
			sb.Append(width);

			if (suffixes.Length > i && suffixes[i] is string suffix && suffix.Length > 0)
				sb.Append(':').Append(suffix);

			sb.Append("}    ");
		}
		return sb.ToString();
	}

	/// <summary>Creates a format string for automatically-sized columns in a table</summary>
	/// <param name="alignLeft">Whether entries should be left or right aligned</param>
	/// <param name="headers">Column headers</param>
	/// <param name="columns">Column contents</param>
	public static string CreateColumnsFor(bool alignLeft, string[] headers, params IEnumerable<string>[] columns)
		=> CreateColumnsFor(alignLeft, [], headers, columns);

	/// <summary>Print a message to the user and wait for a key press.</summary>
	public static void PromptExit(string message)
	{
		Console.WriteLine(" ");
		Console.WriteLine(message);
		Console.WriteLine("Press any key to exit...");
		Console.ReadKey();
	}
}
