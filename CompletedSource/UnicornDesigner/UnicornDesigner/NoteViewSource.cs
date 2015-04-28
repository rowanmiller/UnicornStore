using System;
using MonoMac.AppKit;
using System.Collections.Generic;

namespace UnicornDesigner
{
	public class NoteViewSource : NSTableViewSource
	{
		private List<Note> _notes;

		public NoteViewSource(List<Note> notes)
		{
			_notes = notes;
		}

		public List<Note> Notes { get { return _notes; }}

		public void AddNote(Note note)
		{
			_notes.Add (note);
		}

		public override int GetRowCount (NSTableView tableView)
		{
			return _notes.Count;
		}

		public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, int row)
		{
			var view = (NSTableCellView)tableView.MakeView (tableColumn.Identifier, this);

			string value = string.Empty;
			switch (tableColumn.HeaderCell.Title) {

			case "Added":
				value = _notes [row].Added.ToShortDateString();
				break;

			case "Author":
				value = _notes [row].Author;
				break;

			case "Note":
				value = _notes [row].Content;
				break;

			default:
				break;
			}

			view.TextField.StringValue = value;
			return view;
		}
	}
}

