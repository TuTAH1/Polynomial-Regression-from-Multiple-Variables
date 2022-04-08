
namespace ТПРО
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnCalculatePolynom = new System.Windows.Forms.Button();
			this.dgv1 = new System.Windows.Forms.DataGridView();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.labelInfo = new System.Windows.Forms.Label();
			this.btnAddColumn = new System.Windows.Forms.Button();
			this.tbAddColumn = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelOrder = new System.Windows.Forms.Label();
			this.tbOrder = new System.Windows.Forms.TextBox();
			this.btnCalculateCell = new System.Windows.Forms.Button();
			this.tbAddRow = new System.Windows.Forms.TextBox();
			this.btnAddRow = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCalculatePolynom
			// 
			this.btnCalculatePolynom.Enabled = false;
			this.btnCalculatePolynom.Location = new System.Drawing.Point(281, 698);
			this.btnCalculatePolynom.Name = "btnCalculatePolynom";
			this.btnCalculatePolynom.Size = new System.Drawing.Size(194, 40);
			this.btnCalculatePolynom.TabIndex = 14;
			this.btnCalculatePolynom.Text = "Вычислить полином";
			this.btnCalculatePolynom.UseVisualStyleBackColor = true;
			this.btnCalculatePolynom.Click += new System.EventHandler(this.btnCalculatePolynom_Click);
			// 
			// dgv1
			// 
			this.dgv1.AllowDrop = true;
			this.dgv1.AllowUserToAddRows = false;
			this.dgv1.AllowUserToOrderColumns = true;
			this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv1.Location = new System.Drawing.Point(11, 127);
			this.dgv1.Name = "dgv1";
			this.dgv1.RowHeadersWidth = 62;
			this.dgv1.RowTemplate.Height = 33;
			this.dgv1.Size = new System.Drawing.Size(999, 567);
			this.dgv1.TabIndex = 13;
			// 
			// btnSave
			// 
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(779, 8);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(231, 38);
			this.btnSave.TabIndex = 12;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(540, 8);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(231, 38);
			this.btnLoad.TabIndex = 11;
			this.btnLoad.Text = "Загрузить файл";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// labelInfo
			// 
			this.labelInfo.Location = new System.Drawing.Point(540, 45);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(470, 38);
			this.labelInfo.TabIndex = 15;
			this.labelInfo.Text = "Полином вычислен!";
			this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnAddColumn
			// 
			this.btnAddColumn.Location = new System.Drawing.Point(223, 8);
			this.btnAddColumn.Name = "btnAddColumn";
			this.btnAddColumn.Size = new System.Drawing.Size(194, 38);
			this.btnAddColumn.TabIndex = 16;
			this.btnAddColumn.Text = "Добавить столбец";
			this.btnAddColumn.UseVisualStyleBackColor = true;
			this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
			// 
			// tbAddColumn
			// 
			this.tbAddColumn.Location = new System.Drawing.Point(21, 8);
			this.tbAddColumn.Name = "tbAddColumn";
			this.tbAddColumn.Size = new System.Drawing.Size(195, 31);
			this.tbAddColumn.TabIndex = 17;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(11, 87);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(999, 33);
			this.panel1.TabIndex = 18;
			// 
			// labelOrder
			// 
			this.labelOrder.AutoSize = true;
			this.labelOrder.Location = new System.Drawing.Point(14, 703);
			this.labelOrder.Name = "labelOrder";
			this.labelOrder.Size = new System.Drawing.Size(174, 25);
			this.labelOrder.TabIndex = 19;
			this.labelOrder.Text = "Порядок полинома";
			// 
			// tbOrder
			// 
			this.tbOrder.Location = new System.Drawing.Point(194, 700);
			this.tbOrder.Name = "tbOrder";
			this.tbOrder.Size = new System.Drawing.Size(81, 31);
			this.tbOrder.TabIndex = 20;
			this.tbOrder.Text = "3";
			// 
			// btnCalculateCell
			// 
			this.btnCalculateCell.Enabled = false;
			this.btnCalculateCell.Location = new System.Drawing.Point(540, 698);
			this.btnCalculateCell.Name = "btnCalculateCell";
			this.btnCalculateCell.Size = new System.Drawing.Size(470, 40);
			this.btnCalculateCell.TabIndex = 21;
			this.btnCalculateCell.Text = "Вычислить значение в этой ячейке";
			this.btnCalculateCell.UseVisualStyleBackColor = true;
			this.btnCalculateCell.Click += new System.EventHandler(this.btnCalculateCell_Click);
			// 
			// tbAddRow
			// 
			this.tbAddRow.Location = new System.Drawing.Point(21, 50);
			this.tbAddRow.Name = "tbAddRow";
			this.tbAddRow.Size = new System.Drawing.Size(195, 31);
			this.tbAddRow.TabIndex = 22;
			// 
			// btnAddRow
			// 
			this.btnAddRow.Location = new System.Drawing.Point(224, 50);
			this.btnAddRow.Name = "btnAddRow";
			this.btnAddRow.Size = new System.Drawing.Size(194, 38);
			this.btnAddRow.TabIndex = 23;
			this.btnAddRow.Text = "Добавить строку";
			this.btnAddRow.UseVisualStyleBackColor = true;
			this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1021, 745);
			this.Controls.Add(this.btnAddRow);
			this.Controls.Add(this.tbAddRow);
			this.Controls.Add(this.btnCalculateCell);
			this.Controls.Add(this.tbOrder);
			this.Controls.Add(this.labelOrder);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tbAddColumn);
			this.Controls.Add(this.btnAddColumn);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.btnCalculatePolynom);
			this.Controls.Add(this.dgv1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnLoad);
			this.Name = "Form1";
			this.Text = "Регрессионный анализ";
			((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnCalculatePolynom;
		private System.Windows.Forms.DataGridView dgv1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Label labelInfo;
		private System.Windows.Forms.Button btnAddColumn;
		private System.Windows.Forms.TextBox tbAddColumn;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelOrder;
		private System.Windows.Forms.TextBox tbOrder;
		private System.Windows.Forms.Button btnCalculateCell;
		private System.Windows.Forms.TextBox tbAddRow;
		private System.Windows.Forms.Button btnAddRow;
	}
}

