using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_Thread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            var stopWatch = new Stopwatch();
            string result = "";

            //create a task to run the fibo, so that the UI can not be lock up
            //textBox2.Text can not be updated within the task
            //WPF and most Windows UI frameworks have something called “Thread affinity”.
            //This means you can’t change UI stuff on any thread that isn’t the main UI thread.
            var task = new Task(() =>
            {
                stopWatch.Start();
                result = Fibo(textBox1.Text).ToString();
            });

            //textBox2.Text can be updated within the current UI task which is specified by the second parameter 
            task.ContinueWith((previousTask) =>
            {
                textBox2.Text = result;
                stopWatch.Stop();
                label2.Text = (stopWatch.ElapsedMilliseconds / 1000).ToString();
                stopWatch.Reset();
            },
                        
            //use a TaskScheduler associated with the current UI SynchronizationContex as the second parameter in task.ContinueWith to run a new continuation task on the UI thread.
            TaskScheduler.FromCurrentSynchronizationContext()
            );

            task.Start();
        }

        private ulong Fibo(string nthValue)
        {
            try
            {
                ulong x = 0, y = 1, z = 0, nth, i;
                nth = Convert.ToUInt64(nthValue);
                for (i = 1; i <= nth; i++)
                {
                    z = x + y;
                    x = y;
                    y = z;
                }

                return z;
            }
            catch { }

            return 0;
        }

    }
}
