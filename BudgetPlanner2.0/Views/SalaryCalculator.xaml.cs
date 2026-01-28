using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BudgetPlanner2._0.Views
{
    /// <summary>
    /// Interaction logic for SalaryCalculator.xaml
    /// </summary>
    public partial class SalaryCalculator : Window
    {
        public SalaryCalculator()
        {
            InitializeComponent();
        }
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
