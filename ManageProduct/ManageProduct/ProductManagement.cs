using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageProduct
{
    public partial class ProductManagement : Form
    {
        int n = 0;
        animals[] pLists = new animals[100];
        int index;
        bool fsearch = false;
        bool fedit = false;
        public ProductManagement()
        {
            InitializeComponent();
        }

        private void btnBrowser1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\user\\Lee Min Cun\\Pictures";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "(*.jpg) |*.jpg| (*.png)| *.png|(*.gif) |*.gif|(*.*)|*.* ";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtImage1.Text = openFileDialog1.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            }   

        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            if(checkInput())
            {
                animals p = new animals();
                p.ID = txtID1.Text;
                p.Name = txtName1.Text;
                p.Type = cmbType1.SelectedItem.ToString();
                p.Quantity = int.Parse(txtQuantity1.Text);
                p.Image = openFileDialog1.FileName;
                p.Description = txtDescribe1.Text;
                pLists[n] = p;
                n++;
                LoadData(pLists);
                MessageBox.Show(this, "Successfuly added !", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        private bool checkInput()
        {
            if (txtID1.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "ID can not blank!", "Notice",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtName1.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Name can not blank!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (cmbType1.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Type can not blank!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtQuantity1.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Quantity can not blank!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            try
            {
                string temp = txtQuantity1.Text;
                int.Parse(temp);
            }
            catch (Exception )
            {
                MessageBox.Show(this, "Quantiy is numberic only!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQuantity1.Focus();
                return false;
            }
            if (txtImage1.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Image can not blank!", "Notice", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
        private void LoadData (animals[] pLists)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(int)));
            dt.Columns.Add(new DataColumn("Image", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string))); 
            for (int i = 0; i < n; i++)
            {
                animals p = pLists[i];
                if (p!=null) 
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = p.ID;
                    dr[1] = p.Name;
                    dr[2] = p.Type;
                    dr[3] = p.Quantity;
                    dr[4] = p.Image;
                    dr[5] = p.Description;
                    dt.Rows.Add(dr);
                }
            }
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            animals p = pLists[index];
            PutData (p);
        }
        private void PutData (animals p)
        {
            txtID.Text = p.ID;
            txtName.Text = p.Name;
            cmbType.Text = p.Type;
            txtQuantity.Text = p.Quantity.ToString();
            txtImage.Text = p.Image.ToString();
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = new Bitmap(p.Image);
            txtDescribe.Text = p.Description;


            txtID.ReadOnly= true;
            txtName.ReadOnly= true;
            cmbType.Enabled = false;
            txtQuantity.ReadOnly= true;
            txtImage.ReadOnly= true;
            txtDescribe.ReadOnly= true;
        }

        private void btnBrowser2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\user\\Lee Min Cun\\Pictures";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "(*.jpg)|*.jpg |(*.png) |*.png |(*.gif) |*.gif |(*.*) |*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtImage1.Text = openFileDialog1.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!fsearch)
            {
                txtID.ReadOnly =false;
                btnSearch.Text = "View Result";
            }
            else
            {
                string id = txtID.Text;
                bool found = false;
                for (int i = 0; i < n; i++)
                {
                    animals p = pLists[i];
                    if (p.ID == id)
                    {
                        PutData(p);
                        found = true;
                    }
                }
                if (!found)
                {
                    MessageBox.Show(this, "Animal not found!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                btnSearch.Text = "search";
                txtID.ReadOnly = true;
            }
            fsearch = !fsearch;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this,"Do you want to delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                while (index < n -1)
                {
                    pLists[index] = pLists[index + 1];
                    index++;
                }
            }
            pLists[n-1] = null;
            n = n - 1;
            LoadData(pLists);
            MessageBox.Show(this, "Delete successful!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!fedit)
            {
                txtName.ReadOnly = false;
                cmbType.Enabled = true;
                txtQuantity.ReadOnly = false;
                txtImage.ReadOnly = false;
                txtDescribe.ReadOnly = false;
                btnUpdate.Text = "Save";
            }
            else
            {
                txtID.ReadOnly = true;
                animals p = pLists[index];
                p.Name = txtName.Text;
                p.Type = cmbType.SelectedItem.ToString();
                p.Quantity = int.Parse(txtQuantity.Text);
                p.Image= txtImage.Text;
                p.Description = txtDescribe.Text;
                btnUpdate.Text = "Update";
                LoadData(pLists);
                MessageBox.Show(this ,"Update successful!", "Result", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            fedit = !fedit;
        }

        private void ProductManagement_Load(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void tpReports_Click(object sender, EventArgs e)
        {
            txtID1.ReadOnly = true;
            txtName1.ReadOnly = true;
            cmbType1.Enabled = false;
            txtQuantity1.ReadOnly = true;
            txtImage1.ReadOnly = true;
            txtDescribe1.ReadOnly = true;
        }
    }
}
