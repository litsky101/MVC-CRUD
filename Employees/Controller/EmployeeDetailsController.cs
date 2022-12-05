using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Service;
using Employees.Model;
using MySql.Data.MySqlClient;

namespace Employees.Controller
{
    public class EmployeeDetailsController
    {
        private DataAccess db = null;
        private Employee employee = null;

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public EmployeeDetailsController()
        {
            
        }

        public Employee GetEmployee(long id)
        {
            try
            {
                employee = new Employee();

                db = new DataAccess();

                db.OpenConnection();
                db.ExecuteTSQL(@"SELECT id, employeeid, lastname, firstname, middlename, age, civilstatus, dateadded FROM hei00 WHERE id = @id;");
                db.cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader dr = db.cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        employee.ID = Convert.ToInt32(dr["id"]);
                        employee.EmployeeID = dr["employeeid"].ToString();
                        employee.LastName = dr["lastname"].ToString();
                        employee.FirstName = dr["firstname"].ToString();
                        employee.MiddleName = dr["middlename"].ToString();
                        employee.Age = Convert.ToInt32(dr["age"]);
                        employee.CivilStatus = dr["civilstatus"].ToString();
                        employee.DateAdded = Convert.ToDateTime(dr["dateadded"]);
                    }
                }

                return employee;
            }
            catch
            {

                throw;
            }
        }

        public int Insert()
        {
            try
            {
                int result = 0;

                db = new DataAccess();

                db.BeginTransaction();
                db.ExecuteTSQL(@"INSERT INTO hei00 (employeeid, lastname, firstname, middlename, age, civilstatus)
                        VALUES(@employeeid, @lastname, @firstname, @middlename, @age, @civilstatus); ");

                db.cmd.Parameters.AddWithValue("@employeeid", employee.EmployeeID);
                db.cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                db.cmd.Parameters.AddWithValue("@firstname", employee.FirstName);
                db.cmd.Parameters.AddWithValue("@middlename", employee.MiddleName);
                db.cmd.Parameters.AddWithValue("@age", employee.Age);
                db.cmd.Parameters.AddWithValue("@civilstatus", employee.CivilStatus);

                result = db.cmd.ExecuteNonQuery();

                db.CommitTransaction();

                return result;
            }
            catch
            {
                db.RollbackTransaction();
                throw;
            }
            finally
            {
                db.CloseConnection();
            }

        }

        public int Update()
        {
            try
            {
                int result = 0;

                db = new DataAccess();

                db.BeginTransaction();
                db.ExecuteTSQL(@"UPDATE hei00 SET lastname = @lastname, firstname = @firstname, middlename = @middlename,
                                    age = @age, civilstatus = @civilstatus WHERE employeeid = @employeeid");

                db.cmd.Parameters.AddWithValue("@employeeid", employee.EmployeeID);
                db.cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                db.cmd.Parameters.AddWithValue("@firstname", employee.FirstName);
                db.cmd.Parameters.AddWithValue("@middlename", employee.MiddleName);
                db.cmd.Parameters.AddWithValue("@age", employee.Age);
                db.cmd.Parameters.AddWithValue("@civilstatus", employee.CivilStatus);

                result = db.cmd.ExecuteNonQuery();

                db.CommitTransaction();

                return result;
            }
            catch
            {
                db.RollbackTransaction();
                throw;
            }
            finally
            {
                db.CloseConnection();
            }
        }
    }
}
