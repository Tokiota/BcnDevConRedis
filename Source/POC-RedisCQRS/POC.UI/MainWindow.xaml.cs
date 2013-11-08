using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

using POC.Data;
using POC.Data.Models;
using POC.Messages;
using POC.ReadModel;

namespace POC.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        readonly ReadModelAccess readModelAccess;
        readonly DispatcherTimer myDispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();

            readModelAccess = new ReadModelAccess();
            myDispatcherTimer = new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 0, 10)
                };
            myDispatcherTimer.Tick += EachTick;
            myDispatcherTimer.Start();
            
        }

        private void EachTick(object sender, EventArgs e)
        {
            var stopwatch = Stopwatch.StartNew();
            var data = readModelAccess.GetAllProductLineItems().OrderBy(c => c.CategoryName);
            
            var findTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            Datos.ItemsSource = data;

            var bindTime = stopwatch.ElapsedMilliseconds;

            Mensajes.Text += string.Format("f:{0} - b:{1} //", findTime, bindTime);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            myDispatcherTimer.Tick -= EachTick;
            base.OnClosing(e);
        }

        private void AgregarProducto(object sender, RoutedEventArgs e)
        {
            var categoria = new CategoriaRepository().GetOne(1);
            var product = new Product
                {
                    ProductName = "aaaaa",

                    CategoryID = 1,
                    UnitPrice = 50,
                    UnitsInStock = 300
                };

            var productRepository = new ProductRepository();
            productRepository.Add(product);

            var newProductCreated = new ProductCreated(product.ProductID,product.ProductName,product.UnitPrice,product.UnitsInStock,categoria.CategoryName);

            Publisher.Publish(Channels.ChannelNewProduct, newProductCreated);

        }
        
    }
}
