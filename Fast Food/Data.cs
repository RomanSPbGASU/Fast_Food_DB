using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace Fast_Food
{
	class Data  // по материалам http://www.cyberforum.ru/ado-net/thread182279.html
	{
		string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Fast_Food2;Data Source=РОМАН-ПК\MSSQLSERVER01";
		public void CreateSqlDB()
		{
			SqlConnection myConnection = new SqlConnection(connStr);
			try
			{
				myConnection.Open();
				Console.WriteLine("Подключение открыто");
			}
			catch (SqlException sqlx)
			{
				if (sqlx.Number == 4060)    // если база данных не обнаружена
				{
					Console.WriteLine("Идет создание БД");
					myConnection.Close();
					myConnection = new SqlConnection(@"Data Source=РОМАН-ПК\MSSQLSERVER01; Integrated Security=SSPI");

					SqlCommand CreateDB = new SqlCommand("CREATE DATABASE [Fast_Food2]", myConnection);
					myConnection.Open();
					Console.WriteLine("Посылаем запрос");
					CreateDB.ExecuteNonQuery();
					myConnection.Close();
					Thread.Sleep(5000);

					myConnection = new SqlConnection(connStr);
					myConnection.Open();
				}
			}
			finally
			{
				myConnection.Close();   // закрываем подключение
				myConnection.Dispose(); // освобождаем ресурсы
				Console.WriteLine("Подключение закрыто...");
			}
		}
		public void CreateTables()
		{
			using (SqlConnection conn = new SqlConnection(connStr))	// создаём новое подключение
			{
				try													// пробуем открыть соедиение
				{
					conn.Open();
					Console.WriteLine("Соединение открыто успешно");
				}
				catch(SqlException sqlex)
				{
					Console.WriteLine("Ошибка соединения с базой данных:\n{0}", sqlex);
				}
				List<SqlCommand> CreateTablesCommand = new List<SqlCommand>();
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Ingredients(Ingredient_name nvarchar(100) PRIMARY KEY, Cost_price smallmoney, Unit nvarchar(10))"));
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Dish_Composition(Dish_id int REFERENCES Menu(Dish_id), Ingredient_name nvarchar(100) REFERENCES Ingredients(Ingredient_name), Amount int)"));
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Menu(Dish_id int PRIMARY KEY IDENTITY, Dish_name nvarchar(100), Group_id int REFERENCES Dish_groups(Group_id), Price smallmoney, Image varbinary(MAX))"));
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Dish_groups(Group_id int PRIMARY KEY IDENTITY, Group_name nvarchar(50))"));
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Orders(Order_id int PRIMARY KEY IDENTITY, Receipt_time datetime, Cashier_id int REFERENCES Employees(Employee_id), Delivery_time datetime, Waiter_id int REFERENCES Employees(Employee_id))"));
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Dish(Order_id int REFERENCES Orders(Order_id), Dish_id int REFERENCES Menu(Dish_id), Cook_id int REFERENCES Employees(Employee_id), Taking_time datetime, Cooking_time datetime)"));
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Employees(Employee_id int PRIMARY KEY IDENTITY, Name nvarchar(150), Position_id int  REFERENCES Positions(Position_id))"));
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Positions(Position_id int PRIMARY KEY IDENTITY, Position_name nvarchar(50))"));
				CreateTablesCommand.Add(new SqlCommand("CREATE TABLE Shifts(Employee_id int REFERENCES Employees(Employee_id), Entry_time datetime, Exit_time datetime, PRIMARY KEY(Employee_id, Entry_time))"));

				int RefErrCount;
				do
				{
					RefErrCount = 0;
					foreach (SqlCommand com in CreateTablesCommand)
						try
						{
							com.Connection = conn;
							com.ExecuteNonQuery();
							Console.WriteLine("Таблица {0} создана", com.CommandText.Split(new char[] { ' ', '(' })[2]);
						}
						catch (SqlException sqlx)
						{
							if (sqlx.Number == 1767)    // если таблица ссылается на несуществующую таблицу
								RefErrCount++;
							else if (sqlx.Number == 2714)   // если таблица уже существует
								Console.WriteLine("Таблица {0} уже существует", com.CommandText.Split(new char[] { ' ', '(' })[2]);
							else
								Console.WriteLine("{0}", sqlx.Message);
						}
				} while (RefErrCount != 0);
			}
		}
		public void DeleteTables()
		{
			using (SqlConnection conn  = new SqlConnection(connStr))
			{
				conn.Open();
				SqlCommand Deletetables = new SqlCommand("DeleteTables", conn);
				Deletetables.CommandType = CommandType.StoredProcedure;
				try
				{
					Deletetables.ExecuteNonQuery();
					Console.WriteLine("Таблицы успешно удалены");
				}
				catch (SqlException)
				{
					Console.WriteLine("Ошибка при удалении таблиц");
				}
			}



















			//using (SqlConnection conn = new SqlConnection(connStr))
			//{
			//	conn.Open();
			//	SqlCommand GetTables = new SqlCommand("SELECT * FROM sys.tables WHERE type_desc = 'USER_TABLE'", conn);
			//	SqlDataReader reader = GetTables.ExecuteReader();
			//	reader.Close();
			//	SqlCommand DropTables = new SqlCommand("DROP TABLE @table_name", conn);
			//	DropTables.Parameters.Add(new SqlParameter("@table_name", SqlDbType.NText));
			//	while (reader.Read())
			//	{
			//		Console.WriteLine(String.Format("{0}", reader[0]));
			//		DropTables.Parameters["@table_name"].Value = String.Format("{0}", reader[0]);
			//		DropTables.ExecuteNonQuery();
			//	}
			//}




			//using (SqlConnection conn = new SqlConnection(connStr))
			//{
			//	try
			//	{
			//		conn.Open();
			//		Console.WriteLine("Соединение создано успешно");
			//	}
			//	catch (SqlException sqlx)
			//	{
			//		Console.WriteLine("Ошибка соединения с базой данных:\n{0}", sqlx);
			//	}
			//	SqlCommand GetTables = new SqlCommand("SELECT * AS @table_name FROM sys.tables WHERE type_desc = 'USER_TABLE'", conn);
			//	GetTables.Parameters.Add("@table_name")
			//	SqlDataReader reader = GetTables.ExecuteReader();
			//	while (reader.Read())
			//	{
			//		Console.WriteLine(String.Format("{0}", reader[0]));
			//	}
			//	SqlCommand DeleteTables = new SqlCommand("DROP TABLE @table_name", conn);
			//	DeleteTables.Parameters.Add("@table_name", SqlDbType.NVarChar, 50);
			//	foreach (DataTable dt in reader.GetSchemaTable)
			//	{
			//		DeleteTables.Parameters["@table_name"].Value = dt;
			//		DeleteTables.ExecuteNonQuery();
			//	}
			//}
		}
	}
}
