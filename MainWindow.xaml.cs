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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MemoryGame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		/// <summary>
		/// A hátlapként alkalmazott kép állomány neve.
		/// </summary>
		string CardBackName = "cardback.png";

		/// <summary>
		/// A játékban felhasznált mesefigurákat tartalmazó 
		/// kép állományok nevei.
		/// </summary>
		string[] ImageNames = { "grabowski.jpg", "kukori.jpg",
													"kuldonc.jpg", "vili.jpg"
												};

		/// <summary>
		/// A hátlapkép referenciáját tároló változó.
		/// </summary>
		BitmapImage biCardBack;

		/// <summary>
		/// Tömb a nyolc kép objektum referenciáinak tárolására.
		/// </summary>
		BitmapImage[] biImages = new BitmapImage[8];

		/// <summary>
		/// Tömb a mesefigurák megjelenítésére használt Image
		/// komponensek referenciáinak tárolására.
		/// </summary>
		Image[] imImages;

		/// <summary>
		/// Véletlenszámok előállítására szolgáló objektum.
		/// </summary>
		Random rnd = new Random();

		/// <summary>
		/// Időzítő a képek memorizálására adott idő követéséhez. 
		/// </summary>
		private DispatcherTimer dt;

		/// <summary>
		/// A főablak konstruktora. Létrehozza a képeket megjelenítő 
		/// komponensek tömbjét, és betölti a képeket a memóriába.
		/// </summary>
		public MainWindow()
		{
			// Komponensek létrehozása és inicializálása.
			InitializeComponent();
			// Képeket megjelenítő komponensek tömbjének létrehozása.
			imImages = new Image[]
			{
				im10,im11,im12,im13,
				im20,im21,im22,im23
			};
			// Időzítő objektum létrehozása, az időzítés beállítása 3 másodpercre
			dt = new DispatcherTimer
			{
				Interval = new TimeSpan(0, 0, 0, 0, 3000),
				IsEnabled = false
			};
			dt.Tick += dt_Tick;
			// Képek betöltése a memóriába.
			LoadImages();
			// Képek láthatóvá tétele.
			ShowImages();
			// Minden komponens a hátlapképet mutatja, ha letelt a beállított 
			// időintervallum.
			dt.Start();
		}

		/// <summary>
		/// Láthatóvá teszi a nyolc képet.
		/// </summary>
		private void ShowImages()
		{
			for (int i = 0; i < 8; i++)
			{
				imImages[i].Source = biImages[i];
			}
		}

		/// <summary>
		/// Az időzítés leteltével a hátlapképet helyezi el mind a nyolc kép komponensre,
		/// majd leállítja az időzítőt.
		/// </summary>
		private void dt_Tick(object sender, EventArgs e)
		{
			ShowCardBack();
			dt.Stop();
		}

		/// <summary>
		/// Mind a nyolc helyen a hátlapképet jeleníti meg.
		/// </summary>
		private void ShowCardBack()
		{
			for (int i = 0; i < 8; i++)
			{
				imImages[i].Source = biCardBack;
			}
		}

		/// <summary>
		/// Betölti memóriába a játékhoz használt képeket. A képek a 
		/// projekt gyökérkönyvtárában levő Kepek alkönyvtárában kell legyenek.
		/// </summary>
		private void LoadImages()
		{
			try
			{
				// Hátlapkép betöltése.
				biCardBack = new BitmapImage(new Uri(@"Images/" + CardBackName, UriKind.Relative));
				// A négy mesefigura kép betöltése.
				for (int i = 0; i < 4; i++)
				{
					biImages[i] = new BitmapImage(new Uri(@"Images/" + ImageNames[i],
						UriKind.Relative));
					// A másodpéldányokat beazonosító referenciák.
					biImages[i + 4] = biImages[i];
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Images cannot be found!",
					"Error", MessageBoxButton.OK,MessageBoxImage.Error);
			}
		}

		/// <summary>
		/// Kilép az alkalmazásból.
		/// </summary>
		/// <param name="sender">A Kilép menüpont objektum.</param>
		/// <param name="e"></param>
		private void miExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}


		/// <summary>
		/// A Kever menüpont hatására az induláskor automatikusan 
		/// betöltött négy pár képet véletlenszerű elrendezésben 
		/// megjeleníti. Várakozik, majd a képek helyett zöldre 
		/// festett téglalapokat jelenít meg (hatter.jpg). 
		/// </summary>
		/// <param name="sender">A menüpont objektum.</param>
		/// <param name="e"></param>
		private void miMix_Click(object sender, RoutedEventArgs e)
		{
			// Véletlen sorrend meghatározása.
			Randomize();
			// Képek láthatóvá tétele.
			ShowImages();
			// Várakozás az időzítőben beállított ideig, majd a hátlapképek megjelenítése.
			dt.Start();
		}

		/// <summary>
		/// Meghatározza a képek véletlenszerű sorrendjét.
		/// </summary>
		private void Randomize()
		{
			// Létrehozunk egy lista objektumot a képek referenciáinak tárolássára.
			List<BitmapImage> ImageList = new List<BitmapImage>();
			// Mind a 8 kép referenciáját elhelyezzük a listában.
			ImageList.AddRange(biImages);
			// "Húzás a kalapból" véletlenszerűen kivesszük a nyolc referenciát.
			for (int i = 0; i < 8; i++)
			{
				// A maradék listából véletleszerűen kiválasztunk egy elemet.
				int no = rnd.Next(0, ImageList.Count);
				// Betesszük a tömb i. helyére
				biImages[i] = ImageList[no];
				// Eltávolítjuk a listáról.
				ImageList.RemoveAt(no);
			}
		}


		/// <summary>
		/// A Kérdez menüpont hatására egy új ablakot jelenít meg 
		/// Kérdés fejléccel. Az ablakban az előzőekben látható 
		/// négy képtípus valamelyike jelenik meg véletlenszerűen. 
		/// A felhasználó kiválasztja, hogy szerinte mely helyeken fordult elő a 
		/// főablakban a kép, majd kattint az OK gombon.
		/// Ekkor eltűnik a Kérdez ablak, és a válasz helyességétől 
		/// függően az alábbi két üzenetablak egyike jelenik meg, 
		/// majd újból láthatóvá válik a nyolc kép úgy, ahogy azt a 
		/// legelső ábrán láthatjuk.
		/// </summary>
		/// <param name="sender">A Kérdez menüpont objektum.</param>
		/// <param name="e"></param>
		private void miGuess_Click(object sender, RoutedEventArgs e)
		{
			// Ablak objektum létrehozása.
			var wndGuess = new wndGuess();
			// Kép sorszám kiválasztása véletlenszerűen.
			int no = rnd.Next(0, 8);
			// Keresett kép kiválasztása.
			BitmapImage biImage = biImages[no];
			// Kép komponenshez rendelése.
			wndGuess.biImage = biImage;
			// Megjeleníti a párbeszédablakot.
			if (wndGuess.ShowDialog() == true)
			{
				// Lekérdezi, hogy mely pozíciókat választotta ki a 
				// felhasználó, majd előállítja az adott pozícióban található
				// képek referenciáit.
				BitmapImage biFirst = biImages[wndGuess.FirstNo];
				BitmapImage biSecond = biImages[wndGuess.SecondNo];
				// Megvizsgálja, hogy a keresett kép azonos-e a két megjelölt 
				// képpel.
				if (biFirst == biImage && biSecond == biImage)
					MessageBox.Show("Correct!");
				else
					MessageBox.Show("Missed!");
				// Újból láthatóvá teszi a képeket.
				ShowImages();
			}
		}
	}
}
