using System;
using System.Linq;
using Microsoft.Data.Entity;

using Xamarin.Forms;
using Acr.DeviceInfo;

namespace XamarinFormsTest
{
	public class MyContext : DbContext
	{
		public DbSet<Test> Tests { get; set; }

		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryStore ();
		}
	}

	public class Test
	{
		public int TestId { get; set; }
		public string Name { get; set; }
	}

	public class App : Application
	{
		public App ()
		{
			
			//using (var db = new MyContext ()) {

			//	db.Tests.Add (new Test { Name = "Rowan" });
			//	db.SaveChanges ();

				//var count = db.Tests.Count ();

			MainPage = new NavigationPage(new MenuPage());
		}
		

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

