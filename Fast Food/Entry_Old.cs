using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fast_Food
{
	public partial class Entry_Old : Form
	{
		public Entry_Old()
		{
			InitializeComponent();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			Cashier cashier = new Cashier();
			//cashier.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			cashier.ShowDialog();
		}
		private void button2_Click(object sender, EventArgs e)
		{
			Waiter waiter = new Waiter();
			waiter.ShowDialog();
		}
		private void button3_Click(object sender, EventArgs e)
		{
			Cook cook = new Cook();
			cook.ShowDialog();
		}
		private void button4_Click(object sender, EventArgs e)
		{
			Administrator admin = new Administrator();
			admin.ShowDialog();
		}
		private void button5_Click(object sender, EventArgs e)
		{
			Manager manager = new Manager();
			manager.ShowDialog();
		}
		private void button6_Click(object sender, EventArgs e)
		{
			Bookkeeper bookkeeper = new Bookkeeper();
			bookkeeper.ShowDialog();
		}
	}
}
