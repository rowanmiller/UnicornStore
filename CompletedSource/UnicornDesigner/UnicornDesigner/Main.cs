using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;
using Microsoft.Data.Entity;

namespace UnicornDesigner
{
	class MainClass
	{
		static void Main (string[] args)
		{
			using (var db = new WorkContext ()) {
				db.Database.EnsureDeleted ();
				db.Database.EnsureCreated ();

				db.WorkOrders.Add (new WorkOrder {
					Title = "ASCII Art Unicorn",
					Description = "Need an ASCII art unicorn for developer promotion. Approx 12 characters high",
					IsCompleted = false,
					RequestedBy = "Jeff Citizen",
					AssignedTo = "Rowan Miller"
				});

				db.WorkOrders.Add (new WorkOrder {
					Title = "Banner for Unicorn Watch",
					Description = "We have a new watch arriving shortly and need a vanity shot and wording for a website banner",
					IsCompleted = false,
					RequestedBy = "Jeff Citizen",
					AssignedTo = "Rowan Miller"
				});

				db.SaveChanges ();

				db.Notes.Add (new Note {
					WorkOrderId = 1,
					Added = DateTime.Today,
					Author = "Rowan Miller",
					Content = "Discussed with Amy and decided on max 100 char width"
				});

				db.SaveChanges ();
			}

			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}








}

