using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Core;

namespace WinFormsUI
{
    public class MainForm : Form
    {
        private DataGridView grid;
        private BindingSource bindingSource;
        private List<SimpleTask> tasks;

        public MainForm()
        {
            Text = "Task Manager - Lab 6";
            Size = new Size(600, 400);
            tasks = new List<SimpleTask>();
            bindingSource = new BindingSource { DataSource = tasks };

            grid = new DataGridView 
            { 
                DataSource = bindingSource, 
                Location = new Point(10, 50), 
                Size = new Size(560, 300),
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            Button btnAdd = new Button { Text = "Add", Location = new Point(10, 10) };
            btnAdd.Click += BtnAdd_Click;

            Button btnDelete = new Button { Text = "Delete", Location = new Point(90, 10) };
            btnDelete.Click += BtnDelete_Click;

            Button btnSave = new Button { Text = "Save (JSON)", Location = new Point(170, 10) };
            btnSave.Click += BtnSave_Click;

            Button btnLoad = new Button { Text = "Load (JSON)", Location = new Point(270, 10) };
            btnLoad.Click += BtnLoad_Click;

            Controls.Add(grid);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(btnLoad);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new TaskDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    dialog.TaskResult.Id = tasks.Count > 0 ? tasks[tasks.Count - 1].Id + 1 : 1;
                    tasks.Add(dialog.TaskResult);
                    bindingSource.ResetBindings(false); // Tabloyu guncelle
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    tasks.RemoveAt(grid.CurrentRow.Index);
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "JSON Files|*.json" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(sfd.FileName, json);
                    MessageBox.Show("Data successfully saved!");
                }
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "JSON Files|*.json" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(ofd.FileName);
                    var loadedTasks = JsonSerializer.Deserialize<List<SimpleTask>>(json);
                    tasks.Clear();
                    tasks.AddRange(loadedTasks);
                    bindingSource.ResetBindings(false);
                }
            }
        }
    }
}