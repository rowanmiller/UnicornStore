using System.Collections.Generic;

namespace UnicornDesigner
{
	public class WorkOrder
	{
		public int WorkOrderId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string RequestedBy { get; set; }
		public string AssignedTo { get; set; }
		public bool IsCompleted { get; set; }

		public List<Note> Notes { get; set; }
	}
}

