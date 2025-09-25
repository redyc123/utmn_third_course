using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddStudent : Form
    {
        Int64 editId;
        private DataSet dataset;
        public Int64 newid;
        bool newStudent;
        bool saveb = false;
        public AddStudent(DataSet dataset, bool newStudent, Int64 editId = 0)
        {
            InitializeComponent();
            this.editId = editId;
            this.dataset = dataset;
            this.newStudent = newStudent;
            if (!newStudent)
            {
                this.Text = "Редактирование студента";
                DataView dataView = dataset.student.AsDataView();
                dataView.RowFilter = $"code = {editId}";
                if (dataView.Count > 0)
                {
                    DataRowView row = dataView[0];
                    textBox1.Text = row["name"].ToString();
                    dateTimePicker1.Text = row["birthday"].ToString();
                    comboBox1.Text = row["sex"].ToString();
                    textBox2.Text = row["passport_series"].ToString();
                    textBox3.Text = row["passport_number"].ToString();
                    textBox4.Text = row["passport_date"].ToString();
                    dateTimePicker2.Text = row["birthday"].ToString();
                }
            } 
        }

        private void SaveStudent()
        {
            if (this.newStudent)
            {
                DataRow newRow = dataset.student.NewRow();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
