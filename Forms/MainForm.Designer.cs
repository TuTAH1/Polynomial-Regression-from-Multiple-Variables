
namespace Program
{
	partial class MainForm
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
			this.panelColumnButtons = new System.Windows.Forms.Panel();
			this.labelOrder = new System.Windows.Forms.Label();
			this.tbOrder = new System.Windows.Forms.TextBox();
			this.btnCalculateCell = new System.Windows.Forms.Button();
			this.tbAddRow = new System.Windows.Forms.TextBox();
			this.btnAddRow = new System.Windows.Forms.Button();
			this.btnTrace = new System.Windows.Forms.Button();
			this.btnCalculateAll = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCalculatePolynom
			// 
			this.btnCalculatePolynom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCalculatePolynom.Location = new System.Drawing.Point(279, 654);
			this.btnCalculatePolynom.Name = "btnCalculatePolynom";
			this.btnCalculatePolynom.Size = new System.Drawing.Size(208, 40);
			this.btnCalculatePolynom.TabIndex = 8;
			this.btnCalculatePolynom.Text = "Вычислить полином";
			this.btnCalculatePolynom.UseVisualStyleBackColor = true;
			this.btnCalculatePolynom.EnabledChanged += new System.EventHandler(this.EnabledChanged);
			this.btnCalculatePolynom.Click += new System.EventHandler(this.btnCalculatePolynom_Click);
			// 
			// dgv1
			// 
			this.dgv1.AllowDrop = true;
			this.dgv1.AllowUserToAddRows = false;
			this.dgv1.AllowUserToOrderColumns = true;
			this.dgv1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv1.Location = new System.Drawing.Point(11, 127);
			this.dgv1.Name = "dgv1";
			this.dgv1.RowHeadersWidth = 62;
			this.dgv1.RowTemplate.Height = 33;
			this.dgv1.Size = new System.Drawing.Size(696, 521);
			this.dgv1.TabIndex = 13;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(255)))), ((int)(((byte)(222)))));
			this.btnSave.Location = new System.Drawing.Point(597, 8);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(107, 38);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.EnabledChanged += new System.EventHandler(this.EnabledChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoad.Location = new System.Drawing.Point(436, 8);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(155, 38);
			this.btnLoad.TabIndex = 5;
			this.btnLoad.Text = "Загрузить файл";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.EnabledChanged += new System.EventHandler(this.EnabledChanged);
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// labelInfo
			// 
			this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelInfo.Location = new System.Drawing.Point(436, 45);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(268, 38);
			this.labelInfo.TabIndex = 6;
			this.labelInfo.Text = "Полином вычислен!";
			this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnAddColumn
			// 
			this.btnAddColumn.Location = new System.Drawing.Point(223, 8);
			this.btnAddColumn.Name = "btnAddColumn";
			this.btnAddColumn.Size = new System.Drawing.Size(194, 38);
			this.btnAddColumn.TabIndex = 2;
			this.btnAddColumn.Text = "Добавить столбец";
			this.btnAddColumn.UseVisualStyleBackColor = true;
			this.btnAddColumn.EnabledChanged += new System.EventHandler(this.EnabledChanged);
			this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
			// 
			// tbAddColumn
			// 
			this.tbAddColumn.Location = new System.Drawing.Point(21, 8);
			this.tbAddColumn.Name = "tbAddColumn";
			this.tbAddColumn.Size = new System.Drawing.Size(195, 31);
			this.tbAddColumn.TabIndex = 1;
			this.tbAddColumn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAddColumn_KeyPress);
			// 
			// panelColumnButtons
			// 
			this.panelColumnButtons.Location = new System.Drawing.Point(11, 87);
			this.panelColumnButtons.Name = "panelColumnButtons";
			this.panelColumnButtons.Size = new System.Drawing.Size(999, 33);
			this.panelColumnButtons.TabIndex = 18;
			// 
			// labelOrder
			// 
			this.labelOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelOrder.AutoSize = true;
			this.labelOrder.Location = new System.Drawing.Point(12, 661);
			this.labelOrder.Name = "labelOrder";
			this.labelOrder.Size = new System.Drawing.Size(174, 25);
			this.labelOrder.TabIndex = 99;
			this.labelOrder.Text = "Порядок полинома";
			// 
			// tbOrder
			// 
			this.tbOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbOrder.Location = new System.Drawing.Point(192, 658);
			this.tbOrder.Name = "tbOrder";
			this.tbOrder.Size = new System.Drawing.Size(81, 31);
			this.tbOrder.TabIndex = 7;
			this.tbOrder.Text = "3";
			// 
			// btnCalculateCell
			// 
			this.btnCalculateCell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCalculateCell.Location = new System.Drawing.Point(12, 700);
			this.btnCalculateCell.Name = "btnCalculateCell";
			this.btnCalculateCell.Size = new System.Drawing.Size(314, 40);
			this.btnCalculateCell.TabIndex = 10;
			this.btnCalculateCell.Text = "Вычислить значение в этой ячейке";
			this.btnCalculateCell.UseVisualStyleBackColor = true;
			this.btnCalculateCell.EnabledChanged += new System.EventHandler(this.EnabledChanged);
			this.btnCalculateCell.Click += new System.EventHandler(this.btnCalculateCell_Click);
			// 
			// tbAddRow
			// 
			this.tbAddRow.Location = new System.Drawing.Point(21, 50);
			this.tbAddRow.Name = "tbAddRow";
			this.tbAddRow.Size = new System.Drawing.Size(195, 31);
			this.tbAddRow.TabIndex = 3;
			this.tbAddRow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAddRow_KeyPress);
			// 
			// btnAddRow
			// 
			this.btnAddRow.Location = new System.Drawing.Point(224, 50);
			this.btnAddRow.Name = "btnAddRow";
			this.btnAddRow.Size = new System.Drawing.Size(194, 38);
			this.btnAddRow.TabIndex = 4;
			this.btnAddRow.Text = "Добавить строку";
			this.btnAddRow.UseVisualStyleBackColor = true;
			this.btnAddRow.EnabledChanged += new System.EventHandler(this.EnabledChanged);
			this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
			// 
			// btnTrace
			// 
			this.btnTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTrace.Location = new System.Drawing.Point(493, 654);
			this.btnTrace.Name = "btnTrace";
			this.btnTrace.Size = new System.Drawing.Size(219, 40);
			this.btnTrace.TabIndex = 9;
			this.btnTrace.Text = "Трассировка полинома";
			this.btnTrace.UseVisualStyleBackColor = true;
			this.btnTrace.EnabledChanged += new System.EventHandler(this.EnabledChanged);
			this.btnTrace.Click += new System.EventHandler(this.btnTrace_Click);
			// 
			// btnCalculateAll
			// 
			this.btnCalculateAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCalculateAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
			this.btnCalculateAll.Location = new System.Drawing.Point(332, 700);
			this.btnCalculateAll.Name = "btnCalculateAll";
			this.btnCalculateAll.Size = new System.Drawing.Size(381, 40);
			this.btnCalculateAll.TabIndex = 11;
			this.btnCalculateAll.Text = "Вычислить значения во всех пустых ячейках";
			this.btnCalculateAll.UseVisualStyleBackColor = false;
			this.btnCalculateAll.EnabledChanged += new System.EventHandler(this.EnabledChanged);
			this.btnCalculateAll.Click += new System.EventHandler(this.btnCalculateAll_Click);
			this.btnCalculateAll.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonPaint);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(718, 745);
			this.Controls.Add(this.btnCalculateAll);
			this.Controls.Add(this.btnTrace);
			this.Controls.Add(this.btnAddRow);
			this.Controls.Add(this.tbAddRow);
			this.Controls.Add(this.btnCalculateCell);
			this.Controls.Add(this.tbOrder);
			this.Controls.Add(this.labelOrder);
			this.Controls.Add(this.panelColumnButtons);
			this.Controls.Add(this.tbAddColumn);
			this.Controls.Add(this.btnAddColumn);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.btnCalculatePolynom);
			this.Controls.Add(this.dgv1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnLoad);
			this.MinimumSize = new System.Drawing.Size(740, 300);
			this.Name = "MainForm";
			this.Text = "Регрессионный анализатор";
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
		private System.Windows.Forms.Panel panelColumnButtons;
		private System.Windows.Forms.Label labelOrder;
		private System.Windows.Forms.TextBox tbOrder;
		private System.Windows.Forms.Button btnCalculateCell;
		private System.Windows.Forms.TextBox tbAddRow;
		private System.Windows.Forms.Button btnAddRow;
		private System.Windows.Forms.Button btnTrace;
		private System.Windows.Forms.Button btnCalculateAll;
	}
}

