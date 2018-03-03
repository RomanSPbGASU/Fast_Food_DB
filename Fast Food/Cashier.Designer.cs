namespace Fast_Food
{
	partial class Cashier
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dgvMenu = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.dgvOrders = new System.Windows.Forms.DataGridView();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.button2 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvMenu)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dgvMenu);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(372, 360);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Меню";
			// 
			// dgvMenu
			// 
			this.dgvMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMenu.Location = new System.Drawing.Point(11, 19);
			this.dgvMenu.Name = "dgvMenu";
			this.dgvMenu.Size = new System.Drawing.Size(347, 287);
			this.dgvMenu.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(11, 319);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(348, 35);
			this.button1.TabIndex = 1;
			this.button1.Text = "Добавить в заказ";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.dgvOrders);
			this.groupBox2.Location = new System.Drawing.Point(390, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(386, 173);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Заказ";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(195, 132);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(185, 35);
			this.button3.TabIndex = 1;
			this.button3.Text = "Удалить из заказа";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// dgvOrders
			// 
			this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvOrders.Location = new System.Drawing.Point(6, 19);
			this.dgvOrders.Name = "dgvOrders";
			this.dgvOrders.Size = new System.Drawing.Size(374, 107);
			this.dgvOrders.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.dataGridView2);
			this.groupBox3.Location = new System.Drawing.Point(390, 191);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(385, 134);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Просмотр";
			this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
			// 
			// dataGridView2
			// 
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Location = new System.Drawing.Point(6, 19);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.Size = new System.Drawing.Size(374, 109);
			this.dataGridView2.TabIndex = 0;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(4, 132);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(185, 34);
			this.button2.TabIndex = 3;
			this.button2.Text = "Подтвердить заказ";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(585, 331);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(185, 34);
			this.button4.TabIndex = 3;
			this.button4.Text = "Отменить заказ";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// Cashier
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(788, 384);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Cashier";
			this.Text = "Cashier";
			this.Load += new System.EventHandler(this.Cashier_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvMenu)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView dgvOrders;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.DataGridView dgvMenu;
	}
}