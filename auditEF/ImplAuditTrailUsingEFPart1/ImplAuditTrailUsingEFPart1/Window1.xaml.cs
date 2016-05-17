using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Objects;
using System.Transactions;
using System.Data;

namespace ImplAuditTrailUsingEFPart1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int productId = 777; //Mountain-100 Black, 44
            short quantity = 1;
            string lastName = @"Adams";
            string firstName = @"Frances";
            string invoiceNumber = "PO123457";
            int shipMethod = 5;
            using (AdventureWorksEntities advWorksContext =
    new AdventureWorksEntities())
            {
                try
                {
                    // Get the Contact for the specific customer 
                    // and the related address. 
                    Contact customer = advWorksContext.Contact
                        .Include("SalesOrderHeader.Address")
                        .Where("it.LastName = @lastname",
                            new ObjectParameter("lastname", lastName))
                        .Where("it.FirstName = @firstname",
                            new ObjectParameter("firstname", firstName))
                        .First();

                    // Get the customer's address to use to create
                    // a new order with the same address.
                    Address address = customer.SalesOrderHeader
                        .First().Address;

                    // Get the Product with the requested ID.
                    Product product =
                        advWorksContext.Product.Where("it.ProductID = @product_id",
                        new ObjectParameter("product_id", productId)).First();


                    product.Name = "ProductNameChanged";
                    // Create a new SalesOrderHeader using the static 
                    // CreateSalesOrderHeader method.
                    SalesOrderHeader order = SalesOrderHeader.CreateSalesOrderHeader(0,
                        Convert.ToByte(1), DateTime.Now, DateTime.Today.AddMonths(2),
                        Convert.ToByte(1), false, string.Empty,
                        0, 0, 0, 0, Guid.NewGuid(), DateTime.Now);

                    order.ContactReference.EntityKey = advWorksContext.Contact.Where(c => c.ContactID == customer.ContactID).FirstOrDefault().EntityKey;
                    order.ShipMethodReference.EntityKey = advWorksContext.ShipMethod.Where( s => s.ShipMethodID == shipMethod).FirstOrDefault().EntityKey;
                    order.CustomerReference.EntityKey = advWorksContext.Customer.Where(c => c.CustomerID == customer.ContactID).FirstOrDefault().EntityKey;
                    // Set addition order properties.
                    order.Address = address;
                    order.Address1 = address;
                    order.PurchaseOrderNumber = invoiceNumber;

                    // Create a new SalesOrderDetail using the static 
                    // CreateSalesOrderDetail method.
                    SalesOrderDetail item = SalesOrderDetail.CreateSalesOrderDetail(
                        1, 0, quantity, product.StandardCost,
                        0, 0, Guid.NewGuid(), DateTime.Now);

                    item.SpecialOfferProductReference.EntityKey = advWorksContext.SpecialOfferProduct.Where(sop => sop.ProductID == product.ProductID).FirstOrDefault().EntityKey;
                    // Add item to the items collection and 
                    // add order to the orders collection.
                    order.SalesOrderDetail.Add(item);
                    customer.SalesOrderHeader.Add(order);

                    // Save changes pessimistically. This means that changes 
                    // must be accepted manually once the transaction succeeds.
                    advWorksContext.SaveChanges();

                    MessageBox.Show("Order created with order number: "
                        + order.SalesOrderNumber);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
