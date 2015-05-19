using System;

namespace UnicornDesigner
{
	public class Note
	{
		public int NoteId { get; set; }
		public string Author { get; set; }
		public string Content { get; set; }
		public DateTime Added { get; set; }

		public int WorkOrderId { get; set; }
		public WorkOrder WorkOrder { get; set; }
	}
}

