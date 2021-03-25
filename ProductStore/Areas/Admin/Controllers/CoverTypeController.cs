using Dapper;
using Microsoft.AspNetCore.Mvc;
using ProductStore.DataAccess.IMainRepository;
using ProductStore.Models.DbModels;
using ProductStore.Utility;

namespace ProductStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork; 
        #endregion

        #region CTOR
        public CoverTypeController(IUnitOfWork unitOfWork)
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
            //var allObj = _unitOfWork.CoverType.GetAll();
            var allCoverTypes = _unitOfWork.sp_call.List<CoverType>(ProjectConstant.Proc_CoverType_GetAll, null);
            return Json(new { data = allCoverTypes });
        } 
        #endregion

        public IActionResult Delete(int id)
        {
            //var deleteData = _unitOfWork.CoverType.Get(id);
            //if (deleteData == null)
            //    return Json(new { success = false, message = "Data Not Found!" });

            //_unitOfWork.CoverType.Remove(deleteData);
            //_unitOfWork.Save();
            //return Json(new { success = true, message = "Delete Operation Successfully" });

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);

            var deleteData = _unitOfWork.sp_call.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get, parameter);
            if (deleteData == null)
                return Json(new { success = false, message = "Data Not Found!" });

            _unitOfWork.sp_call.Execute(ProjectConstant.Proc_CoverType_Delete, parameter);
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
            CoverType coverType = new CoverType();
            if (id==null)
            {
                //This for create
                return View(coverType);
            }

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            coverType = _unitOfWork.sp_call.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get, parameter);

            //cat = _unitOfWork.CoverType.Get((int)id);
            if (coverType != null)
                return View(coverType);

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType CoverType)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", CoverType.Name);
                if (CoverType.Id == 0)
                {
                    //Create
                    //_unitOfWork.CoverType.Add(CoverType);
                    _unitOfWork.sp_call.Execute(ProjectConstant.Proc_CoverType_Create, parameter);
                }
                else
                {
                    //Update
                    parameter.Add("@Id", CoverType.Id);
                    //_unitOfWork.CoverType.Update(CoverType);
                    _unitOfWork.sp_call.Execute(ProjectConstant.Proc_CoverType_Update, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(CoverType);
        }
    }
}
