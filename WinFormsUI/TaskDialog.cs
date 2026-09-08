using System;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace WinFormsUI
{
    public class TaskDialog : Form
    {
        public SimpleTask TaskResult { get; private set; }
        private TextBox txtTitle;
        private NumericUpDown numHours;

        public TaskDialog(SimpleTask taskToEdit = null)
        {
            Text = taskToEdit == null ? "Add New Task" : "Edit Task";
            Size = new Size(300, 200);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            Label lblTitle = new Label { Text = "Task Title:", Location = new Point(10, 20) };
            txtTitle = new TextBox { Location = new Point(100, 20), Width = 150 };

            Label lblHours = new Label { Text = "Est. Hours:", Location = new Point(10, 60) };
            numHours = new NumericUpDown { Location = new Point(100, 60), Width = 150, Maximum = 1000 };

            if (taskToEdit != null)
            {
                txtTitle.Text = taskToEdit.Title;
                numHours.Value = taskToEdit.Hours;
            }

            Button btnSave = new Button { Text = "Save", Location = new Point(100, 110), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) => 
            {
                TaskResult = new SimpleTask { Title = txtTitle.Text, Hours = (int)numHours.Value };
            };

            Controls.Add(lblTitle); Controls.Add(txtTitle);
            Controls.Add(lblHours); Controls.Add(numHours);
            Controls.Add(btnSave);
        }
    }
}