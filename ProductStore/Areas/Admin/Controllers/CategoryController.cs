using Microsoft.AspNetCore.Mvc;
using ProductStore.DataAccess.IMainRepository;
using ProductStore.Models.DbModels;

namespace ProductStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork; 
        #endregion

        #region CTOR
        public CategoryController(IUnitOfWork unitOfWork)
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
            var allObj = _unitOfWork.category.GetAll();
            return Json(new { data = allObj });
        } 
        #endregion

        public IActionResult Delete(int id)
        {
            var deleteData = _unitOfWork.category.Get(id);
            if (deleteData == null)
                return Json(new { success = false, message = "Data Not Found!" });

            _unitOfWork.category.Remove(deleteData);
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
            Category cat = new Category();
            if (id==null)
            {
                //This for create
                return View(cat);
            }

            cat = _unitOfWork.category.Get((int)id);
            if (cat != null)
            {
                return View(cat);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    //Create
                    _unitOfWork.category.Add(category);
                }
                else
                {
                    //Update
                    _unitOfWork.category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
