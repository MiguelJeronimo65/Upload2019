using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using System.Security.Permissions;
using System.Runtime.InteropServices.WindowsRuntime;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        //public IActionResult Index()
        //{
        //    var list = _sellerService.FindAll();
        //    return View(list);
        //}
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        //public IActionResult Create()
        //{
        //    var departments = _departmentService.FindALL();
        //    var viewModel = new SellerFormViewModel { Departments = departments };
        //    return View(viewModel);

        //}

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindALLAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Seller seller)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        var departments = _departmentService.FindALL();
        //        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        //        return View(viewModel);
        //    }
        //    _sellerService.Insert(seller);
        //    return RedirectToAction("Index");
        //    //return RedirectToAction(nameof(Index);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {

            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindALLAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction("Index");
            //return RedirectToAction(nameof(Index);

        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        //return NotFound();
        //        return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
        //    }

        //    var obj = _sellerService.FindById(id.Value);
        //    if (obj == null)
        //    {
        //        //return NotFound();
        //        return RedirectToAction(nameof(Error), new { Message = "Id Not Found" });
        //    }

        //    return View(obj);
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { Message = "Id Not Found" });
            }

            return View(obj);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        //{
        //    _sellerService.Remove(id);
        //    //return RedirectToAction("Index");
        //    return RedirectToAction(nameof(Index));

        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));

        }

        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        //return NotFound();
        //        return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
        //    }

        //    var obj = _sellerService.FindById(id.Value);
        //    if (obj == null)
        //    {
        //        //return NotFound();
        //        return RedirectToAction(nameof(Error), new { Message = "Id Not Found" });
        //    }

        //    return View(obj);
        //}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { Message = "Id Not Found" });
            }

            return View(obj);
        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        //return NotFound();
        //        return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
        //    }

        //    var obj = _sellerService.FindById(id.Value);
        //    if (obj == null)
        //    {
        //        //return NotFound();
        //        return RedirectToAction(nameof(Error), new { Message = "Id Not Found" });
        //    }

        //    /*Lista todo os Departamentos*/
        //    List<Department> departments = _departmentService.FindALL();

        //    /*carregar os objectos que vão ser usados*/
        //    SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

        //    return View(viewModel);
        //}


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { Message = "Id Not Found" });
            }

            /*Lista todo os Departamentos*/
            List<Department> departments = await _departmentService.FindALLAsync();

            /*carregar os objectos que vão ser usados*/
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, Seller seller)
        //{

        //    if (!ModelState.IsValid)
        //    {

        //        var departments = _departmentService.FindALL();
        //        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        //        return View(viewModel);
        //    }

        //    if (id != seller.Id)
        //    {
        //        //return BadRequest();
        //        return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
        //    }

        //    try
        //    {
        //        _sellerService.Update(seller);
        //        //Redireccionar para o index
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (NotFoundException e)
        //    {
        //        //return NotFound();
        //        return RedirectToAction(nameof(Error), new { Message = e.Message });
        //    }
        //    catch (DBConcurrencyException e)
        //    {
        //        //return BadRequest();
        //        return RedirectToAction(nameof(Error), new { Message = e.Message });
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            if (!ModelState.IsValid)
            {

                var departments =await _departmentService.FindALLAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                //Redireccionar para o index
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
            catch (DBConcurrencyException e )
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier   /*ID interno */

            };

            return View(viewModel);

        }
    }
}