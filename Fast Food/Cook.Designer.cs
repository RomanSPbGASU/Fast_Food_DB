namespace Fast_Food
{
	partial class Cook
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.dgvOrders = new System.Windows.Forms.DataGridView();
			this.dgvCooked = new System.Windows.Forms.DataGridView();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvCooked)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dgvOrders);
			this.groupBox1.Location = new System.Drawing.Point(28, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(346, 464);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Заказанные блюда";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.dgvCooked);
			this.groupBox2.Location = new System.Drawing.Point(467, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(268, 193);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Готовые блюда";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(380, 81);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(81, 50);
			this.button1.TabIndex = 2;
			this.button1.Text = "Блюдо готово >>";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dgvOrders
			// 
			this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvOrders.Location = new System.Drawing.Point(6, 19);
			this.dgvOrders.Name = "dgvOrders";
			this.dgvOrders.Size = new System.Drawing.Size(334, 439);
			this.dgvOrders.TabIndex = 0;
			// 
			// dgvCooked
			// 
			this.dgvCooked.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvCooked.Location = new System.Drawing.Point(6, 19);
			this.dgvCooked.Name = "dgvCooked";
			this.dgvCooked.Size = new System.Drawing.Size(256, 168);
			this.dgvCooked.TabIndex = 0;
			// 
			// Cook
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(747, 488);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Cook";
			this.Text = "Cook";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvCooked)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView dgvOrders;
		private System.Windows.Forms.DataGridView dgvCooked;
	}
}