using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra.Double;
using Titanium;

namespace ТПРО
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			btnSave.Enabled = false;
			labelInfo.Text = "";
			dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgv1.AutoResizeColumns();
			panel1.Controls.Add(new Button() {AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink});
		}

		#region Обработчики событий

		private void btnLoad_Click(object sender, EventArgs eArgs)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Multiselect = false;
			openDialog.Title = "Выберите файл";
			openDialog.Filter = "Файл таблицы|*.dgf";
			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				labelInfo.Text = "Идёт загрузка файла...";
				dgv1.Clear();
				loadFile(File.ReadLines(openDialog.FileName, Encoding.Default).ToArray());
				labelInfo.Text = "";
			}
			
		}

		private void btnSave_Click(object sender, EventArgs eArgs)
		{
			SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.Title = "Выберите место для сохранения";
			saveDialog.Filter = "Файл таблицы|*.dgf";
			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				labelInfo.Text = "Идёт сохранение файла...";
				SaveFile(saveDialog.FileName);
				labelInfo.Text = "";

			}
		}

		private void btnAddColumn_Click(object sender, EventArgs eArgs)
		{
			var ColumnName = tbAddColumn.Text;
			dgv1.Columns.Add(ColumnName,ColumnName);
			Button btn = new Button {BackColor = Color.DarkRed, ForeColor = Color.White};
			btn.Click += DeleteColumn_Click;
			ColumnButtons.Add(btn);
			panel1.Controls.Add(btn);
			ResizeButtons();
			if (dgv1.Columns.Count >= 2) btnCalculatePolynom.Enabled = true;
			btnCalculatePolynom.Text = "Вычислить полином";
			btnCalculateCell.Enabled = false;
			_polynoms = null;
		}

		private void btnAddRow_Click(object sender, EventArgs e)
		{
			var RowName = tbAddRow.Text;
			dgv1.Rows.Add(1);
			dgv1.Rows[^1].HeaderCell.Value = RowName;

		}

		void DeleteColumn_Click(object sender, EventArgs eArgs)
		{
			int Row = ColumnButtons.IndexOf(sender as Button);
			ColumnButtons[Row].Dispose();
			ColumnButtons.RemoveAt(Row);
			dgv1.Columns.RemoveAt(Row);
			ResizeButtons();
			if (dgv1.Columns.Count < 2) btnCalculatePolynom.Enabled = false;
			btnCalculatePolynom.Text = "Вычислить полином";
			btnCalculateCell.Enabled = false;
			_polynoms = null;
		}

		private PolynomialRegression[,] _polynoms;

		private void btnCalculatePolynom_Click(object sender, EventArgs eArgs)
		{
			try
			{
				int columnsCount = dgv1.Columns.Count;
				if (tbOrder.Text == "") throw new ApplicationException("Введите порядок перед вычислением");
				int order = tbOrder.Text.ToIntT(false, -1);
				if (order == -1) throw new ApplicationException("Порядок должно быть числом");
				tbOrder.Text = order.ToString();

				List<double[]> stuffedRow = new List<double[]>(); //: Строки, где ВСЕ ячейки заполненны
				bool[,] emptyCells = new bool[dgv1.Rows.Count,columnsCount];
				stuffedRow.Add(new double[dgv1.Columns.Count]);

				int k = 0;
				bool skip;
				for (int i = 0; i < dgv1.Rows.Count; i++)
				{
					skip = false;
					for (int j = 0; j <columnsCount; j++)
					{
						if (skip) emptyCells[i, j] = true;
						else
						{
							double temp = -1;
							try
							{
								temp = dgv1[j, i].Value.ToString().ToDoubleT();
							}
							catch (Exception)
							{
								skip = true; //:Если хотя бы одна из ячеек не заполнена, пропускаю строку
							}

							stuffedRow[k][j] = temp;
						}
					}
					if (skip) continue;
					stuffedRow.Add(new double[dgv1.Columns.Count]);
					k++;
				}

				if (k == 0) throw new ApplicationException("Ни найдено ни одной строки с полными данными. Для регрессионного анализа необходимо хотя бы несколько полностью заполненных строк");
				if (k <= order+1) throw new ApplicationException("Слишком мало полных данных для регрессионного анализа. Попробуйте понизить порядок");


				List<double[]> benchmarkColumns = new List<double[]>();

				for (int i = 0; i < columnsCount; i++)
				{
					benchmarkColumns.Add(new double[stuffedRow.Count]); 
				}

				for (int i = 0; i < stuffedRow.Count; i++)
				{
					for (int j = 0; j < stuffedRow[i].Length; j++)
					{
						benchmarkColumns[j][i] = stuffedRow[i][j];
					}
				}

				_polynoms = new PolynomialRegression[dgv1.Columns.Count,columnsCount]; //: Кубик зависимостей [известный столбец, неизвестный столбец], диагональ пуста

				for (int i = 0; i < columnsCount; i++)
				{
					for (int j = 0; j < columnsCount; j++)
					{
						if (i==j) continue;
						_polynoms[i, j] = new PolynomialRegression(DenseVector.OfArray(benchmarkColumns[i]), DenseVector.OfArray(benchmarkColumns[j]), order);
					}
				}

				btnCalculateCell.Enabled = true;
				btnCalculatePolynom.Text = "Пересчитать полином";
			}
			catch (ApplicationException ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCalculateCell_Click(object sender, EventArgs e)
		{
			var point = dgv1.CurrentCellAddress;
			int calculationsCount = 0;
			double result = 0;
			for (int i = 0; i < dgv1.Columns.Count; i++)
			{
				try
				{
					result+= _polynoms[i, point.X].Fit(dgv1[i, point.Y].Value.ToString().ToDoubleT());
				}
				catch (Exception ) { continue;}

				calculationsCount++;
			}

			dgv1[point.X, point.Y].Value = result/calculationsCount;  //: Среднее арифметическое из всех вычисленных предположений

		}

		#endregion

		#region Интерфейсные функции

		private void loadFile(string[] fileLines)
		{
			try
			{
				
				if (fileLines.Length == 0) throw new FileLoadException("Файл пустой");

				dgv1.Rows.Clear();

				foreach (var columnName in fileLines[0].Split('\t'))
				{
					if(columnName!= "") dgv1.Columns.Add(columnName, columnName);
				}
				

				//if (fileLines.Length == 1) throw new FileLoadException("Файл не содержит данных, но имена столбцов добавлены");


				foreach (var line in fileLines[1..])
				{
					var rows = line.Split('\t');
					dgv1.Rows.Add(rows[1..]);
					dgv1.Rows[^1].HeaderCell.Value = rows[0];
				}

				btnCalculatePolynom.Enabled = true;
				btnCalculatePolynom.Text = "Рассчитать";
				btnSave.Enabled = false;
				MessageBox.Show("Файл успешно загружен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (FileNotFoundException)
			{
				MessageBox.Show($"Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void SaveFile(string Path)
		{
			try
			{
				using (var sw = new StreamWriter(Path,true))
				{
					for (int i = 0; i < dgv1.Columns.Count; i++)
					{
						sw.Write(dgv1.Columns[i].HeaderText + (i== dgv1.Columns.Count-1? "": '\t'));
					}

					sw.WriteLine();

					for (int i = 0; i < dgv1.Rows.Count; i++)
					{
						sw.Write(dgv1.Rows[i].HeaderCell.Value .ToString() + '\t');
						for (int j = 0; j < dgv1.Rows[i].Cells.Count; j++)
						{
							sw.Write(dgv1.Rows[i].Cells[j].Value.ToString() + (j==dgv1.Rows[i].Cells.Count-1? '\n':'\t'));
						}
					}

					/*foreach (DataGridViewRow row in dgv1.Rows)
					{
						foreach (DataGridViewCell cell in row.Cells)
						{
							sw.WriteLine(cell.Value.ToString() + '\t');
						}	
						sw.WriteLine('\n');
					}*/
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private List<Button> ColumnButtons = new();

		enum SizeEnum
		{
			undefined,
			Max,
			Medium,
			Min
		}

		void ResizeButtons()
		{

			int ColumnLeft = dgv1.RowHeadersWidth;
			for (int i = 0; i < ColumnButtons.Count; i++)
			{
				int size = dgv1.Columns[i].Width;
				SizeEnum sizeType;
				if (size > 156) sizeType = SizeEnum.Max;
				else if (size > 86) sizeType = SizeEnum.Medium;
				else sizeType = SizeEnum.Min;

				string text = "";
				switch (sizeType)
				{
					case SizeEnum.Max: text = "Удалить столбец";
						break;

					case SizeEnum.Medium: text = "Удалить";
						break;

					case SizeEnum.Min: text = "×";
						break;
				}

				ColumnButtons[i].Text = text;
				ColumnButtons[i].Left = ColumnLeft;
				ColumnButtons[i].Size = new Size(size, panel1.Size.Height);
				ColumnLeft += dgv1.Columns[i].Width+1;
			}
		}

		#endregion

		#region Математические функции

		#endregion

	}
	public static class MyFuncs
	{
		public static void Clear(this DataGridView dgv)
		{
			dgv.Rows.Clear();  // удаление всех строк
			int count = dgv.Columns.Count;
			for (int i = 0; i < count; i++)     // цикл удаления всех столбцов
			{
				dgv.Columns.RemoveAt(0);
			}
		}
	}
}
