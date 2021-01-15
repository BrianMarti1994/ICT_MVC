using ICTApplication.Context;
using ICTApplication.Models;
using ICTApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICTApplication.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly ICTEmployeeEntities _dbContext;

        public EmployeeController()
        {
            this._dbContext = new ICTEmployeeEntities();
        }


        // GET: AllEmployee
        public ActionResult EmployeeList()
        {

            List<Employee> Employeelist = new List<Employee>();

            var objEmployee = (from p in _dbContext.tblEmployees
                               join e in _dbContext.tblDepartments
                               on p.DepId equals e.DepId
                               where p.IsActive == true
                               select new
                               {
                                   EmpId = p.EmpId,
                                   Name = p.Name,
                                   Code = p.Code,
                                   DOB = p.DOB,
                                   DepartmenName = e.Name
                               }).ToList();
            foreach (var item in objEmployee)
            {

                Employee objEmpe = new Employee();
                objEmpe.EmpId = item.EmpId;
                objEmpe.Code = item.Code;
                objEmpe.DOB = Convert.ToDateTime(item.DOB);
                objEmpe.Name = item.Name;
                objEmpe.DepartmenName = item.DepartmenName;
                Employeelist.Add(objEmpe);
            }


            return View(Employeelist);

        }

        //Add Employee View
        public ActionResult AddEmployee()
        {

            var Dep = _dbContext.tblDepartments.ToList();
            List<Department> lstDepartment = new List<Department>();
            Employee objEmployee = new Employee();
            foreach (var c in Dep)
            {
                Department objDep = new Department();
                objDep.Name = c.Name;
                objDep.DepId = c.DepId;
                lstDepartment.Add(objDep);
            }
            DateTime oDate = DateTime.Now;
            string strDate = oDate.ToString("dd-MM-yyyy");
            objEmployee.DOB = Convert.ToDateTime(strDate);
            var employeeViewModel = new ViewModel.EmployeeViewModel()
            {

                Department = lstDepartment,
                Employee = objEmployee
            };
            return View("AddEmployee", employeeViewModel);

        }

        //Edit Employee Details
        public ActionResult Edit(int id)
        {
            try
            {


                var employees = this._dbContext.tblEmployees.FirstOrDefault(x => x.EmpId == id);
                var department = this._dbContext.tblDepartments.ToList();
                List<Department> lstDepartment = new List<Department>();
                Employee objEmployee = new Employee();
                foreach (var c in department)
                {
                    Department objDep = new Department();
                    objDep.Name = c.Name;
                    objDep.DepId = c.DepId;
                    lstDepartment.Add(objDep);
                }
                objEmployee.EmpId = employees.EmpId;
                objEmployee.Name = employees.Name;
                objEmployee.Code = employees.Code;
                DateTime oDate = Convert.ToDateTime(employees.DOB);
                string strDate = oDate.ToString("dd-MM-yyyy");
                objEmployee.DOB = Convert.ToDateTime(strDate);
                objEmployee.DepId = employees.DepId;

                var viewModel = new EmployeeViewModel()
                {
                    Department = lstDepartment,
                    Employee = objEmployee
                };
                return View("AddEmployee", viewModel);
            }
            catch (Exception)
            {

                return View("~/Shared/Error.cshtml");
            }
        }

        //Soft Delete Employee Details
        public ActionResult Delete(int id)
        {
            try
            {


                var employeeDb = this._dbContext.tblEmployees.FirstOrDefault(x => x.EmpId == id);
                employeeDb.IsActive = false;
                _dbContext.SaveChanges();
                return RedirectToAction("EmployeeList", "Employee");
            }
            catch (Exception)
            {

                return View("~/Shared/Error.cshtml");
            }
        }

        //Save Add or Update Employee Details into DB
        [HttpPost]
        public ActionResult SaveEmployee(EmployeeViewModel objEmployee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("AddEmployee", "Employee");
                }
                tblEmployee objtblEmployee = new tblEmployee();

                if (objEmployee.Employee.EmpId == 0)
                {
                    objtblEmployee.Name = objEmployee.Employee.Name;
                    objtblEmployee.Code = objEmployee.Employee.Code;
                    objtblEmployee.DOB = Convert.ToString(objEmployee.Employee.DOB.ToString());
                    objtblEmployee.DepId = objEmployee.Employee.DepId;
                    objtblEmployee.IsActive = true;
                    _dbContext.tblEmployees.Add(objtblEmployee);
                }
                else
                {
                    var employeesDb = this._dbContext.tblEmployees.FirstOrDefault(x => x.EmpId == objEmployee.Employee.EmpId);
                    employeesDb.EmpId = objEmployee.Employee.EmpId;
                    employeesDb.Name = objEmployee.Employee.Name;
                    employeesDb.Code = objEmployee.Employee.Code;
                    employeesDb.DOB = Convert.ToString(objEmployee.Employee.DOB.ToString());
                    employeesDb.DepId = objEmployee.Employee.DepId;

                }

                _dbContext.SaveChanges();
                return RedirectToAction("EmployeeList", "Employee");
            }
            catch (Exception)
            {

                return View("~/Shared/Error.cshtml");
            }
        }

    }
}