using AutoMapper;
using Deme.DAL.Entities;
using Demo.BLL.Interfaces;
using Demo.BLL.Reposatories;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace Demo.PL.Controllers
{
    public class DepartmentController1 : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IDepartmentRepository departmentReposatory;
        private readonly ILogger<DepartmentController1> logger;
        private readonly IMapper mapper;

        public DepartmentController1(IUnitOfWork unitOfWork
            ,ILogger<DepartmentController1>logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            //this.departmentReposatory = departmentReposatory;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult index()
        {
            ViewBag.Message = "Hello From ViewBag";
            ViewData["Message"] = "Hello From ViewData";
            var departments = unitOfWork.DepartmentRepository.GetAll();
           var departmentViewModel = mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(departmentViewModel);
        }
        [HttpGet]
        public IActionResult create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult create(DepartmentViewModel departmentViewModel)
        {
            //TempData["Message"] = "Department Added Succesfully!";



            if (ModelState.IsValid)
            {
                var department=mapper.Map<Department>(departmentViewModel);
               unitOfWork.DepartmentRepository.Add(department);
                unitOfWork.complete();
                return RedirectToAction(nameof(index));
            }
            return View(departmentViewModel);
        }

        [HttpGet]
        public IActionResult Details(int ?id) {

            try
            {
                if (id is null)
                    return BadRequest();
                var department = unitOfWork.DepartmentRepository.GetById(id);
                if (department == null)
                    return NotFound();
                return View(department);
             
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error","Home");

            }
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {

            try
            {
                if (id is null)
                    return BadRequest();
                var department =  unitOfWork.DepartmentRepository.GetById(id);
                if (department == null)
                    return NotFound();
                return View(department);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");

            }
        }
        [HttpPost]


        public IActionResult Update(int id,Department department)
        {

            if (id != department.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepository.Update(department);
                unitOfWork.complete();
                return RedirectToAction(nameof(index));
            }
            return View(department);

        }


        [HttpGet]

        public IActionResult Delete(int? id)
        {
            try
            {
                if (id is null)
                    return BadRequest();
                var department = unitOfWork.DepartmentRepository.GetById(id);
                if (department == null)
                    return NotFound();
                if (ModelState.IsValid)
                {
                    unitOfWork.DepartmentRepository.Delete(department);
                    unitOfWork.complete();

                }
                return RedirectToAction(nameof(index));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");

            }

        }


    }
}
