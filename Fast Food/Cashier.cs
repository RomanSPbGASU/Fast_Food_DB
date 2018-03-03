using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Fast_Food
{
	public partial class Cashier : Form
	{
		string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Fast_Food2;Data Source=РОМАН-ПК\MSSQLSERVER01";
		string SelectMenu = "SELECT * FROM Menu";
		//string SelectSales = "SELECT * FROM Sales";
		//string SelectOrders = "SELECT * FROM Orders";
		DataSet ds = new DataSet();
		DataSet initds;
		//SqlDataAdapter adapter;
		//SqlCommandBuilder builder;
		public Cashier()
		{
			InitializeComponent();
			dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			// создадим таблицу (DataTable) для заказов
			DataTable TableOrders = new DataTable("Orders");
			TableOrders.Columns.Add("Id_order", typeof(int));
			TableOrders.Columns.Add("Id_dish", typeof(int));
			// добавим таблицу заказов в DataSet
			ds.Tables.Add(TableOrders);
			// назначим таблицу заказов источником для dataGridView
			dgvOrders.DataSource = ds.Tables["Orders"];

			// зададим id заказа по умолчанию (равным максимальному Id_orders в БД + 1)
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
				SqlCommand GetLast_id_order = new SqlCommand("SELECT MAX(Id_order) FROM Sales", conn);
				ds.Tables["Orders"].Columns["Id_order"].DefaultValue = (int)GetLast_id_order.ExecuteScalar() + 1;
			}

			//using (SqlDataAdapter adapter = new SqlDataAdapter(SelectSales, connStr))
			//{
			//	adapter.Fill(ds, "Sales");
			//	dataGridView1.DataSource = ds.Tables["Sales"];
			//}

			dgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			using (SqlDataAdapter adapter = new SqlDataAdapter(SelectMenu, connStr))
			{
				adapter.Fill(ds, "Menu");
				dgvMenu.DataSource = ds.Tables["Menu"];
				//dataGridView3.Columns["Id_menu"].Visible = false;
				dgvMenu.Columns["Price"].DefaultCellStyle.Format = "n2";
			}
			initds = ds.Copy();

		}
		private void button1_Click(object sender, EventArgs e)  // добавить блюдо из меню в datatable заказа (в составе ds)
		{
			// для каждой выделенной строки в dgv.меню добавляем строку в datatable.заказа
			//  значение в столбец Id_Orders будет добавлено при сохранении заказа в таблицу Sales
			for (int i = dgvMenu.SelectedRows.Count - 1; i >= 0; i--)
			{
				DataRow row = ds.Tables["Orders"].NewRow();
				row["Id_dish"] = dgvMenu.SelectedRows[i].Cells["Id_menu"].Value;
				ds.Tables["Orders"].Rows.Add(row);
			}
		}

		private void button2_Click(object sender, EventArgs e)	// подтверждение заказа
		{
			// добавить продукты в таблицу продаж

			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
				ds.Tables["Orders"].Columns["Id_order"].DefaultValue = (int)ds.Tables["Orders"].Columns["Id_order"].DefaultValue + 1;
				SqlCommand ConfirmOrder = new SqlCommand("INSERT INTO Sales(Id_order, Id_dish) VALUES (@id_order, @id_dish)", conn);
				ConfirmOrder.Parameters.Add("@id_order", SqlDbType.Int);
				ConfirmOrder.Parameters.Add("@id_dish", SqlDbType.Int);
				foreach (DataRow row in ds.Tables["Orders"].Rows)
				{
					ConfirmOrder.Parameters["@id_order"].Value = row["Id_order"];
					ConfirmOrder.Parameters["@id_dish"].Value = row["Id_dish"];
					ConfirmOrder.ExecuteNonQuery();
				}
				ds.Tables["Orders"].Clear();

				//SqlDataAdapter adapter = new SqlDataAdapter
				//{
				//	SelectCommand = new SqlCommand("SELECT * FROM Sales; SET @last_id_order = SCOPE_IDENTITY()", conn),
				//	InsertCommand = new SqlCommand("INSERT INTO Sales(Id_order, Id_dish) VALUES (@id_order, @id_dish); SET @last_id_order = SCOPE_IDENTITY() ", conn)
				//};
				//SqlParameter last_id_order_Param = new SqlParameter
				//{
				//	ParameterName = "@last_id_order",
				//	SqlDbType = SqlDbType.Int,
				//	Direction = ParameterDirection.Output
				//};
				//adapter.SelectCommand.Parameters.Add(last_id_order_Param);
				//ds.Tables["Orders"].Columns["Id_order"].DefaultValue = (int)last_id_order_Param.Value + 1;
				//adapter.InsertCommand.Parameters.Add("@id_order", SqlDbType.NVarChar, 150, "Id_order");
				//adapter.InsertCommand.Parameters.Add("@id_dish", SqlDbType.SmallMoney, 0, "Id_dish");
				//adapter.Update(ds, "Orders");
				//ds.Tables["Orders"].Clear();
				//adapter.Dispose();
			}
		}






















		//// добавляет данные в таблицу заказы и обновляет dataGridView
		//private void button1_Click(object sender, EventArgs e)  // добавить блюдо из меню в заказы
		//{
		//	using (SqlConnection conn = new SqlConnection(connStr))
		//	{
		//		conn.Open();
		//		dataGridView3.DataSource = ds.Tables["Menu"];

		//		//foreach (DataGridViewRow selectedrow in dataGridView3.SelectedRows)
		//		for (int i = 0; i < dataGridView3.SelectedRows.Count; i++)
		//		{
		//			SqlCommand AddToOrders = new SqlCommand("INSERT INTO Orders(Id_dish) VALUES (@id_dish)", conn);
		//			AddToOrders.Parameters.Add(new SqlParameter("@id_dish", dataGridView3.SelectedRows[i].Cells["Id_menu"].Value));
		//			AddToOrders.ExecuteNonQuery();
		//		}
		//	}
		//	using (SqlDataAdapter adapter = new SqlDataAdapter(SelectOrders, connStr))
		//	{
		//		adapter.Fill(ds, "Orders");
		//	}
		//}

		private void groupBox3_Enter(object sender, EventArgs e)
		{

		}

		private void Cashier_Load(object sender, EventArgs e)
		{

		}


	}
}

