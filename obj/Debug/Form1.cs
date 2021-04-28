using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace link_prediction_PSO_LA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public class freind
        {
            int index;
            double  score;
            int year;
        }
        public class node
        {
           public  int index;
           public  int degree;
           public   double weight;
           public  freind[] freinds;
           public node(int f)
            {
                degree = 0;
                freinds = new freind[f] ;
            }

        }

        public int loadfromfileEg(string filename,ref node[] nodes, int N )
        {
            nodes = new node[N];
           

            {
                bool e = true;
                int j = 1;
                int n = 0;
                FileStream f = File.OpenRead(filename);
                TextReader r = new StreamReader(f);

                while (e)
                {
                    string s = r.ReadLine();

                    if ((s == null))   // end of file
                    {
                        e = false;
                        break;
                    }
                    n++;
                    s = s.Replace("\t", "            ") + "               ";
                    bool check = true;
                    int l = 0;
                    int[] index = new int[10];

                    for (int i = 0; i < s.Length; i++)    // counting numbers in each line of the file
                    {
                        if (s[i] == ' ')
                            check = true;
                        if (check & s[i] != ' ')
                        {
                            index[l++] = i;
                            check = false;
                        }
                    }
                    index[l] = s.Length + 1;

                    //  for (int i = 0; i < index.Length - 1; i++)   // reading numbers
                    //      if (index[i + 1] != 0)
                    //      {
                    int ind = Convert.ToInt32(s.Substring(index[0], (index[1] - index[0])).Trim());
                    int ind2 = Convert.ToInt32(s.Substring(index[1], 8).Trim());

                    

                    if (!(nodes[ind].freinds.Contains((ind2))))
                    {
                        nodes[ind].degree++;
                        
                        nodes[ind].freinds[0] = ind;
                        nodes[ind].freinds[nodes[ind].degree] = Convert.ToInt32(s.Substring(index[1], 8).Trim());
                    }
                    if (!(nodes[ind2].freinds.Contains(ind)))
                    {
                        nodes[ind2].degree++;
                        nodes[ind2].freinds[0] = ind2;
                        nodes[ind2].freinds[nodes[ind2].degree] = ind;

                    }

                    //      }

                    j++;
                }

                r.Close();
                f.Close();
                return n;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename;
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult dr= fd.ShowDialog();
            if (dr.Equals(DialogResult.OK))
                filename = fd.FileName;

          //  Task T = new Task(new Action(loadfile))


        }
    }
}
