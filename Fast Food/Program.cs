using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fast_Food
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//Data d1 = new Data();
			//d1.CreateSqlDB();
			//d1.DeleteTables();
			//d1.CreateTables();
			//Application.Run(new Entry_Old());
			Application.Run(new Example());
		}
	}
}
