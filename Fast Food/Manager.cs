using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Fast_Food
{
	public partial class Manager : Form
	{
		string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Fast_Food2;Data Source=РОМАН-ПК\MSSQLSERVER01";
		DataSet ds = new DataSet();
		DataSet initds;
		SqlDataAdapter adapter;
		SqlCommandBuilder builder;
		public Manager()
		{
			InitializeComponent();
			dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			//using (SqlDataAdapter adapter = new SqlDataAdapter(SelectJurnal, connStr))
			//{
			//	adapter.Fill(ds, "Jurnal");
			//	dataGridView1.DataSource = ds.Tables["Jurnal"];
			//}
			dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT Ingredient_name, Cost_Price, Unit FROM Ingredients", connStr))
			{
				adapter.Fill(ds, "Ingredients");
				dataGridView2.DataSource = ds.Tables["Ingredients"];
				//dataGridView2.Columns["Id"].Visible = false;
				dataGridView2.Columns["Cost_Price"].DefaultCellStyle.Format = "n2";
			}
			dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT Menu.Dish_name, Menu.Price, Dish_groups.Group_name FROM Menu JOIN Dish_groups ON Menu.Group_id = Dish_groups.Group_id", connStr))
			{
				adapter.Fill(ds, "Menu");
				dataGridView3.DataSource = ds.Tables["Menu"];
				//dataGridView3.Columns["Id_menu"].Visible = false;
				dataGridView3.Columns["Price"].DefaultCellStyle.Format = "n2";
			}
			initds = ds.Copy();
		}
		private void button3_Click(object sender, EventArgs e)	// сохранить изменения в Ingredients
		{
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				adapter = new SqlDataAdapter();
				builder = new SqlCommandBuilder(adapter);
				adapter.SelectCommand = new SqlCommand("SELECT Ingredient_name, Cost_Price, Unit FROM Ingredients", conn);
				// закоменченный код не нужен, так как есть SqlCommandBuilder
				//adapter.InsertCommand = new SqlCommand("INSERT INTO Ingredients(Ingredient_name, Cost_Price, Unit) VALUES (@name, @cost, @unit)", conn);
				//adapter.InsertCommand.Parameters.Add("@name", SqlDbType.NVarChar, 150, "Ingredient_name");
				//adapter.InsertCommand.Parameters.Add("@cost", SqlDbType.SmallMoney, 0, "Cost_Price");
				//adapter.InsertCommand.Parameters.Add("@unit", SqlDbType.NVarChar, 10, "Unit");
				adapter.Update(ds, "Ingredients");
				initds = ds.Copy();
				adapter.Dispose();
				builder.Dispose();
			}
		}

		private void button2_Click(object sender, EventArgs e)  // отменить изменения в Ingredients
		{
			int currenttopindex = dataGridView2.FirstDisplayedCell.RowIndex;
			int startrowscount = dataGridView2.Rows.Count;
			int selectedrow = dataGridView2.CurrentRow.Index;
			ds = initds.Copy();
			dataGridView2.DataSource = ds.Tables["Ingredients"];
			if (currenttopindex < dataGridView2.RowCount)
			{
				dataGridView2.FirstDisplayedScrollingRowIndex = currenttopindex;
				dataGridView2.ClearSelection();
				dataGridView2.Rows[selectedrow].Selected = true;
			}
			else
			{
				dataGridView2.FirstDisplayedScrollingRowIndex = currenttopindex + dataGridView2.Rows.Count - startrowscount;
				dataGridView2.ClearSelection();
				dataGridView2.Rows[dataGridView2.Rows.Count - 1].Selected = true;
			}
		}

		private void button5_Click(object sender, EventArgs e)	// сохранить изменения в Menu
		{
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				adapter = new SqlDataAdapter();
				builder = new SqlCommandBuilder(adapter);
				adapter.SelectCommand = new SqlCommand("SELECT Menu.Dish_name, Menu.Price, Dish_groups.Group_name FROM Menu JOIN Dish_groups ON Menu.Group_id = Dish_groups.Group_id", conn);
				adapter.InsertCommand = new SqlCommand("INSERT INTO Menu(Dish_name, Price) VALUES (@dish, @price) INSERT INTO Dish_groups(Group_name) VALUES (@group)", conn);
				adapter.InsertCommand.Parameters.Add("@dish", SqlDbType.NVarChar, 150, "Dish_name");
				adapter.InsertCommand.Parameters.Add("@price", SqlDbType.SmallMoney, 0, "Price");
				adapter.InsertCommand.Parameters.Add("@group", SqlDbType.NVarChar, 50, "Group_name");
				adapter.Update(ds, "Menu");
				initds = ds.Copy();
				adapter.Dispose();
				builder.Dispose();
			}
		}

		private void button4_Click(object sender, EventArgs e)	// отменить изменения в Menu
		{
			int currenttopindex = dataGridView3.FirstDisplayedCell.RowIndex;
			int startrowscount = dataGridView3.Rows.Count;
			int selectedrow = dataGridView3.CurrentRow.Index;
			ds = initds.Copy();
			dataGridView3.DataSource = ds.Tables["Menu"];
			if (currenttopindex < dataGridView2.RowCount)
			{
				dataGridView3.FirstDisplayedScrollingRowIndex = currenttopindex;
				dataGridView3.ClearSelection();
				dataGridView3.Rows[selectedrow].Selected = true;
			}
			else
			{
				dataGridView3.FirstDisplayedScrollingRowIndex = currenttopindex + dataGridView2.Rows.Count - startrowscount;
				dataGridView3.ClearSelection();
				dataGridView3.Rows[dataGridView2.Rows.Count - 1].Selected = true;
			}
		}



		//public Manager()
		//{
		//	InitializeComponent();
		//}

		//private void button3_Click(object sender, EventArgs e)
		//{
		//	SqlConnection conn = new SqlConnection(connStr);
		//	try
		//	{
		//		conn.Open();
		//	}
		//	catch (SqlException se)
		//	{
		//		Console.WriteLine("Ошибка подключения: {0}", se.Message);
		//		return;
		//	}
		//	Console.WriteLine("Соединение произведено");
		//	//SqlCommand CreateTable = new SqlCommand("CREATE PROCEDURE [dbo].[add_Product]" +
		//	//										"	@name nvarchar(100)," +
		//	//										"	@cost float" +
		//	//										"AS" +
		//	//										"INSERT INTO Products (Name, Cost)" +
		//	//										"VALUES (@name, @cost)" +
		//	//										"GO", conn);
		//	SqlCommand CreateTable = new SqlCommand("INSERT INTO Products (Name, Cost) VALUES (Eggs, 2.99)", conn);
		//	try
		//	{
		//		CreateTable.ExecuteNonQuery();
		//	}
		//	catch
		//	{
		//		Console.WriteLine("Ошибка при создании процедуры");
		//		return;
		//	}

		//	Console.WriteLine("Процедура создана успешно");
		//	//закрвываем соединение
		//	conn.Close();
		//	conn.Dispose();

		//}
	}
}
