﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using coffeeApp.UI;
using coffeeApp.Models;

namespace coffeeApp
{
    public partial class Beans : Form
    {
        private List<Coffee> orderList = new List<Coffee>();
        public Beans()
        {
            InitializeComponent();
            listViewOrder.View = View.Details;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
        }

        private void countToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String msgBox = String.Format("There are {0} coffee flavors in the list", comboBoxCoffeeFlavors.Items.Count);
            MessageBox.Show(msgBox, "Number of Coffee Flavors");
        }

        private void buttonAddToOrder_Click(object sender, EventArgs e)
        {
            string coffeeFlavor;
            string syrupFlavor;
            string quantity;
            string price;
            string size;
            bool sizeSelected = radioButtonExtraLarge.Checked == false && radioButtonLarge.Checked == false && radioButtonMedium.Checked == false && radioButtonSmall.Checked == false;

            if(comboBoxCoffeeFlavors.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a coffee flavor", "Select Coffee Flavor");
                return;
            }
            if(comboBoxQuantity.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a quantity", "Select Number of Drinks");
                return;
            }
            if (sizeSelected)
            {
                MessageBox.Show("You must select a drink size", "Select drink size");
                return;
            }
            if(listBoxSyrupFlavors.SelectedIndex != -1)
            {
                syrupFlavor = listBoxSyrupFlavors.GetItemText(listBoxSyrupFlavors.SelectedIndex);
            }
            else
            {
                syrupFlavor = "";
            }
            coffeeFlavor = comboBoxCoffeeFlavors.Text;
            size = getSize();
            price = getPrice(size);
            quantity = comboBoxQuantity.Text;

            Coffee coffee = new Coffee(coffeeFlavor, syrupFlavor, size, price, quantity);
            orderList.Add(coffee);
            addOrderToListView();
        }

        private void addOrderToListView()
        {
            foreach(Coffee coffee in orderList)
            {
                ListViewItem listViewItem = new ListViewItem(coffee.coffeeFlavor, 0);
                listViewItem.SubItems.Add(coffee.syrupFlavor);
                listViewItem.SubItems.Add(coffee.quantity);
                listViewItem.SubItems.Add(coffee.price);
                listViewOrder.Items.Add(listViewItem);
            }
        }

        private string getSize()
        {
            if(radioButtonExtraLarge.Checked == true)
            {
                return "Extra-Large";
            }
            else if(radioButtonLarge.Checked == true)
            {
                return "Large";
            }
            else if(radioButtonMedium.Checked == true)
            {
                return "Medium";
            }
            else
            {
                return "Small";
            }
        }

        private string getPrice(string size)
        {
            string price = "";
            double tempPrice = 0;
            if(listBoxSyrupFlavors.SelectedIndex != -1)
            {
                tempPrice += 2.50;
            }
            if(size == "Small")
            {
                tempPrice += 1.00;
            }
            else if(size == "Medium")
            {
                tempPrice += 1.50;
            }
            else if(size == "Large")
            {
                tempPrice += 2.50;
            }
            else
            {
                tempPrice += 3.00;
            }
            
            price = string.Format("${0:N2}", tempPrice);
            return price;
        }

        private void buttonCompleteOrder_Click(object sender, EventArgs e)
        {
            if(maskedTextBoxOrderName.Text == string.Empty)
            {
                MessageBox.Show("You must give an order name", "Give an order name");
            }
        }

        private void buttonClearOrder_Click(object sender, EventArgs e)
        {

        }
    }
}
