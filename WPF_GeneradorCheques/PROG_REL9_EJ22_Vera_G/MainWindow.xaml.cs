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
// ---------------
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace PROG_REL9_EJ22_Vera_G
{
	/// <summary>
	/// Lógica de interacción para MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static string cantidad = "##";
		public MainWindow()
		{
			InitializeComponent();
			ResetCampos();
		}

		private void btnReset_Click(object sender, RoutedEventArgs e)
		{
			ResetCampos();
		}

		private void ResetCampos()
		{
			tbkCantidad.Text = cantidad;
			tbkNombre.Text = "AL PORTADOR";
			tbxFecha.Text = DateTime.Now.ToShortDateString();
			tbkNombreCantidad.Text = "CERO DOLARES";
		}

		private void btnGenerar_Click(object sender, RoutedEventArgs e)
		{
			tbkCantidad.Text = tbkCantidad.Text.ToUpper();
			tbkNombre.Text = tbkNombre.Text.ToUpper();
			tbkNombreCantidad.Text = tbkNombreCantidad.Text.ToUpper();



			// Captura de pantalla completa
			//System.Drawing.Rectangle region = Screen.AllScreens[0].Bounds;
			//Bitmap bitmap = new Bitmap(region.Width, region.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

			//Graphics graphic = Graphics.FromImage(bitmap);
			//graphic.CopyFromScreen(region.Left, region.Top, 0, 0, region.Size);
			//bitmap.Save(@"C:\basura\pantalla.png", ImageFormat.Png);

			int contador = 1;
			string nombreCaptura = "cheque_" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
			string ruta = @"C:\basura\" + nombreCaptura + ".png";

			string nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(ruta);
			string extension = System.IO.Path.GetExtension(ruta);
			string rutaTMP = System.IO.Path.GetDirectoryName(ruta);
			string nuevaRutaCompleta = ruta;

			while (File.Exists(nuevaRutaCompleta))
			{
				string tempFileName = string.Format("{0}_({1})", nombreArchivo, contador++);
				nuevaRutaCompleta = System.IO.Path.Combine(rutaTMP, tempFileName + extension);
			}

			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(784, 340, 96, 96, PixelFormats.Pbgra32);
			renderTargetBitmap.Render(grdPanel);
			PngBitmapEncoder pngImage = new PngBitmapEncoder();
			pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

			using (Stream fileStream = File.Create(nuevaRutaCompleta))
			{
				pngImage.Save(fileStream);
			}
		}


	}
}
