using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra.Double;
using Titanium;

namespace Program
{
	public partial class MainForm : Form
	{
		
		public MainForm()
		{
			//CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
			InitializeComponent();
			btnCalculateAll.Enabled = false;
			btnSave.Enabled = false;
			btnCalculateCell.Enabled = false;
			btnCalculatePolynom.Enabled = false;
			
			labelInfo.Text = "";
			dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgv1.AutoResizeColumns();
			panelColumnButtons.Controls.Add(new Button() {AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink});
			btnAddRow.Enabled = false;
			btnTrace.Enabled = false;
			dgv1.ColumnAdded+=Dgv1_ColumnAdded;
			dgv1.ColumnRemoved+=Dgv1_ColumnRemoved;
			dgv1.RowsAdded+=Dgv1_RowsAdded;
			dgv1.RowHeadersWidthChanged += ResizeButtons;
			dgv1.ColumnWidthChanged += ResizeButtons;
			ToolTip tip = new ToolTip();
			tip.SetToolTip(btnCalculatePolynom,"Вычисляет полином, необходимый для регрессионного анализа");
			
		}

		private void Dgv1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			dgv1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
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
			tbAddColumn.Text = "";
			tbAddColumn.Focus();
		}
		private void tbAddColumn_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != '\r') return;

			btnAddColumn.PerformClick();
		}

		private void Dgv1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
		{
			lock (this)
			{
				Button btn = new Button { BackColor = Color.DarkRed, ForeColor = Color.White };
				btn.Click += DeleteColumn_Click;
				ColumnButtons.Add(btn);
				panelColumnButtons.Controls.Add(btn);
			}
			if (dgv1.Columns.Count >= 2) btnCalculatePolynom.Enabled = true;
			_Polynoms = null;
			btnAddRow.Enabled = true;
		}

		void DeleteColumn_Click(object sender, EventArgs eArgs)
		{
			int Row = ColumnButtons.IndexOf(sender as Button);
			dgv1.Columns.RemoveAt(Row);
			ResizeButtons();
		}

		private void Dgv1_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
		{
			if (dgv1.Columns.Count < 2) {btnCalculatePolynom.Enabled = false;
				if (dgv1.ColumnCount < 1) btnAddRow.Enabled = false;}
			_Polynoms = null;
			ColumnButtons[e.Column.Index].Dispose();
			ColumnButtons.RemoveAt(e.Column.Index);
		}

		private void btnAddRow_Click(object sender, EventArgs e)
		{
			var RowName = tbAddRow.Text;
			dgv1.Rows.Add(1);
			dgv1.Rows[^1].HeaderCell.Value = RowName;
			tbAddRow.Text = "";
			tbAddColumn.Focus();
		}

		private void tbAddRow_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != '\r') return;

			btnAddRow.PerformClick();
		}

		private PolynomialRegression[,] ___polynoms;
		private PolynomialRegression[,] _Polynoms
		{
			get
			{
				return ___polynoms;
			}
			set
			{
				___polynoms = value;
				if (value == null)
				{
					btnCalculatePolynom.Text = "Вычислить полином";
					btnTrace.Enabled = btnCalculateCell.Enabled = btnCalculateAll.Enabled = false;
					
				}
				else
				{
					btnCalculatePolynom.Text = "Пересчитать полином";
					btnTrace.Enabled = btnCalculateCell.Enabled = btnCalculateAll.Enabled = true;
				}
			}
		}

		private void btnCalculatePolynom_Click(object sender, EventArgs eArgs)
		{
#if !DEBUG
			try
			{
#endif
				int columnsCount = dgv1.Columns.Count;
				if (tbOrder.Text == "") throw new ApplicationException("Введите порядок перед вычислением");
				int order = tbOrder.Text.ToIntT(false, -1);
				if (order == -1) throw new ApplicationException("Порядок должно быть числом");
				tbOrder.Text = order.ToString();
				labelInfo.Text = "Идёт вычисление полинома";

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
								temp = dgv1[j, i].Value.GetDouble(); //% Бутылочное горлышко
							}                                                   // FIXED (вроде бы)
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

				_Polynoms = new PolynomialRegression[dgv1.Columns.Count,columnsCount]; //: Кубик зависимостей [известный столбец, неизвестный столбец], диагональ пуста

				for (int i = 0; i < columnsCount; i++)
				{
					for (int j = 0; j < columnsCount; j++)
					{
						if (i==j) continue;
						_Polynoms[i, j] = new PolynomialRegression(DenseVector.OfArray(benchmarkColumns[i]), DenseVector.OfArray(benchmarkColumns[j]), order);
					}
				}

				labelInfo.Text = "Полином вычислен!";
#if !DEBUG
			}
			catch (ApplicationException ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
#endif
		}

		private void btnCalculateCell_Click(object sender, EventArgs e)
		{
			var point = dgv1.CurrentCellAddress;
			CalculateCell(point);
			btnSave.Enabled = true;

		}

		private void CalculateCell(Point CellPosition)
		{
			int calculationsCount = 0;
			double result = 0;
			for (int i = 0; i < dgv1.Columns.Count; i++)
			{
				try
				{
					result += _Polynoms[i, CellPosition.X].Fit(dgv1[i, CellPosition.Y].Value.GetDouble());
				}
				catch (Exception)
				{
					continue;
				}

				calculationsCount++;
			}

			dgv1[CellPosition.X, CellPosition.Y].Value = result / calculationsCount; //: Среднее арифметическое из всех вычисленных предположений
		}

		private void btnCalculateAll_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in dgv1.Rows)
			{
				foreach (DataGridViewCell cell in row.Cells)
				{
					if (!double.IsNaN(cell.Value.GetDouble())) continue; //: Если ячейка не пуста, то пропустить
					
					CalculateCell(new Point(cell.ColumnIndex, cell.RowIndex)); //: Иначе, вычислить значение
				}
			}
		}

		private void btnTrace_Click(object sender, EventArgs e)
		{
			string[] columnNames = new string[dgv1.ColumnCount];

			for (int i = 0; i < columnNames.Length; i++)
			{
				columnNames[i] = dgv1.Columns[i].Name;
			}

			var form = new TraceForm(_Polynoms, columnNames);
			form.Show();
		}

		private void EnabledChanged(object sender, System.EventArgs e)
		{
			var btn = sender as Button;
			if (!btn.Enabled)
			{
				DefaultColors.TryAdd(btn, (btn.BackColor, btn.ForeColor));
			}

			btn.BackColor = btn.Enabled ? DefaultColors[btn].Background : DisabledBackColor;
			btn.ForeColor = btn.Enabled ? DefaultColors[btn].Foreground : DisabledForeColor;
		}

		static Color DisabledBackColor = ColorTranslator.FromHtml("#CCCCCC");
		static Color DisabledForeColor = ColorTranslator.FromHtml("#a0a0a0");
		static Dictionary<Button, (Color Background, Color Foreground)> DefaultColors = new();

		private void ButtonPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			/*var btn = (Button)sender;
			var text = btn.Text;
			btn.Text = string.Empty;
			TextRenderer.DrawText(e.Graphics,text,btn.Font, new Point(, 12),btn.ForeColor);*/
			
			base.OnPaint(e);
		}

		#endregion

		#region Интерфейсные функции

		private void loadFile(string[] fileLines)  //:27.10.2022 Теперь он закидывает в dgv Double вместо String
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
					dgv1.Rows.Add();

					var t = rows[1..].ToDoubleT('.');
					for (var i = 0; i < t.Length; i++)
					{
						var num = t[i];
						dgv1.Rows[^1].Cells[i].Value = double.IsNaN(num)? "" : num;
					}

					dgv1.Rows[^1].HeaderCell.Value = rows[0];
				}

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
					for (int i = 0; i < dgv1.Columns.Count; i++)  //: Заголовки столбцов
					{
						sw.Write(dgv1.Columns[i].HeaderText + (i== dgv1.Columns.Count-1? "": '\t'));
					}

					sw.WriteLine();

					for (int i = 0; i < dgv1.Rows.Count; i++)
					{
						sw.Write(dgv1.Rows[i].HeaderCell.Value.ToString() + '\t'); //: Заголовок строки
						for (int j = 0; j < dgv1.Rows[i].Cells.Count; j++)
						{
							var value = dgv1.Rows[i].Cells[j].Value;
							sw.Write(value is double d? d.ToString() : value.ToString() + (j==dgv1.Rows[i].Cells.Count-1? '\n':'\t')); //: Значение ячейки
						} //TODO: Модифицировать ToStringT(), чтобы гарантировано записывал только точки
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

		void ResizeButtons(object _ = null, EventArgs __ = null)
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
				ColumnButtons[i].Size = new Size(size, panelColumnButtons.Size.Height);
				ColumnLeft += dgv1.Columns[i].Width;
			}
		}

		#endregion

		private void dgv1_ColumnAdded_1(object sender, DataGridViewColumnEventArgs e)
		{
			e.Column.DefaultCellStyle.FormatProvider = CultureInfo.InvariantCulture;
		}

		private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex<0||e.RowIndex<0) return; //: WTF index<0???
			var cellValue = dgv1[e.ColumnIndex, e.RowIndex].Value;
			var cellStringValue = cellValue.ToString();
			if (string.IsNullOrEmpty(cellStringValue)) return;

			int slicePosition = cellStringValue.Length;
			for (int i = cellStringValue.Length-1; i >=0 ; i++)
			{
				if (cellValue.ToString()[i].IsDoubleT()) break;
			}
			if (slicePosition != cellStringValue.Length) //:if cellStringValue changed
				dgv1[e.ColumnIndex, e.RowIndex].Value = cellStringValue[..slicePosition];

		}
	}
	public static class MyFuncs
	{
		public static double GetDouble(this object obj) => obj is double value ? value : obj.ToString().ToDoubleT();

		public static void Clear(this DataGridView dgv)
		{
			dgv.Rows.Clear();  // удаление всех строк
			int count = dgv.Columns.Count;
			for (int i = 0; i < count; i++)     // цикл удаления всех столбцов
			{
				dgv.Columns.RemoveAt(0);
			}
		}

		/*static Color DisabledBackColor = ColorTranslator.FromHtml("#CCCCCC");
		static Color DisabledForeColor = ColorTranslator.FromHtml("#a0a0a0");
		private static Dictionary<Button, (Color Background, Color Foreground)> DefaultColors = new();
		

		public static void ChangeDisableState(this Button btn, bool Enable)
		{
			if (btn.Enabled && !Enable) //: if enabled state is changes from true to false
			{
				DefaultColors.TryAdd(btn, (btn.BackColor, btn.ForeColor));
			}
			btn.Enabled = true;
			btn.BackColor = Enable ? DefaultColors[btn].Background : DisabledBackColor;
			btn.ForeColor = Enable ? DefaultColors[btn].Foreground : DisabledForeColor;
			btn.Enabled = Enable;
		}

		public static void Enable(this Button btn) => btn.ChangeDisableState(true);
		public static void Disable(this Button btn) => btn.ChangeDisableState(false);*/
	}
}
