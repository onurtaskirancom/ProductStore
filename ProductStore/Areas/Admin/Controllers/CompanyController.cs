using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.DataAccess.IMainRepository;
using ProductStore.Models.DbModels;
using ProductStore.Utility;

namespace ProductStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ProjectConstant.Role_Admin + "," + ProjectConstant.Role_Employee)]
    public class CompanyController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork; 
        #endregion

        #region CTOR
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 
        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        } 
        #endregion

        #region API CALLS
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Company.GetAll();
            return Json(new { data = allObj });
        } 
        #endregion

        public IActionResult Delete(int id)
        {
            var deleteData = _unitOfWork.Company.Get(id);
            if (deleteData == null)
                return Json(new { success = false, message = "Data Not Found!" });

            _unitOfWork.Company.Remove(deleteData);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });

        }

        /// <summary>
        /// Create or update Get Method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Company cat = new Company();
            if (id==null)
            {
                //This for create
                return View(cat);
            }

            cat = _unitOfWork.Company.Get((int)id);
            if (cat != null)
            {
                return View(cat);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company Company)
        {
            if (ModelState.IsValid)
            {
                if (Company.Id == 0)
                {
                    //Create
                    _unitOfWork.Company.Add(Company);
                }
                else
                {
                    //Update
                    _unitOfWork.Company.Update(Company);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(Company);
        }
    }
}
