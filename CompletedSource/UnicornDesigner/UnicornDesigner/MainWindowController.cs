
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace UnicornDesigner
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		private WorkContext _db = new WorkContext();
		private WorkOrder _currentOrder;

		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			JobTable.Activated += WorkOrderClicked;

			var orders = _db.WorkOrders
				.OrderByDescending (o => o.WorkOrderId)
				.ToList ();
			
			JobTable.Source = new WorkOrdersViewSource (orders);

			NewNoteField.Activated += AddNote;
		}

		private void WorkOrderClicked(object sender, EventArgs e)
		{
			if (JobTable.ClickedRow > -1) {
				_currentOrder = ((WorkOrdersViewSource)JobTable.Source).WorkOrders [JobTable.ClickedRow];

				OrderNoLabel.StringValue = "Job #" + _currentOrder.WorkOrderId.ToString().PadLeft(6, '0');

				var notes = _db.Notes
					.Where (n => n.WorkOrderId == _currentOrder.WorkOrderId)
					.ToList ();

				NoteTable.Source = new NoteViewSource (notes);
			}
		}
		 
		private void AddNote(object sender, EventArgs e)
		{
			if (_currentOrder != null && !string.IsNullOrWhiteSpace (NewNoteField.StringValue)) {
				var note = new Note {
					WorkOrderId = _currentOrder.WorkOrderId,
					Author = "Rowan Miller",
					Added = DateTime.Now,
					Content = NewNoteField.StringValue
				};

				_db.Notes.Add (note);
				_db.SaveChanges ();

				((NoteViewSource)NoteTable.Source).AddNote (note);
				NoteTable.ReloadData ();

				NewNoteField.StringValue = string.Empty;
			}
		}
	}
}

