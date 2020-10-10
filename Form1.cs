using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Order_Menu_Items
{
    public partial class Form1 : Form
    {
        static int i = 0, total = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            int price, quantity;
            string item;

            if (listView1.SelectedItems.Count != 0 && txt_quantity.Text != "")
            {
                price = int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                if (int.TryParse(txt_quantity.Text, out quantity))
                {
                    if (quantity > 0)
                    {
                        price = price * quantity;
                        item = listView1.SelectedItems[0].Text;

                        listView2.Items.Add(item);
                        listView2.Items[i].SubItems.Add(price.ToString());
                        i++;

                        total = total + price;
                        lbl_total.Text = total.ToString();
                        lbl_total.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Quantity\nYou have to input" +
                                        " an integer greater than zero", 
                                        "Input Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Quantity\nYou have to input" +
                                    " an integer greater than zero",
                                    "Input Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Select Item and input Quantity you want", 
                                "Error", 
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }


        
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btn_orderCancel_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 0)
            {
                int deductSum = 0; //total deduction 
                int rmItemCount = 0; //no of multiple items removed
                var confirm = MessageBox.Show("Are you sure you want to" +
                                              " cancel the order", 
                                              "DELETE ORDER", 
                                              MessageBoxButtons.YesNo, 
                                              MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    for(int j = 0; j < listView2.Items.Count; j++)
                    {
                        if (listView2.Items[j].Selected)
                        {
                            int deduct=int.Parse(listView2.Items[j].SubItems[1].Text);
                            listView2.Items[j].Remove();
                            rmItemCount += 1;
                            j--;
                            deductSum += deduct;
                        }
                    }
                    i -= rmItemCount;
                }

                total = total - deductSum;
                lbl_total.Text = total.ToString();
                lbl_total.Visible = true;
            }
            else
            {
                MessageBox.Show("Select Item to Cancel Order", 
                                "Cancel Order", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
            }
        }
    }
}
