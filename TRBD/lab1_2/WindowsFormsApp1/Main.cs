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
    public partial class Main : Form
    {
        DataSet dataset = new DataSet();
        string filepath = "Data.xml";

        public Main()
        {
            InitializeComponent();
        }

        private void SaveToXML()
        {
            dataset.student.AcceptChanges();
            dataset._check_in.AcceptChanges();
            dataset.WriteXml(this.filepath);
        }

        private void LoadFromXML()
        {
            dataset.Clear();
            dataset.ReadXml(this.filepath);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadFromXML();
            dataGridView1.Sort(dataGridView1.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void SelectRow(DataGridView dataGrid, int bIndex)
        {
            if (bIndex >= 0 && bIndex < dataGrid.Rows.Count)
            {
                dataGrid.CurrentCell = dataGrid.Rows[bIndex].Cells[2];
                dataGrid.Rows[bIndex].Selected = true;
                dataGrid.FirstDisplayedScrollingColumnIndex = bIndex;
            }
        }

        private void SelectRowId(DataGridView dataGrid, Int64 id)
        {
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == id)
                {
                    dataGrid.CurrentCell = row.Cells[2];
                    row.Selected = true;
                    dataGrid.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null && dataGridView2.DataSource != null)
            {
                dataGridView2.DataSource = dataset.student;
                (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = String.Format(
                    "code = {0}",
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value
                );
            }
        }

        private void AddStudentClick(object sender, EventArgs e)
        {
            AddStudent form = new AddStudent(dataset, true);
            form.ShowDialog();
            SaveToXML();
            SelectRowId(dataGridView1, form.NewId)
        }

        private void EditStudentClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                long editId = Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value);
                AddStudent form = new AddStudent(dataset, false, editId);
                form.ShowDialog();
                SaveToXML();
            }
        }

        private void DelBoolClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                SaveToXML();
                SelectRow(dataGridView1, 0);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
