using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Fast_Food
{
	public partial class Example : Form
	{
		string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Fast_Food2;Data Source=РОМАН-ПК\MSSQLSERVER01";
		DataSet ds = new DataSet();
		private BindingSource menuBS = new BindingSource();
		private BindingSource consistBS = new BindingSource();
		//DataSet initds;
		//SqlDataAdapter adapter;
		//SqlCommandBuilder builder;

		public Example()
		{
			InitializeComponent();
			dgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			// Заполняем DataSet
			using (SqlDataAdapter menuAdapter = new SqlDataAdapter("SELECT Dish_id, Dish_name, Group_id AS 'Group', Price, Description, Image FROM Menu", connStr)
			{
				InsertCommand = new SqlCommand(),
				UpdateCommand = new SqlCommand(),
			})
				menuAdapter.Fill(ds, "Menu");
			using (SqlDataAdapter consistAdapter = new SqlDataAdapter("SELECT * FROM Dish_Composition", connStr))
				consistAdapter.Fill(ds, "Consist");
			using (SqlDataAdapter groupsAdapter = new SqlDataAdapter("SELECT * FROM Dish_Groups", connStr))
				groupsAdapter.Fill(ds, "Groups");
			using (SqlDataAdapter ingredientsAdapter = new SqlDataAdapter("SELECT * FROM Ingredients", connStr))
				ingredientsAdapter.Fill(ds, "Groups");
			ds.Relations.Add(new DataRelation("Menu-Consist", ds.Tables["Menu"].Columns["Dish_id"], ds.Tables["Consist"].Columns["Dish_id"]));
			// Привязываем DataSet к DataGridView
			menuBS.DataSource = ds;
			menuBS.DataMember = "Menu";
			consistBS.DataSource = menuBS;
			consistBS.DataMember = "Menu-Consist";
			dgvConsist.DataSource = consistBS;
			// Создаём столбец ComboBox-ов (Groups) для dgvMenu 
			dgvMenu.Columns.Add(new DataGridViewComboBoxColumn() {
				HeaderText = "Группа",
				DisplayMember = "Group_name",
				ValueMember = "Group_id",
				DataPropertyName = "Group",
				DataSource = ds.Tables["Groups"],
				FlatStyle = FlatStyle.Flat,
				AutoComplete = true});
			dgvMenu.DataSource = menuBS;
		}
		private void dgvMenu_EditingControlShowing(object)








		//public Example()
		//{
		//	InitializeComponent();
		//	dgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		//	//using (SqlDataAdapter adapter = new SqlDataAdapter(SelectJurnal, connStr))
		//	//{
		//	//	adapter.Fill(ds, "Jurnal");
		//	//	dataGridView1.DataSource = ds.Tables["Jurnal"];
		//	//}
		//	dgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		//	using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT Ingredient_name, Cost_Price, Unit FROM Ingredients", connStr))
		//	{
		//		adapter.Fill(ds, "Ingredients");
		//		dgvMenu.DataSource = ds.Tables["Ingredients"];
		//		//dataGridView2.Columns["Id"].Visible = false;
		//		dgvMenu.Columns["Cost_Price"].DefaultCellStyle.Format = "n2";
		//	}




		//	dgvConsist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		//	using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT Menu.Dish_name, Menu.Price, Dish_groups.Group_name FROM Menu JOIN Dish_groups ON Menu.Group_id = Dish_groups.Group_id", connStr))
		//	{
		//		adapter.Fill(ds, "Menu");
		//		dgvConsist.DataSource = ds.Tables["Menu"];
		//		dgvConsist.Columns["Price"].DefaultCellStyle.Format = "n2";
		//	}
		//	initds = ds.Copy();
		//}
		//private void button3_Click(object sender, EventArgs e)  // сохранить изменения в Ingredients
		//{
		//	using (SqlConnection conn = new SqlConnection(connStr))
		//	{
		//		adapter = new SqlDataAdapter();
		//		builder = new SqlCommandBuilder(adapter);
		//		adapter.SelectCommand = new SqlCommand("SELECT Ingredient_name, Cost_Price, Unit FROM Ingredients", conn);
		//		// закоменченный код не нужен, так как есть SqlCommandBuilder
		//		//adapter.InsertCommand = new SqlCommand("INSERT INTO Ingredients(Ingredient_name, Cost_Price, Unit) VALUES (@name, @cost, @unit)", conn);
		//		//adapter.InsertCommand.Parameters.Add("@name", SqlDbType.NVarChar, 150, "Ingredient_name");
		//		//adapter.InsertCommand.Parameters.Add("@cost", SqlDbType.SmallMoney, 0, "Cost_Price");
		//		//adapter.InsertCommand.Parameters.Add("@unit", SqlDbType.NVarChar, 10, "Unit");
		//		adapter.Update(ds, "Ingredients");
		//		initds = ds.Copy();
		//		adapter.Dispose();
		//		builder.Dispose();
		//	}
		//}

		//private void button2_Click(object sender, EventArgs e)  // отменить изменения в Ingredients
		//{
		//	int currenttopindex = dgvMenu.FirstDisplayedCell.RowIndex;
		//	int startrowscount = dgvMenu.Rows.Count;
		//	int selectedrow = dgvMenu.CurrentRow.Index;
		//	ds = initds.Copy();
		//	dgvMenu.DataSource = ds.Tables["Ingredients"];
		//	if (currenttopindex < dgvMenu.RowCount)
		//	{
		//		dgvMenu.FirstDisplayedScrollingRowIndex = currenttopindex;
		//		dgvMenu.ClearSelection();
		//		dgvMenu.Rows[selectedrow].Selected = true;
		//	}
		//	else
		//	{
		//		dgvMenu.FirstDisplayedScrollingRowIndex = currenttopindex + dgvMenu.Rows.Count - startrowscount;
		//		dgvMenu.ClearSelection();
		//		dgvMenu.Rows[dgvMenu.Rows.Count - 1].Selected = true;
		//	}
		//}

		//private void button5_Click(object sender, EventArgs e)  // сохранить изменения в Menu
		//{
		//	using (SqlConnection conn = new SqlConnection(connStr))
		//	{
		//		adapter = new SqlDataAdapter();
		//		builder = new SqlCommandBuilder(adapter);
		//		adapter.SelectCommand = new SqlCommand("SELECT Menu.Dish_name, Menu.Price, Dish_groups.Group_name FROM Menu JOIN Dish_groups ON Menu.Group_id = Dish_groups.Group_id", conn);
		//		adapter.InsertCommand = new SqlCommand("INSERT INTO Menu(Dish_name, Price) VALUES (@dish, @price) INSERT INTO Dish_groups(Group_name) VALUES (@group)", conn);
		//		adapter.InsertCommand.Parameters.Add("@dish", SqlDbType.NVarChar, 150, "Dish_name");
		//		adapter.InsertCommand.Parameters.Add("@price", SqlDbType.SmallMoney, 0, "Price");
		//		adapter.InsertCommand.Parameters.Add("@group", SqlDbType.NVarChar, 50, "Group_name");
		//		adapter.Update(ds, "Menu");
		//		initds = ds.Copy();
		//		adapter.Dispose();
		//		builder.Dispose();
		//	}
		//}

		//private void button4_Click(object sender, EventArgs e)  // отменить изменения в Menu
		//{
		//	int currenttopindex = dgvConsist.FirstDisplayedCell.RowIndex;
		//	int startrowscount = dgvConsist.Rows.Count;
		//	int selectedrow = dgvConsist.CurrentRow.Index;
		//	ds = initds.Copy();
		//	dgvConsist.DataSource = ds.Tables["Menu"];
		//	if (currenttopindex < dgvMenu.RowCount)
		//	{
		//		dgvConsist.FirstDisplayedScrollingRowIndex = currenttopindex;
		//		dgvConsist.ClearSelection();
		//		dgvConsist.Rows[selectedrow].Selected = true;
		//	}
		//	else
		//	{
		//		dgvConsist.FirstDisplayedScrollingRowIndex = currenttopindex + dgvMenu.Rows.Count - startrowscount;
		//		dgvConsist.ClearSelection();
		//		dgvConsist.Rows[dgvMenu.Rows.Count - 1].Selected = true;
		//	}
		//}
	}
}
