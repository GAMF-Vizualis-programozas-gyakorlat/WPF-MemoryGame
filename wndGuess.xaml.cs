using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MemoryGame
{
	/// <summary>
	/// Interaction logic for wndGuess.xaml
	/// </summary>
	public partial class wndGuess : Window
	{
		public wndGuess()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Lehetővé teszi a párbeszédablakban megjelenített kép beállítását.
		/// </summary>
		public BitmapImage biImage
		{
			set { imMain.Source = value; }
		}
		/// <summary>
		/// -1 -es értéket ad vissza, amennyiben a felhasználó nem választott egyetlen 
		/// RadioButton-t se az első csoportból, egyéb esetben pedig visszaadja a 
		/// kiválasztott RadioButton sorszámát (0..7).
		/// </summary>
		public int FirstNo
		{
			get
			{
				int no = -1;
				for (int i = 0; i < wpFirst.Children.Count; i++)
				{
					if ((wpFirst.Children[i] as RadioButton)?.IsChecked == true)
					{
						no = i;
					}
				}
				return no;
			}
		}
		/// <summary>
		/// -1 -es értéket ad vissza, amennyiben a felhasználó nem választott egyetlen 
		/// RadioButton-t se a második csoportból, egyéb esetben pedig visszaadja a 
		/// kiválasztott RadioButton sorszámát (0..7).
		/// </summary>
		public int SecondNo
		{
			get
			{
				int no = -1;
				for (int i = 0; i < wpSecond.Children.Count; i++)
				{
					if ((wpSecond.Children[i] as RadioButton)?.IsChecked == true)
					{
						no = i;
					}
				}
				return no;
			}
		}


		/// <summary>
		/// OK gombon történő kattintás esetén megviszgálja, hogy van-e kiválasztott. 
		/// RadioButton Ha nincs, akkor hibaüzenettel választásra készteti a 
		/// felhasználót, ha van, akkor engedélyezi a párbeszédablak lezárását.
		/// </summary>
		private void btOK_Click(object sender, RoutedEventArgs e)
		{
			if (FirstNo != -1 && SecondNo != -1)
				DialogResult = true;
			else
				MessageBox.Show("Select first the two positions!");
		}

	}
}
