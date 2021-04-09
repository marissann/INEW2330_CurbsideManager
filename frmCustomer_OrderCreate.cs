using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMB3_Curbside_Manager
{
    public partial class frmCustomer_OrderCreate : Form
    {
        public frmCustomer_OrderCreate()
        {
            InitializeComponent();
        }

        double Total = 0.0;

        public struct Product
        {
            //struct variables
            public int productID;
            public string productName;
            public double productPrice;
            public int itemQuantity;

            //constructor
            public Product(int productID, string productName, double productPrice, int itemQuantity)
            {
                this.productID = productID;
                this.productName = productName;
                this.productPrice = productPrice;
                this.itemQuantity = itemQuantity;
            }
        }

        List<Product> productList = new List<Product>();


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomer_OrderCreate_Load(object sender, EventArgs e)
        {
            ProgOps.CreateNewOrder(tbxMerchandiseID, dgvMerchandise, true);
        }

        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
            
            double dblPrice;
            if (tbxMerchandiseID.Text == "")
            {
                ProgOps.CreateNewOrder(tbxMerchandiseID, dgvMerchandise, true);

            }
            else
            {
                Console.WriteLine(Total);
                ProgOps.CreateNewOrder(tbxMerchandiseID, dgvMerchandise, false);
                
                //Add order to cart on form
                
                lbxOrder.Items.Add("Item Name : " + dgvMerchandise.CurrentRow.Cells[1].Value.ToString());
                lbxOrder.Items.Add("Price: $" + dgvMerchandise.CurrentRow.Cells[2].Value);
                dblPrice = Convert.ToDouble(dgvMerchandise.CurrentRow.Cells[2].Value);
                Total += dblPrice;

                //Good Job Scott
                productList.Add(new Product(int.Parse(tbxMerchandiseID.Text), dgvMerchandise.CurrentRow.Cells[1].Value.ToString(), dblPrice, int.Parse(tbxQuantity.Text)));
                MessageBox.Show(productList[0].productName + "\n" + productList[0].productID + "\n" + productList[0].productPrice + "\n" + productList[0].itemQuantity);


                ProgOps.CreateNewOrder(tbxMerchandiseID, dgvMerchandise, true);
            }




        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            ProgOps.CreateOrder(Total);
        }

        private void tbxMerchandiseID_TextChanged(object sender, EventArgs e)
        {

        }

    }

}
