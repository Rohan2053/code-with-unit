using System.Numerics;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _welcome = String.Empty;
        private string _productInfo = String.Empty;
        private string _shippingAssistants = String.Empty;
        private string _orderTracking = String.Empty;
        private string _search = String.Empty;
        private string _help = String.Empty;

        public string Welcome
        {
            get => _welcome;
            set => _welcome = value;
        }

        public string ProductInfo
        {
            get => _productInfo;
            set => _productInfo = value;
        }

        public string ShippingAssistants
        {
            get => _shippingAssistants;
            set => _shippingAssistants = value;
        }

        public string OrderTracking
        {
            get => _orderTracking;
            set => _orderTracking = value;
        }

        public string Search
        {
            get => _search;
            set => _search = value;
        }

        public string Help
        {
            get => _help;
            set => _help = value;
        }

        public void Save()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
                    UPDATE orders
                    SET welcome = $welcome,
                        product_info = $productInfo,
                        shipping_assistants = $shippingAssistants,
                        order_tracking = $orderTracking,
                        search = $search,
                        help = $help
                    WHERE welcome = $welcome
                ";
                commandUpdate.Parameters.AddWithValue("$welcome", Welcome);
                commandUpdate.Parameters.AddWithValue("$productInfo", ProductInfo);
                commandUpdate.Parameters.AddWithValue("$shippingAssistants", ShippingAssistants);
                commandUpdate.Parameters.AddWithValue("$orderTracking", OrderTracking);
                commandUpdate.Parameters.AddWithValue("$search", Search);
                commandUpdate.Parameters.AddWithValue("$help", Help);
               
                int nRows = commandUpdate.ExecuteNonQuery();

                if (nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
                        INSERT INTO orders(welcome, product_info, shipping_assistants, order_tracking, search, help)
                        VALUES($welcome, $productInfo, $shippingAssistants, $orderTracking, $search, $help)
                    ";
                    commandInsert.Parameters.AddWithValue("$welcome", Welcome);
                    commandInsert.Parameters.AddWithValue("$productInfo", ProductInfo);
                    commandInsert.Parameters.AddWithValue("$shippingAssistants", ShippingAssistants);
                    commandInsert.Parameters.AddWithValue("$orderTracking", OrderTracking);
                    commandInsert.Parameters.AddWithValue("$search", Search);
                    commandInsert.Parameters.AddWithValue("$help", Help);
                   
                    int nRowsInserted = commandInsert.ExecuteNonQuery();
                }
            }
        }

 
    }
}
