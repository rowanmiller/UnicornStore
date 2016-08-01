using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace UnicornDesigner
{
	public class WorkOrdersViewSource : NSTableViewSource
	{
		private List<WorkOrder> _orders;

		public WorkOrdersViewSource(List<WorkOrder> orders)
		{
			_orders = orders;
		}

		public List<WorkOrder> WorkOrders { get { return _orders; }}

		public override int GetRowCount (NSTableView tableView)
		{
			return _orders.Count;
		}

		public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, int row)
		{
			var view = (NSTableCellView)tableView.MakeView (tableColumn.Identifier, this);

			string value = string.Empty;
			switch (tableColumn.HeaderCell.Title) {

			case "Job#":
				value = _orders [row].WorkOrderId.ToString().PadLeft(10, '0');
				break;

			case "Title":
				value = _orders [row].Title;
				break;

			default:
				break;
			}

			view.TextField.StringValue = value;
			return view;
		}
	}
}

