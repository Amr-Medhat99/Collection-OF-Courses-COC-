using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coc_graduation_project_.Services;
using coc_graduation_project_.ViewModel;
using coc_graduation_project_.ViewModelRecieveData;
using coc_graduation_project_.ViewModelReturnData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace coc_graduation_project_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseservice;
        private readonly IMailService _mailservice;
        private readonly IConfiguration _configuration;
        public CourseController(ICourseService courseservice, IMailService mailservice, IConfiguration configuration)
        {
            _courseservice = courseservice;
            _mailservice = mailservice;
            _configuration = configuration;
        }
        [HttpGet("SelectCourses")]
        public IActionResult SelectCourses()
        {
            var result = _courseservice.SelectCourses();
            return Ok(result);
        }
        //Retrun List Of Cources
        [HttpGet("RetrunListOfCources")]
        public IActionResult RetrunListOfCources()
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.RetrunListOfCources();
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        //Retrun List Of Cources
        [HttpGet("AcceptCourse")]
        public IActionResult AcceptCourse(int CourseID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AcceptCourse(CourseID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        //Retrun List Of Cources
        [HttpGet("RejectCourse")]
        public IActionResult RejectCourse(int CourseID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.RejectCourse(CourseID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpPost("AddQAndAVideo")]
        public IActionResult AddQAndAVideo([FromForm] AddMediaViewModel model, int videoID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddQAndAVideo(model, videoID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        //Retrun List Of Cources
        [HttpGet("RetrunListOfCourcesAndNews")]
        public IActionResult RetrunListOfCourcesAndNews(int SubCategoryID, string choice)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.RetrunListOfCourcesAndNews(SubCategoryID, choice);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpPost("AddComment")]
        public IActionResult AddComment([FromForm] AddCommentViewModel Model, int studentID, int videoID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddComment(Model, studentID, videoID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        //add main category
        [HttpPost("AddMainCategory")]
        public IActionResult AddMainCategory([FromForm] AddCategoryViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddMainCategory(Model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        //return all main category
        [HttpGet("ReturnMainCategory")]
        public IActionResult ReturnMainCategory()
        {
            var result = _courseservice.ReturnMainCategory();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpPost("AddSubCategory")]
        public IActionResult AddSubCategory([FromForm] AddSubCategoryViewModel Model, int MainCategoryID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddSubCategory(Model, MainCategoryID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpGet("ReturnSubCategory")]
        public IActionResult ReturnSubCategory(int MainCategoryID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.ReturnSubCategory(MainCategoryID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpPost("AddPackage")]
        public IActionResult AddPackage([FromForm] AddPackageViewModel Model, int CourseID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddPackage(Model, CourseID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpPost("AddComponent")]
        public IActionResult AddComponent([FromForm] AddComponent Model, int packageID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddComponent(Model, packageID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpPost("AddVideo")]
        public IActionResult AddVideo([FromForm] AddVideo Model, int componetID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddVideo(Model, componetID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpPost("AddFreeVideo")]
        public IActionResult AddFreeVideo([FromForm] AddVideo Model, int componetID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddFreeVideo(Model, componetID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpGet("ReturnListChannel")]
        public IActionResult ReturnListChannel(int SubCategoryID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.ReturnListChannel(SubCategoryID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpPost("AddNewChannel")]
        public IActionResult AddNewChannel([FromForm] AddNewChannel Model)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddNewChannel(Model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpPost("AddNewCourse")]
        public IActionResult AddNewCourse(int centerID, int instructorID, int subCategoryID, [FromForm] AddCourse model)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddNewCourse(centerID, instructorID,subCategoryID,model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("AddCourseToWatchLater")]
        public IActionResult AddCourseToWatchLater(int CourseID, int WatchLaterID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddCourseToWatchLater(CourseID, WatchLaterID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("AddSubCategoryToFavorite")]
        public IActionResult AddSubCategoryToFavorite(int subCategoryID, int favoriteID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddSubCategoryToFavorite(subCategoryID, favoriteID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        
        //android
        [HttpPost("AddPackageToCart")]
        public IActionResult AddPackageToCart(int CartID, [FromBody] Root x)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddPackageToCart(CartID, x);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpPost("RemovePackageFromCart")]
        public IActionResult RemovePackageFromCart(int CartID, [FromBody] Root x)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.RemovePackageFromCart(CartID, x);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpPost("AddPackageToCurrentCourse")]
        public IActionResult AddPackageToCurrentCourse(int CurrentCoursesID, [FromBody] Root x, int cartID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddPackageToCurrentCourse(CurrentCoursesID, x,cartID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        //web
        [HttpGet("AddPackageToCartWeb")]
        public IActionResult AddPackageToCartWeb(int CartID,int packageID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddPackageToCartWeb(CartID, packageID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpGet("RemovePackageFromCartWeb")]
        public IActionResult RemovePackageFromCartWeb(int CartID, int packageID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.RemovePackageFromCartWeb(CartID, packageID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("AddPackageToCurrentCourseWeb")]
        public IActionResult AddPackageToCurrentCourseWeb(int CurrentCoursesID, int packageID, int cartID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.AddPackageToCurrentCourseWeb(CurrentCoursesID, packageID, cartID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("DeleteSubCategoryFromFavorite")]
        public IActionResult DeleteSubCategoryFromFavorite(int favoriteID, int subCategoryID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.DeleteSubCategoryFromFavorite(favoriteID, subCategoryID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("DeleteCourseFromWatchLater")]
        public IActionResult DeleteCourseFromWatchLater(int watchLaterID, int courseID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.DeleteCourseFromWatchLater(watchLaterID, courseID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("ReturnPackage")]
        public IActionResult ReturnPackage(int CourseID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.ReturnPackage(CourseID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("ReturnCurrentCourses")]
        public IActionResult ReturnCurrentCourses(int CurrentCoursesID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.ReturnCurrentCourses(CurrentCoursesID);
                if (result.Issucess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("ReturnCourseDetails")]
        public async Task<IActionResult> ReturnCourseDetails(int CourseID)
        {
            if (ModelState.IsValid)
            {
                var result =await _courseservice.ReturnCourseDetails(CourseID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("ReturnCourseDetailsAdmin")]
        public async Task<IActionResult> ReturnCourseDetailsAdmin(int CourseID)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseservice.ReturnCourseDetailsAdmin(CourseID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }


        [HttpGet("ReturnCartData")]
        public IActionResult ReturnCartData(int CartID)
        {
            if (ModelState.IsValid)
            {
                var result =_courseservice.ReturnCartData(CartID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("ReturnWatchLateCourses")]
        public IActionResult ReturnWatchLateCourses(int WatchLaterID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.ReturnWatchLateCourses(WatchLaterID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        
        [HttpGet("ReturnSubCategoryList")]
        public IActionResult ReturnSubCategoryList(int favoriteID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.ReturnSubCategoryList(favoriteID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("DeleteSubCategoryFromFavoriteWeb")]
        public IActionResult DeleteSubCategoryFromFavoriteWeb(int favoriteID, int subCategoryID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.DeleteSubCategoryFromFavoriteWeb(favoriteID, subCategoryID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("DeleteCourseFromWatchLaterWeb")]
        public IActionResult DeleteCourseFromWatchLaterWeb(int watchLaterID, int courseID)
        {
            if (ModelState.IsValid)
            {
                var result = _courseservice.DeleteCourseFromWatchLaterWeb(watchLaterID, courseID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpGet("ReturnListSubCat")]
        public IActionResult ReturnListSubCat()
        {
                var result = _courseservice.s();
                return Ok(result);
        }
        [HttpGet("ReturnListComponent")]
        public IActionResult ReturnListComponent()
        {
            var result = _courseservice.c();
            return Ok(result);
        }
    }
}
