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
		private BindingSource menuBS = new BindingSource(), consistBS = new BindingSource();
		SqlDataAdapter menuAdapter, consistAdapter, groupsAdapter, ingredAdapter;

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
				InsertCommand = new SqlCommand("INSERT INTO Dish_Composition(Dish_id, Ingredient_name, Amount VALUES(@id, @name, @amount) INSERT INTO Ingredients(Ingredient_name, Cost_price, Unit) VALUES (@name, @cost, @unit"),
				UpdateCommand = new SqlCommand(""),
				DeleteCommand = new SqlCommand("")
			};
			
			//consistAdapter.InsertCommand.Parameters.Add(new SqlParameter("@id"))
			consistAdapter.Fill(ds, "Consist");





			groupsAdapter = new SqlDataAdapter("SELECT * FROM Dish_Groups", connStr);
			groupsAdapter.Fill(ds, "Groups");
			SqlCommandBuilder groupsCB = new SqlCommandBuilder(groupsAdapter);





			ingredAdapter = new SqlDataAdapter("SELECT Ingredient_name FROM Ingredients", connStr);
			ingredAdapter.Fill(ds, "Ingred");
			SqlCommandBuilder ingredCB = new SqlCommandBuilder(ingredAdapter);




			ds.Relations.Add(new DataRelation("Menu-Consist", ds.Tables["Menu"].Columns["Dish_id"], ds.Tables["Consist"].Columns["Dish_id"]));





			// Связываем DataSet и DataGridView
			menuBS.DataSource = ds;
			menuBS.DataMember = "Menu";
			consistBS.DataSource = menuBS;
			consistBS.DataMember = "Menu-Consist";
			dgvConsist.Columns.Add(new DataGridViewComboBoxColumn()
			{
				HeaderText = "Ингредиент",
				ValueMember = "Ingredient_name",
				DataPropertyName = "Ingredient_name",
				DataSource = ds.Tables["Ingred"],
				FlatStyle = FlatStyle.Flat,
				AutoComplete = true
			});
			dgvConsist.DataSource = consistBS;



			ds.Tables["Groups"].PrimaryKey = new DataColumn[] { ds.Tables["Groups"].Columns["Group_name"] };    // создаём первичный ключ для метода Find()


			ds.Tables["Ingred"].PrimaryKey = new DataColumn[] { ds.Tables["Ingred"].Columns["Ingredient_name"] };



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
			ingredAdapter.Update(ds.Tables["Ingred"]);
			ds.AcceptChanges();
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			ds.RejectChanges();
		}
		#region Ввод значений в dgvComboBoxColumn
		private void dgvConsist_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			var cbo = e.Control as ComboBox;
			if (cbo == null)
				return;
			cbo.DropDownStyle = ComboBoxStyle.DropDown;
		}
		private void dgvConsist_CellLeave(object sender, DataGridViewCellEventArgs e)
		{
			if (dgvConsist.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewComboBoxColumn))
			{
				if (dgvConsist[e.ColumnIndex, e.RowIndex].IsInEditMode)
				{
					object efv = dgvConsist[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
					if (ds.Tables["Ingred"].Rows.Find(efv) == null ? true : false)
					{
						ds.Tables["Ingred"].Rows.Add(efv);
						dgvConsist[e.ColumnIndex, e.RowIndex].Value = efv;
						return;
					}
				}
			}
		}
		private void dgvMenu_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			var cbo = e.Control as ComboBox;
			if (cbo == null)
				return;
			cbo.DropDownStyle = ComboBoxStyle.DropDown;
		}

		private void dgvMenu_CellLeave(object sender, DataGridViewCellEventArgs e)
		{
			if (dgvMenu.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewComboBoxColumn))
			{
				if (dgvMenu[e.ColumnIndex, e.RowIndex].IsInEditMode)
				{
					object efv = dgvMenu[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
					if (ds.Tables["Groups"].Rows.Find(efv) == null ? true : false)
					{
						ds.Tables["Groups"].Rows.Add(efv);
						dgvMenu[e.ColumnIndex, e.RowIndex].Value = efv;
					}
				}
			}
		}
		#endregion
	}
}
