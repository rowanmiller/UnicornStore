// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace UnicornDesigner
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField DescriptionLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSScrollView DetailsSection { get; set; }

		[Outlet]
		MonoMac.AppKit.NSView DetailsView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView JobTable { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField NewNoteField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView NoteTable { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField OrderNoLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField StatusLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}

			if (DetailsView != null) {
				DetailsView.Dispose ();
				DetailsView = null;
			}

			if (JobTable != null) {
				JobTable.Dispose ();
				JobTable = null;
			}

			if (NewNoteField != null) {
				NewNoteField.Dispose ();
				NewNoteField = null;
			}

			if (NoteTable != null) {
				NoteTable.Dispose ();
				NoteTable = null;
			}

			if (OrderNoLabel != null) {
				OrderNoLabel.Dispose ();
				OrderNoLabel = null;
			}

			if (StatusLabel != null) {
				StatusLabel.Dispose ();
				StatusLabel = null;
			}

			if (DetailsSection != null) {
				DetailsSection.Dispose ();
				DetailsSection = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
