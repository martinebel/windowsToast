using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windowsToast
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toastWindow toast = new windowsToast.toastWindow("1", "Nuevo Toast Generado!", "Martin Ebel - 08:22hs", toastWindow.toastClass.Primary, 3000);
            toast.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            toastWindow toast = new windowsToast.toastWindow("1", "Nuevo Toast con Fade Out!", "Martin Ebel - " + DateTime.Now.ToShortTimeString(), toastWindow.toastClass.Secondary, 3000);
            toast.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            toastWindow toast = new windowsToast.toastWindow("1", "Nuevo Toast Generado!", "Martin Ebel - " + DateTime.Now.ToShortTimeString(), toastWindow.toastClass.Danger, 0);
            toast.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            toastWindow toast = new windowsToast.toastWindow("1", "Nuevo Toast Generado!", "Martin Ebel - " + DateTime.Now.ToShortTimeString(), toastWindow.toastClass.Warning, 0);
            toast.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            toastWindow toast = new windowsToast.toastWindow("1", "Nuevo Toast Generado!", "Martin Ebel - " + DateTime.Now.ToShortTimeString(), toastWindow.toastClass.Info, 0);
            toast.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            toastWindow toast = new windowsToast.toastWindow("1", "Nuevo Toast Generado!", "Martin Ebel - " + DateTime.Now.ToShortTimeString(), toastWindow.toastClass.Light, 0);
            toast.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            toastWindow toast = new windowsToast.toastWindow("1", "Nuevo Toast Generado!", "Martin Ebel - " + DateTime.Now.ToShortTimeString(), toastWindow.toastClass.Dark, 0);
            toast.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            toastWindow toast = new windowsToast.toastWindow("1", "Nuevo Toast Generado!", "Martin Ebel - "+DateTime.Now.ToShortTimeString(), toastWindow.toastClass.Success, 0);
            toast.Show();
        }
    }
}
