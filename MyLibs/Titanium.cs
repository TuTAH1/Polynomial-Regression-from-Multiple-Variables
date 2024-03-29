﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Titanium
{
	/// <summary>
	/// Just my library of small functions that makes c# programming easier. The automatization of automatization instrument
	/// <para> Despite of the program license, THIS file is <see href="https://creativecommons.org/licenses/by-sa/4.0">CC BY-SA</see></para>
	/// <list type="table">
	/// <item>
	/// <term>Author</term>
	/// <see href="https://github.com/TuTAH1">Титан</see>
	/// </item>
	/// </list>
	/// </summary>
	public static class TypesFuncs
	{ //!03.10.2021 legacy


		#region Parsing

		#region IsType
		public static bool IsDigit(this char c) => c >= '0' && c <= '9';

		public static bool IsDoubleT(this char c) => c >= '0' && c <= '9' || c == '.' || c == ',' || c == '-';
		#endregion

		#region ToType
		#region Int


		/// <summary>
		/// Преобразует число в char в Int
		/// </summary>
		/// <param name="str"></param>
		/// <returns>Число, если содержимое Char – число:
		/// -1 в противном случае</returns>
		public static int ToIntT(this char ch)
		{
			return ch >= '0' && ch <= '9' ?
				ch - '0' : -1;
		}

		/// <summary>
		/// "534 Убирает все лишние символы и читает Int. 04" => 53404
		/// </summary>
		/// <param name="str"></param>
		/// <param name="CanBeNegative">Может ли число быть отрицательным (если нет, то - будет игнорироваться, иначе он будет учитываться только если стоит рядом с числом)</param>
		/// <param name="ExceptionValue">Возвращаемое значение при исключении (если не указывается, то при исключении... вызывается исключение</param>
		/// <returns>Числа, если они содержаться в строке</returns>
		public static int ToIntT(this string str, bool CanBeNegative = true, int? ExceptionValue = null) //:08.10.2021 return behavior changed
		{
			int Int = 0;
			bool isContainsInt = false;
			bool negative = false;
			foreach (var ch in str)
			{
				if (ch == '-' && isContainsInt == false) negative = true;
				else if (ch != ' ') negative = false;
				if (ch.IsDigit())
				{
					isContainsInt = true;
					Int = Int * 10 + ch.ToIntT();
				}
			}

			if (!isContainsInt)
			{
				if (ExceptionValue == null)
				{
					throw new ArgumentException("Строка не содержит числа");
				}
				else return (int)ExceptionValue;
			}


			return negative ? -Int : Int;
		}

		public static long ToLongT(this string str, bool CanBeNegative = true, long? ExceptionValue = null) //:08.10.2021 return behavior changed
		{
			long Int = 0;
			bool isContainsInt = false;
			bool negative = false;
			foreach (var ch in str)
			{
				if (ch == '-' && isContainsInt == false) negative = true;
				else if (ch != ' ') negative = false;
				if (ch.IsDigit())
				{
					isContainsInt = true;
					Int = Int * 10 + ch.ToIntT();
				}
			}

			if (!isContainsInt)
			{
				if (ExceptionValue == null)
				{
					throw new ArgumentException("Строка не содержит числа");
				}
				else return (long)ExceptionValue;
			}


			return negative ? -Int : Int;
		}

		public static int? ToNIntT(this string str, bool CanBeNegative = true) //:08.10.2021 new func
		{
			try
			{
				return str.ToIntT(CanBeNegative);
			}
			catch (Exception)
			{
				return null;
			}
		}


		/// <summary>
		/// Searches for INT in string. Throws IndexOutOfRangeException if no any int found in this string.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="thousandSeparator"></param>
		/// <returns>first INT number found in string</returns>
		public static int FindInt(this string str, char thousandSeparator = ' ')
		{
			int end = 0, start = 0;
			bool minus = false;
			while (!str[start].IsDigit())
			{
				minus = str[start] == '-';
				start++;
			}

			end = minus ? start - 1 : start;

			do
			{
				end++;
			} while ((str[end].IsDigit() || str[end] == thousandSeparator) && end < str.Length - 1);

			return Convert.ToInt32(str.Substring(start, end - start).Replace(thousandSeparator.ToString(), ""), new CultureInfo("en-US", true));
		}
		public static int FindIntBetween(this string MainStr, string LeftStr, string RightStr = null)
		{
			if (RightStr == null) RightStr = LeftStr;
			int right = MainStr.IndexOf(RightStr),
				left = MainStr.LastIndexOf(LeftStr, 0, right - 1);
			int number;

			do
			{ //TODO: Сначала искать IndexOf Right, потом LastIndexOf Left, 
				if (int.TryParse(MainStr.Substring(left + 1, right - (left + 1)), out number)) return number;
				right = MainStr.IndexOf(RightStr);
				left = MainStr.LastIndexOf(LeftStr, left, right - left - 1);
			} while (left > 0 && right > 0);

			return -1;
		}

		#endregion

		#region String

		/// <summary>
		/// Int to subscript numbers string
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		public static string ToIndex(this int number)
		{
			string num = number.ToString();
			string index = "";

			for (int i = 0; i < num.Length; i++)
			{
				if (num[i] == '1') index += '₁';
				if (num[i] == '2') index += '₂';
				if (num[i] == '3') index += '₃';
				if (num[i] == '4') index += '₄';
				if (num[i] == '5') index += '₅';
				if (num[i] == '6') index += '₆';
				if (num[i] == '7') index += '₇';
				if (num[i] == '8') index += '₈';
				if (num[i] == '9') index += '₉';
				if (num[i] == '0') index += '₀';
			}
			return index;
		}

		public static string ToVisibleString(this string s) //!03.10.2021
		{
			string a = "";
			foreach (char c in s)
			{
				if (!char.IsControl(c)) a += c;
			}

			return a;
		}


		/// <summary>
		/// Преобразовывает string в double, обрезая дробную часть до момента, когда начинаются нули
		/// </summary>
		/// <param name="Double"></param>
		/// <param name="Accuracy"></param>
		/// <returns></returns>
		public static string ToStringT(this double Double, int Accuracy = 3) //:10.10.2021 Created
		{
			var String = Double.ToString(CultureInfo.InvariantCulture); //: Пока я не умею преобразовывать double в String с минимально возможными затратами самостоятельно
			var temp = String.Split('.');
			if (temp.Length == 1)
			{
				return temp[0];
			}
			else
			{
				string intPart = temp[0], decPart = temp[1];
				int superfluity = decPart.IndexOf('0'.Multiply(Accuracy)); //TODO: вместо нулей можно пробегать по строке, пока символ не начнет повторятся несколько раз
				if (superfluity == -1) return String;

				decPart = decPart.Slice(0, superfluity);
				return intPart + "." + decPart;
			}

		}



		#endregion

		#region Double

		public static double[] ToDoubleT(this IEnumerable<string> StringArray, char? Separator = null, bool CanBeShortcuted = true, bool DotShouldBeAttachedToNumber = true, bool ThrowException = false)
		{
			double[] res = new double[StringArray.Count()];
			int i = 0;
			foreach (var s in StringArray)
			{
				res[i++] = s.ToDoubleT(Separator, CanBeShortcuted, DotShouldBeAttachedToNumber, ThrowException);
			}

			return res;
		}

		/// <summary>
		/// "534 Убирает все лишние символы и читает double. 0.4" => 5340.4
		/// </summary>
		/// <param name="String"></param>
		/// <param name="Separator">Разделитель целой и дробной части</param>
		/// <param name="CanBeShortcuted">Может ли нуль целой части быть опущен (".23" вместо "0.23")</param>
		/// <returns></returns>
		public static double ToDoubleT(this string String, char? Separator = null, bool CanBeShortcuted = true, bool DotShouldBeAttachedToNumber = true, bool ThrowException = true) //:06.10.2021 behavior changed a bit //:27.10.2022 Теперь разделитель по умолчанию зависит от культуры
		{
			double Double = 0;
			bool? IntPart = true;
			int FractionalPart = -1;
			bool isContainsDouble = false;
			Separator ??= CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
			foreach (var ch in String)
			{
				if (IntPart == true)
				{
					if (ch.IsDigit())
					{
						Double = Double * 10 + ch.ToIntT();
						isContainsDouble = true;
					}
					else if (ch == Separator) IntPart = null; //: Состояние квантовой запутанности. Целая часть и закончилась или нет одновременно. Ну а на самом деле просто чтобы не парсил точку или запятую в предложении как разделитель
				}
				else
				{
					if (ch.IsDigit())
					{
						IntPart = false;
						//:на случай строки вроде ".23" (=="0.23")
						if (!isContainsDouble && !CanBeShortcuted)
						{
							Double = Double * 10 + ch.ToIntT();
							isContainsDouble = true;
							IntPart = true;
							continue;
						}
						Double += ch.ToIntT() * Math.Pow(10, FractionalPart--);
					}
					else if (IntPart == null && DotShouldBeAttachedToNumber) IntPart = true;
				}



			}

			if (!isContainsDouble)
			{
				if (ThrowException == true)
				{
					throw new ArgumentException("Строка не содержит числа");
				}
				else return double.NaN;
			}
			return Double;
		}

		public static double ToDoubleT(this string String, char[] Separators, bool CanBeShortcuted = true, bool DotShouldBeAttachedToNumber = true, bool ThrowException = true) //:27.10.2022 New old func
		{
			double Double = 0;
			bool? IntPart = true;
			int FractionalPart = -1;
			bool isContainsDouble = false;
			Separators ??= new []{ CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0] };
			foreach (var ch in String) 
			{
				if (IntPart == true)
				{
					if (ch.IsDigit())
					{
						Double = Double * 10 + ch.ToIntT();
						isContainsDouble = true;
					}
					else if (Separators.Contains(ch)) IntPart = null; //: Состояние квантовой запутанности. Целая часть и закончилась или нет одновременно. Ну а на самом деле просто чтобы не парсил точку или запятую в предложении как разделитель
				}
				else
				{
					if (ch.IsDigit())
					{
						IntPart = false;
						//:на случай строки вроде ".23" (=="0.23")
						if (!isContainsDouble && !CanBeShortcuted)
						{
							Double = Double * 10 + ch.ToIntT();
							isContainsDouble = true;
							IntPart = true;
							continue;
						}
						Double += ch.ToIntT() * Math.Pow(10, FractionalPart--);
					}
					else if (IntPart == null && DotShouldBeAttachedToNumber) IntPart = true;
				}



			}

			if (!isContainsDouble)
			{
				if (ThrowException == true)
				{
					throw new ArgumentException("Строка не содержит числа");
				}
				else return double.NaN;
			}
			return Double;
		}

		public static double? ToNDoubleT(this string str, char Separator = '.', bool CanBeShortcuted = true, bool DotShouldBeAttachedToNumber = true) //:08.10.2021 new func
		{
			try
			{
				return str.ToDoubleT(Separator, CanBeShortcuted, DotShouldBeAttachedToNumber);
			}
			catch (Exception)
			{
				return null;
			}
		}


		#endregion

		#region Float

		public static float FindFloat(this string str, char DecimalSeparator = '.', char thousandSeparator = ' ')
		{
			int start = 0;
			bool minus = false;
			while (!str[start].IsDigit())
			{
				minus = str[start] == '-';
				start++;
			}

			int end = minus ? start - 1 : start;
			do
			{
				end++;
			} while ((str[end].IsDigit() || str[end] == thousandSeparator || str[end] == DecimalSeparator) && end < str.Length - 1);

			return Convert.ToSingle(str.Substring(start, end - start).Replace(DecimalSeparator, '.').Replace(thousandSeparator.ToString(), ""), new CultureInfo("en-US", true));
		}


		#endregion

		#region Short

		public static short ReadShortUntilLetter(this string str)
		{
			short Short = 0;
			foreach (var ch in str)
			{
				if (ch.IsDigit())
				{
					Short = (short)(Short * 10 + (short)ch);
				}
				else break;
			}

			return Short;
		}

		#endregion

		#region Bool

		public static bool ToBool(this string S)
		{
			return S.ToLower() is "true" or "yes" or "да" or "1";
		}

		public static string ToRuString(this bool Bool)
		{
			return Bool ? "Да" : "Нет";
		}

		public static bool RandBool(int TrueProbability)
		{
			Random rand = new Random((int)DateTime.Now.Ticks);
			return rand.Next() <= TrueProbability;
		}

		#endregion

		#region Array

		public static double[,] ToDoubleT(this string[,] Strings, char Separator = '.')
		{
			int d0 = Strings.GetLength(0), d1 = Strings.GetLength(1);
			double[,] doubles = new double[d0, d1];
			for (int i = 0; i < d0; i++)
			{
				for (int j = 0; j < d1; j++)
				{
					doubles[i, j] = Strings[i, j].ToDoubleT(Separator);
				}
			}

			return doubles;
		}

		public static string[,] ToStringT(this double[,] Doubles, string Format)
		{
			int d0 = Doubles.GetLength(0), d1 = Doubles.GetLength(1);
			string[,] strings = new string[d0, d1];
			for (int i = 0; i < d0; i++)
			{
				for (int j = 0; j < d1; j++)
				{
					strings[i, j] = Doubles[i, j].ToString(Format);
				}
			}

			return strings;
		}

		public static string[,] ToStringMatrix(this double[,] Doubles)
		{
			int d0 = Doubles.GetLength(0), d1 = Doubles.GetLength(1);
			string[,] strings = new string[d0, d1];
			for (int i = 0; i < d0; i++)
			{
				for (int j = 0; j < d1; j++)
				{
					strings[i, j] = Doubles[i, j].ToString();
				}
			}

			return strings;
		}

		#endregion

		#region Other

		/// <summary>
		/// Reads String and parses to Short until meets a letter
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>

		public static string ToHex(this byte[] bytes)
		{
			char[] c = new char[bytes.Length * 2];

			byte b;

			for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
			{
				b = (byte)(bytes[bx] >> 4);
				c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

				b = (byte)(bytes[bx] & 0x0F);
				c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
			}

			return new string(c);
		}

		#endregion
		#endregion

		#region Array

		public static int GetMaxIndex(this double[] array)
		{
			int MaxIndex = 0;
			double MaxValue = array[0];

			for (int i = 1; i < array.Length; i++)
			{
				if (array[i] > MaxValue)
				{
					MaxValue = array[i];
					MaxIndex = i;
				}
			}

			return MaxIndex;
		}

		#endregion

		#region List

		/// <summary>
		/// Случайным образом перемешивает массив
		/// </summary>
		public static List<T> RandomShuffle<T>(this IEnumerable<T> list)
		{
			Random random = new Random();
			var shuffle = new List<T>(list);
			for (var i = shuffle.Count() - 1; i >= 1; i--)
			{
				int j = random.Next(i + 1);

				var tmp = shuffle[j];
				shuffle[j] = shuffle[i];
				shuffle[i] = tmp;
			}
			return shuffle;
		}

		public static List<T> RandomShuffle<T>(this IEnumerable<T> list, Random random)
		{
			var shuffle = new List<T>(list);
			for (var i = shuffle.Count() - 1; i >= 1; i--)
			{
				int j = random.Next(i + 1);

				var tmp = shuffle[j];
				shuffle[j] = shuffle[i];
				shuffle[i] = tmp;
			}
			return shuffle;
		}

		public static List<int> RandomList(int start, int count)
		{
			List<int> List = new List<int>(count);
			List<bool> Empty = new List<bool>();
			for (int i = 0; i < count; i++)
			{
				List.Add(0);
				Empty.Add(true);
			}
			Random Random = new Random();

			int End = start + count;
			for (int i = start; i < End;)
			{
				int Index = Random.Next(0, count); //C#-повский рандом гавно. Надо заменить чем-то

				if (Empty[Index])
				{
					List[Index] = i;
					Empty[Index] = false;
					i++;

				}
			}

			return List;
		}

		public static T Pop<T>(this List<T> list)
		{
			T r = list[^1];
			list.RemoveAt(list.Count - 1);
			return r;
		}

		public static void Swap<T>(this List<T> list, int aIndex, int bIndex)
		{
			T value = list[aIndex];
			list[aIndex] = list[bIndex];
			list[bIndex] = value;
		}

		public static void Swap<T>(this T[] list, int aIndex, int bIndex)
		{
			T value = list[aIndex];
			list[aIndex] = list[bIndex];
			list[bIndex] = value;
		}

		#endregion

		#endregion


		#region OtherTypeFuncs

		#region String

		public static int SymbolsCount(this string s)
		{
			int i = s.Length;
			foreach (var c in s)
			{
				if (char.IsControl(c)) i--;
			}

			return i;
		}

		public static string FormatToString(this double d, int n, Positon pos, char filler = ' ') //:Ленивый и неоптимизированный способ
		{
			d = Math.Round(d, n);
			string s = d.ToString();
			if (s.Length < n)
			{
				switch (pos)
				{
					case Positon.left:
						{
							for (int i = s.Length; i < n; i++)
							{
								s += filler;
							}
						}
						break;
					case Positon.center:
						{
							int halfString = (n - s.Length) / 2;
							for (int i = 0; i < halfString; i++)
							{
								s = s.Insert(0, filler.ToString());
							}
							for (int i = s.Length; i < n; i++)
							{
								s += filler;
							}
						}
						break;
					case Positon.right:
						{
							for (int i = 0; i < n - s.Length; i++)
							{
								s = s.Insert(0, filler.ToString());
							}
						}
						break;
				}
			}
			else if (s.Length > n)
			{
				int Eindex = s.LastIndexOf('E');

				if (Eindex > 0)
				{ //если в строке есть Е+хх
					string E = s.TrimStart('E');
					s = s.Substring(0, n - E.Length);
					s += E;
				}
				else
				{
					s = s.Substring(0, n);
				}
			}
			return s;
		}

		public static string FormatString(this string s, int n, Positon pos, char filler = ' ')
		{
			if (s.Length < n)
			{
				switch (pos)
				{
					case Positon.left:
						{
							for (int i = s.Length; i < n; i++)
							{
								s += filler;
							}
						}
						break;
					case Positon.center:
						{
							int halfString = (n - s.Length) / 2;
							for (int i = 0; i < halfString; i++)
							{
								s = s.Insert(0, filler.ToString());
							}
							for (int i = s.Length; i < n; i++)
							{
								s += filler;
							}
						}
						break;
					case Positon.right:
						{
							for (int i = 0; i < n - s.Length; i++)
							{
								s = s.Insert(0, filler.ToString());
							}
						}
						break;
				}
			}
			else if (s.Length > n)
			{
				int Eindex = s.LastIndexOf('E');

				if (Eindex > 0)
				{ //если в строке есть Е+хх
					string E = s.TrimStart('E');
					s = s.Substring(0, n - E.Length);
					s += E;
				}
				else
				{
					s = s.Substring(0, n);
				}
			}
			return s;
		}

		public enum Positon : byte { left, center, right }

		public static string Slice(this string s, string Start, string End)
		{
			var start = s.IndexOfEnd(Start);
			if (start < 0) return null;

			var end = s.LastIndexOf(End);
			if (end < 0) return null;

			if (start > end) throw new ArgumentException($"start ({start}) is be bigger than end ({end})");

			return s.Slice(start, end);

		}

		public static string Slice(this string s, int Start, string End)
		{
			var end = s.LastIndexOf(End);
			if (end < 0) return null;

			if (Start > end) throw new ArgumentException($"start ({Start}) is be bigger than end ({end})");

			return s.Slice(Start, end);

		}

		public static string Slice(this string s, string Start, int End = int.MaxValue)
		{
			var start = s.IndexOfEnd(Start);
			if (start < 0) return null;

			if (start > End) throw new ArgumentException($"start ({start}) is be bigger than end ({End})");

			return s.Slice(start, End);

		}

		/// <summary>
		/// Slices the string form Start to End not including End
		/// </summary>
		/// <param name="s"></param>
		/// <param name="Start"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public static string Slice(this string s, int Start = 0, int End = int.MaxValue)
		{
			if (Start < 0) throw new ArgumentOutOfRangeException($"Start ({Start}) can't be negative");
			if (Start > End) throw new ArgumentException($"start ({Start}) is be bigger than end ({End})");
			if (End == int.MaxValue) End = s.Length;
			return s.Substring(Start, End - Start);
		}

		public static int IndexOfEnd(this string s, string s2)
		{
			if (s == null)
				if (s2.Length == 0) return 0;
			int i = s.IndexOf(s2);
			if (i == -1) return -1;
			else return i + s2.Length;
		}

		public static string Multiply(this string s, int a)
		{
			StringBuilder sb = new StringBuilder(s.Length * a);
			for (int i = 0; i < a; i++)
			{
				sb.Append(s);
			}

			return sb.ToString();
		}

		public static string Multiply(this char s, int a)
		{
			StringBuilder sb = new StringBuilder(a);
			for (int i = 0; i < a; i++)
			{
				sb.Append(s);
			}

			return sb.ToString();
		}

		#endregion

		#region Double

		public static bool isEven(this int number) => number % 2 == 0;

		#endregion

		#region Other

		public static void Swap<T>(ref T a, ref T b)
		{
			T c = a;
			a = b;
			b = c;
		}

		#endregion

		#endregion

	}
}
