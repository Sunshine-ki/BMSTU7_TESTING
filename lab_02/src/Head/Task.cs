using System;
using Npgsql;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Head
{
	public static class Task
	{
		public static List<ArrayList> ExecTask(string query, NpgsqlConnection con)
		{
			List<ArrayList> result = new List<ArrayList>();

			var cmd = new NpgsqlCommand(query, con);
			var reader = cmd.ExecuteReader();

			int fieldCount = reader.FieldCount;
			while (reader.Read())
			{
				ArrayList currentRow = new ArrayList();
				for (int i = 0; i < fieldCount; i++)
				{
					currentRow.Add(reader[i]);
				}
				result.Add(currentRow);
			}

			// cmd.Dispose();

			reader.Close();
			return result;
		}

		public static bool CompareRows(ArrayList rowUser, ArrayList rowTeacher)
		{
			if (rowUser.Count != rowTeacher.Count)
				return false;
			for (int i = 0; i < rowUser.Count; i++)
			{
				Console.WriteLine($"Compare = {rowUser[i]} {rowTeacher[i]} {Object.Equals(rowTeacher[i], rowUser[i])}");
				if (!Object.Equals(rowTeacher[i], rowUser[i]))
					return false;
			}

			return true;
		}

		public static bool CompareResults(List<ArrayList> userResult, List<ArrayList> teacherResult)
		{
			if (userResult is null || teacherResult is null)
			{
				return false;
			}

			Console.WriteLine($"Columns count:  {userResult[0].Count} {teacherResult[0].Count}");

			// Compare rows count
			if (userResult.Count != teacherResult.Count)
				return false;

			// Compare columns count
			if (userResult[0].Count != teacherResult[0].Count)
				return false;

			int rowCount = userResult.Count;
			for (int i = 0; i < rowCount; i++)
			{
				if (!CompareRows(userResult[i], teacherResult[i]))
					return false;
			}

			return true;
		}
	}
}