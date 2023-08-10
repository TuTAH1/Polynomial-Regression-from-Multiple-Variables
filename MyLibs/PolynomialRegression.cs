using System;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;


namespace Program
{
	/// <summary>
	/// <list type="table">
	///  <item>
	/// <term>source</term>
	/// <see href="https://gist.github.com/KvanTTT/3849678">Ivan Kochurkin</see>
	/// </item>
	/// <item>
	/// <term>edited by</term>
	/// <see href="https://github.com/TuTAH1">Титан</see>
	/// </item>
	/// </list>
	/// </summary>
	public class PolynomialRegression
	{
		private int _order; //: Порядок полинома

		private Vector<double> _coefs = null;

		public PolynomialRegression(Vector<double> coefs, int order)
		{
			_coefs = coefs;
			_order = order;
		}

		public PolynomialRegression(DenseVector xData, DenseVector yData, int order)
		{
			_order = order;
			int n = xData.Count;

			var vandMatrix = new DenseMatrix(n, order + 1);
			for (int i = 0; i < n; i++)
				vandMatrix.SetRow(i, VandermondeRow(xData[i]));

			_coefs = vandMatrix.TransposeThisAndMultiply(vandMatrix).LU().
				Solve(vandMatrix.TransposeThisAndMultiply(yData));
		}

		private Vector<double> VandermondeRow(double x)
		{
			double[] result = new double[_order + 1];
			double mult = 1;
			for (int i = 0; i <= _order; i++)
			{
				result[i] = mult;
				mult *= x;
			}
			return new DenseVector(result);
		}

		public double Fit(double x) => _coefs == null ? throw new Exception("Вычисление точки невозможно: коэффициенты не были вычислены. ") : VandermondeRow(x) * _coefs;

		public string GetCoefs()
		{
			string str = "";

			foreach (var num in _coefs)
			{
				str += num.ToString("G5") + ", ";
			}

			str = str[..^2];

			return str;
		}
	}
}