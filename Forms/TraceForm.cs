using System;
using System.Windows.Forms;

namespace Program
{
	public partial class TraceForm : Form
	{
		public TraceForm(PolynomialRegression[,] Polynoms, string[] ColumnNames)
		{
			var dimension = ColumnNames.Length;
			if (Polynoms.GetLength(0) != dimension) throw new ArgumentOutOfRangeException("Polynoms dimension != ColumnNames dimension");
			InitializeComponent();
			foreach (var column in ColumnNames)
			{
				dgv.Columns.Add(column, column);
			}

			dgv.Rows.Add(dimension);

			for (int i = 0; i < dimension; i++)
			{
				dgv.Rows[i].HeaderCell.Value = ColumnNames[i];
				for (int j = 0; j < dimension; j++)
				{
					dgv.Rows[i].Cells[j].Value = Polynoms[i, j]?.GetCoefs();
				}
			}
			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgv.AutoSize = true;
			dgv.AutoResizeColumns();
			dgv.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
			dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
		}
	}
}
