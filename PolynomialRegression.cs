using System;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualBasic.CompilerServices;

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
		
		_coefs = vandMatrix.TransposeThisAndMultiply(vandMatrix).LU().Solve(TransposeAndMult(vandMatrix, yData));
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

	private static DenseVector TransposeAndMult(Matrix m, Vector v)
	{
		var result = new DenseVector(m.ColumnCount);
		for (int j = 0; j < m.RowCount; j++)
		for (int i = 0; i < m.ColumnCount; i++)
			result[i] += m[j, i] * v[j];
		return result;
	}

	public double Fit(double x)
	{
		if (_coefs == null) throw new Exception("Вычисление точки невозможно: коэффициенты не были вычислены. ");
		return VandermondeRow(x) * _coefs;
	}
}