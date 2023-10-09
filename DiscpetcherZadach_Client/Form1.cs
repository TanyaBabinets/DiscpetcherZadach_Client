using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscpetcherZadach_Client
{
    public partial class Form1 : Form
    {

        MyProcess myProcess;
        public SynchronizationContext uiContext;
        Controller controller;
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
            controller = new Controller();
            controller.sender += ShowToBox;
        }


        public void ShowToBox(List<MyProcess> LP)
        {
                      
                uiContext.Send(d => listBox1.DataSource =null, null);//обнуление списка
                uiContext.Send(d => listBox1.DataSource=LP, null);   //заполняем списком        
        }
        private void button2_Click(object sender, EventArgs e)//SHOW PROCESS
        {
            Task.Factory.StartNew(() => { controller.Connect(1, null); } );
        }

        private void button3_Click(object sender, EventArgs e)//DELETE PROCESS
        {
            Task.Factory.StartNew(() => { controller.Connect(2, myProcess); });
        }

        private void button4_Click(object sender, EventArgs e)//UPDATE
        {
            Task.Factory.StartNew(() => { controller.Connect(1, null); });
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            myProcess = listBox1.SelectedItem as MyProcess;

        }
    }
}

