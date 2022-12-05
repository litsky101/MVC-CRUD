using Employees.Controller;
using Employees.Model;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employees.View
{
    public partial class EmployeeDetails : MetroForm
    {
        EmployeeDetailsController controller = null;
        Employee employee = null;

        public EmployeeDetails()
        {
            InitializeComponent();
        }

        private void InsertEmployeeDetails()
        {
            try
            {
                controller = new EmployeeDetailsController();
                Employee emp = new Employee();

                emp.EmployeeID = txtEmployeeID.Text;
                emp.LastName = txtLastName.Text;
                emp.FirstName = txtFirstName.Text;
                emp.MiddleName = txtMiddleName.Text;
                emp.Age = !string.IsNullOrEmpty(txtAge.Text) ? Convert.ToInt32(txtAge.Text) : 0;
                emp.CivilStatus = cboStatus.Text;

                controller.Employee = emp;

                if (controller.Insert() > 0)
                {
                    MetroMessageBox.Show(this, "Saved successfully", "Result",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MetroMessageBox.Show(this, "Failed to save new employee. Please try again", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

                throw;
            }
        }

        private void UpdateEmployeeDetails()
        {
            try
            {
                controller = new EmployeeDetailsController();
                Employee emp = new Employee();

                emp.EmployeeID = txtEmployeeID.Text;
                emp.LastName = txtLastName.Text;
                emp.FirstName = txtFirstName.Text;
                emp.MiddleName = txtMiddleName.Text;
                emp.Age = !string.IsNullOrEmpty(txtAge.Text) ? Convert.ToInt32(txtAge.Text) : 0;
                emp.CivilStatus = cboStatus.Text;

                controller.Employee = emp;

                if (controller.Update() > 0)
                {
                    MetroMessageBox.Show(this, "Update successfully", "Result",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MetroMessageBox.Show(this, "Failed to update employee. Please try again", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {

                throw;
            }
        }

        private void ClearFields()
        {
            cboStatus.SelectedIndex = 0;
            txtAge.Clear();
            txtDateAdded.Clear();
            txtEmployeeID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            lblID.Text = "0";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text == "0")
                    InsertEmployeeDetails();
                else
                    UpdateEmployeeDetails();

            }
            catch (Exception er)
            {
                MetroMessageBox.Show(this, er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                controller = new EmployeeDetailsController();
                employee = new Employee();

                long id = 0;

                id = Convert.ToInt32(lblID.Text);
                id++;

                employee = controller.GetEmployee(id);

                if (!string.IsNullOrEmpty(employee.EmployeeID))
                {
                    lblID.Text = employee.ID.ToString();
                    txtEmployeeID.Text = employee.EmployeeID;
                    txtLastName.Text = employee.LastName;
                    txtFirstName.Text = employee.FirstName;
                    txtMiddleName.Text = employee.MiddleName;
                    txtAge.Text = employee.Age.ToString();
                    cboStatus.Text = employee.CivilStatus;
                    txtDateAdded.Text = employee.DateAdded.ToString();
                }

            }
            catch (Exception er)
            {
                MetroMessageBox.Show(this, er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                controller = new EmployeeDetailsController();
                employee = new Employee();

                long id = 0;

                id = Convert.ToInt32(lblID.Text);

                if (id > 0)
                    id--;
                else
                    id = 1;

                employee = controller.GetEmployee(id);

                if (!string.IsNullOrEmpty(employee.EmployeeID))
                {
                    lblID.Text = employee.ID.ToString();
                    txtEmployeeID.Text = employee.EmployeeID;
                    txtLastName.Text = employee.LastName;
                    txtFirstName.Text = employee.FirstName;
                    txtMiddleName.Text = employee.MiddleName;
                    txtAge.Text = employee.Age.ToString();
                    cboStatus.Text = employee.CivilStatus;
                    txtDateAdded.Text = employee.DateAdded.ToString();

                    btnSave.Text = "Update";
                }
            }
            catch (Exception er)
            {
                MetroMessageBox.Show(this, er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
