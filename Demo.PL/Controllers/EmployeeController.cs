using AutoMapper;
using Deme.DAL.Entities;
using Demo.BLL.Interfaces;
using Demo.BLL.Reposatories;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IActionResult Index(string searchValue="")
        {
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeViewModel> employeesViewModel;
            if (string.IsNullOrEmpty(searchValue))
            {
                employees = unitOfWork.EmployeeRepository.GetAll();
                employeesViewModel = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }

            else
            {
                employees = unitOfWork.EmployeeRepository.Search(searchValue);
                employeesViewModel = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
            return View(employeesViewModel);
            
        }

        public IActionResult Create() { 
          ViewBag.Departments=unitOfWork.DepartmentRepository.GetAll();
            return View(new EmployeeViewModel());
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {

           // ModelState["Department"].ValidationState = ModelValidationState.Valid;
            if(ModelState.IsValid)
            {
                //manual mapping
                //Employee employee = new Employee()
                //{
                //    Name = employeeViewModel.Name,
                //    Address = employeeViewModel.Address,
                //    HiringDate = employeeViewModel.HiringDate,
                //    Email = employeeViewModel.Email,
                //    DepartmentId = employeeViewModel.DepartmentId,
                //    Salary = employeeViewModel.Salary,
                //    IsActive = employeeViewModel.IsActive,

                //};
                var employee=mapper.Map<Employee>(employeeViewModel);
                employee.ImageUrl = DocumentSetting.UploadFile(employeeViewModel.Image, "Images");
                unitOfWork.EmployeeRepository.Add(employee);
                unitOfWork.complete();
                return RedirectToAction(nameof(Index));

            }
            ViewBag.Departments = unitOfWork.DepartmentRepository.GetAll();
            return View(employeeViewModel);
        }
        public IActionResult Update(int? id )
        {
            ViewBag.Departments = unitOfWork.DepartmentRepository.GetAll();
            if (id == null )
                return BadRequest();
            var employee= unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null)
                return NotFound();
            return View(new EmployeeViewModel());
        }
        [HttpPost]
        public IActionResult Update(int id, EmployeeViewModel employeeViewModel)
        {
            //ModelState["Department"].ValidationState = ModelValidationState.Valid;

           
            if (ModelState.IsValid)
            {
                var employee = mapper.Map<Employee>(employeeViewModel);
                employee.ImageUrl = DocumentSetting.UploadFile(employeeViewModel.Image, "Images");
                unitOfWork.EmployeeRepository.Update(employee);
                unitOfWork.complete();

                return RedirectToAction(nameof(Index));
            }
         
            return View(employeeViewModel);

        }
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee=unitOfWork.EmployeeRepository.GetById(id);
            if(employee == null)
                return NotFound();
            if (ModelState.IsValid)
            {

                unitOfWork.EmployeeRepository.Delete(employee);
                unitOfWork.complete();
              
            }
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details(int? id)
        {

            
                if (id is null)
                    return BadRequest();
                var employee = unitOfWork.EmployeeRepository.GetById(id);
                if (employee == null)
                    return NotFound();
                return View(employee);

            
          
        }

    }
}
