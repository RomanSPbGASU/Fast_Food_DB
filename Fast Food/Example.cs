using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Fast_Food
{
	public partial class Example : Form
	{
		string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Fast_Food2;Data Source=РОМАН-ПК\MSSQLSERVER01";
		DataSet ds = new DataSet();
		private BindingSource menuBS = new BindingSource();
		private BindingSource consistBS = new BindingSource();
		SqlDataAdapter menuAdapter, consistAdapter, groupsAdapter;

		public Example()
		{
			InitializeComponent();
			dgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			// Заполняем DataSet

			menuAdapter = new SqlDataAdapter("SELECT Dish_id, Dish_name, Group_name AS 'Group', Price, Description, Image FROM Menu", connStr)
			{
				InsertCommand = new SqlCommand("INSERT INTO Menu(Dish_name, Group_name, Price, Description, Image) VALUES(@name, @group, @price, @description, @image) SET @id = SCOPE_IDENTITY()"),
				//UpdateCommand = new SqlCommand("Update Menu SET Dish_id = @id, Dish_name = @name, Group_name = @group, Price = @price, Description = @description, Image = @image WHERE Dish_id = @id"),
				//DeleteCommand = new SqlCommand("DELETE FROM Menu WHERE Dish_id = @id")
			};
			SqlCommandBuilder menuCommandBuilder = new SqlCommandBuilder(menuAdapter);
			menuAdapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 100, "Dish_name"));
			menuAdapter.InsertCommand.Parameters.Add(new SqlParameter("@group", SqlDbType.NVarChar, 50, "Group"));
			menuAdapter.InsertCommand.Parameters.Add(new SqlParameter("@price", SqlDbType.SmallMoney, 0, "Price"));
			menuAdapter.InsertCommand.Parameters.Add(new SqlParameter("@description", SqlDbType.Text, 0, "Description"));
			menuAdapter.InsertCommand.Parameters.Add(new SqlParameter("@image", SqlDbType.VarBinary, 0, "Image"));
			SqlParameter insParam = menuAdapter.InsertCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Dish_id"));
			insParam.Direction = ParameterDirection.Output;
			//menuAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 100, "Dish_name"));
			//menuAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@group", SqlDbType.NVarChar, 50, "Group_name"));
			//menuAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@price", SqlDbType.SmallMoney, 0, "Price"));
			//menuAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@description", SqlDbType.Text, 0, "Description"));
			//menuAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@image", SqlDbType.VarBinary, 0, "Image"));
			//SqlParameter updParam = menuAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Dish_id"));
			//updParam.SourceVersion = DataRowVersion.Original;

			//SqlParameter delParam = menuAdapter.DeleteCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Dish_id"));
			//delParam.SourceVersion = DataRowVersion.Original;
			menuAdapter.Fill(ds, "Menu");

			consistAdapter = new SqlDataAdapter("SELECT DC.Dish_id, DC.Ingredient_name, DC.Amount, I.Cost_price, I.Unit FROM Dish_Composition AS DC JOIN Ingredients AS I ON I.Ingredient_name = DC.Ingredient_name", connStr)
			{
				InsertCommand = new SqlCommand("INSERT INTO Dish_Composition(")
			};

			consistAdapter.Fill(ds, "Consist");

			groupsAdapter = new SqlDataAdapter("SELECT * FROM Dish_Groups", connStr);
			groupsAdapter.Fill(ds, "Groups");
			SqlCommandBuilder groupsCB = new SqlCommandBuilder(groupsAdapter);
			ds.Relations.Add(new DataRelation("Menu-Consist", ds.Tables["Menu"].Columns["Dish_id"], ds.Tables["Consist"].Columns["Dish_id"]));
			// Связываем DataSet и DataGridView
			menuBS.DataSource = ds;
			menuBS.DataMember = "Menu";
			consistBS.DataSource = menuBS;
			consistBS.DataMember = "Menu-Consist";
			dgvConsist.DataSource = consistBS;
			ds.Tables["Groups"].PrimaryKey = new DataColumn[] { ds.Tables["Groups"].Columns["Group_name"] };    // создаём первичный ключ для метода Find()

			// Создаём столбец ComboBox-ов (Groups) для dgvMenu 
			dgvMenu.Columns.Add(new DataGridViewComboBoxColumn()
			{
				HeaderText = "Группа",
				//DisplayMember = "Group_name",	// совпадает с ValueMember
				ValueMember = "Group_name",
				DataPropertyName = "Group",
				DataSource = ds.Tables["Groups"],
				FlatStyle = FlatStyle.Flat,
				AutoComplete = true
			});
			dgvMenu.DataSource = menuBS;
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			//consistAdapter.Update(ds.Tables["Consist"]);
			groupsAdapter.Update(ds.Tables["Groups"]);
			menuAdapter.Update(ds.Tables["Menu"]);
			ds.AcceptChanges();
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			ds.RejectChanges();
		}
		#region Ввод значений в dgvComboBoxColumn
		private void dgvMenu_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			var cbo = e.Control as ComboBox;
			if (cbo == null)
				return;
			cbo.DropDownStyle = ComboBoxStyle.DropDown;
		}
		object editingvalue;
		private void dgvMenu_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			var cell = dgvMenu.CurrentCell as DataGridViewComboBoxCell;
			if (cell == null)
				return;
			var efv = cell.EditedFormattedValue;
			if (cell != null)
			{
				if (ds.Tables["Groups"].Rows.IndexOf(ds.Tables["Groups"].Rows.Find(efv)) == -1)
				{
					editingvalue = ds.Tables["Groups"].Rows.Add(new object[] { efv }).ItemArray[0];
				}
			}
		}
		private void dgvMenu_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (dgvMenu.CurrentCell.GetType() == typeof(DataGridViewComboBoxCell))
			{
				if (editingvalue != null)
				{
					dgvMenu.CurrentCell.Value = editingvalue;
					editingvalue = null;
				}
			}
		}
		#endregion
	}
}
