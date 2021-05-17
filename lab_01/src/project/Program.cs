using System;
using Microsoft.EntityFrameworkCore;

namespace db
{
	class Program
	{
		static void Main(string[] args)
		{
			// using (ApplicationContext db = new ApplicationContext())
			// {  
			// }

			IRepositoryUser db = new PostgreSQLRepositoryUser();
			foreach (var elem in db.GetUsers())
				Console.WriteLine($"{elem.Id} {elem.Name} {elem.Surname}");


		}
	}
}

// // // // // // // // // // // // // // // // 
// Всякие плюшки
// Добавление данных.
// using (ApplicationContext db = new ApplicationContext())
// {
// 	// Task task = new Task { Id = 0, Name = "None", ShortDescription = "ShortDescription", DetailedDescription = "DetailedDescription", Solution = "Solution", TableName = "TableName" };
// 	CompletedTask user = new CompletedTask { UserId = 4, TaskId = 13 };
// 	db.CompletedTasks.Add(user);

// 	db.SaveChanges();
// }

// получение данных
// using (ApplicationContext db = new ApplicationContext())
// {
// var user = db.Users.Find(4);
// Console.WriteLine($"{user.Id} {user.Name}");
// // получаем объекты из бд и выводим на консоль
// var task = db.Users.ToList();
// Console.WriteLine("Tasks list:");
// foreach (User u in task)
// {
// 	Console.WriteLine($"{u.Id}.{u.Name}");
// }
// }

// using (ApplicationContext db = new ApplicationContext())
// {
// var tmp = db.Tasks.Join(db.Users, // Tables
// 	p => p.AuthorId, // FK
// 	c => c.Id,       // PK
// 	(p, c) => new    // Result
// 	{
// 		TaskName = p.Name,
// 		UserName = c.Name
// 	});
// var tmp = (from task in db.Tasks
// 		   join user in db.Users
// 		   on task.AuthorId equals user.Id
// 		   select new
// 		   {
// 			   TaskName = task.Name,
// 			   UserName = user.Name
// 		   });

// Console.WriteLine(tmp);
// foreach (var p in tmp)
// 	Console.WriteLine($"TaskName = {p.TaskName} UserName = {p.UserName}");

// var g = from p in db.Users
// 		group p by p.Name;

// // var tmp = db.Users.GroupBy(p => p.Name);
// foreach (var p in g)
// {

// 	// 	Console.WriteLine($"{p.Key}");
// }

// int c = db.Users.Count();
// Console.WriteLine(c);
// Console.WriteLine(db.Users.Max(p => p.Id));
// Console.WriteLine(db.Users.Min(p => p.Id));
// Console.WriteLine(db.Users.Where(p => p.Name == "Annu").Sum(p => p.Id));

// var user = db.Users.FromSqlRaw("SELECT * FROM \"Users\" WHERE \"Id\" > {0}", 5);
// foreach (var u in user)
// 	Console.WriteLine($"{u.Id} {u.Name}");

// }

// IRepositoryUser db = new PostgreSQLRepositoryUser();

// db.Add(new User { Name = "Annu" });
// db.Save();

// var user = db.GetUsers().OrderBy(p => p.Name).ThenByDescending(p => p.Id);

// var user = from p in db.GetUsers()
// 		   orderby p.Name descending
// 		   select p;

// foreach (User u in user)
// 	Console.WriteLine($"{u.Id} {u.Name}");


