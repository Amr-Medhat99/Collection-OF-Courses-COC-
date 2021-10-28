using coc_graduation_project_.Models;
using coc_graduation_project_.ViewModel;
using coc_graduation_project_.ViewModelRecieveData;
using coc_graduation_project_.ViewModelReturnData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Services
{
    public interface ICenterService
    {
        //ErrorResponse AddCourseToCenter(AddCourseViewModel Model,int CenterID);
        Task<ErrorResponse> AddBranchAsync(BranchViewModel Model, int centerID);
        Task<ErrorResponse> EditCenter(EditCenter Model, int CenterID);
        Task<ReturnCenterProfile> ReturnCenterProfile(int CenterID);

    }
    public class CenterService : ICenterService
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly DBcontext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailServices;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CenterService(UserManager<IdentityUser> usermanager, DBcontext dbContext, IConfiguration configuration, IMailService mailServices, IWebHostEnvironment hostEnvironment)
        {
            _usermanager = usermanager;
            _dbContext = dbContext;
            _configuration = configuration;
            _mailServices = mailServices;
            webHostEnvironment = hostEnvironment;
        }
        private string UploadedFile(EditCenter Model)
        {
            string uniqueFileName = null;

            if (Model.logo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Model.logo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Model.logo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<ErrorResponse> AddBranchAsync(BranchViewModel Model, int centerID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Branch View Is Null");
                }
                var check = await _dbContext.Branches.FindAsync(Model.branchName);
                if (check != null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "This Branch Name Is Already Found"
                    };
                }
                var Branch = new Branch
                {
                    BranchAddress = Model.branchAddress,
                    BranchName = Model.branchName,
                    BranchPhone = Model.branchPhone,
                    CenterId = centerID
                };
                var x = _dbContext.Branches.Add(Branch);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Branch Created Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Branch Not Added Some Error Occuer Please Try Again"
                };
            }
            catch (Exception ex)
            {
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }   
        }

        public async Task<ErrorResponse> EditCenter(EditCenter Model, int CenterID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("AddProfileDtaAsync View Is Null");
                }
                Center center =_dbContext.Centers.Find(CenterID);
                if (center == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No Center Have This ID"
                    };
                }

                string uniqueFileName = UploadedFile(Model);
                center.logo= uniqueFileName;
                center.CenterName = Model.username;
                center.address = Model.address;

                var x = _dbContext.Centers.Update(center);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    var userID = center.AppUserId;
                    var Identity =await _usermanager.FindByIdAsync(userID);
                    Identity.PhoneNumber = Model.phone;
                    Identity.Email = Model.email;
                    var result = await _usermanager.UpdateAsync(Identity);
                    if (result.Succeeded)
                    {
                        return new ErrorResponse
                        {
                            IsSuccess = true,
                            Message = "Profile Updated Successfully"
                        };
                    }
                    else
                    {
                        return new ErrorResponse
                        {
                            IsSuccess = false,
                            Error = result.Errors.Select(s => s.Description)
                        };
                    }
                }
                else
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Profile Not Updated Successfully",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ReturnCenterProfile> ReturnCenterProfile(int CenterID)
        {
            try
            {
                Center center = _dbContext.Centers.Find(CenterID);
                if (center == null)
                {
                    return new ReturnCenterProfile
                    {
                        IsSuccess = false,
                        Message = "No Center Found With This CenterID"
                    };
                }
                List<CourseListData> courseLD = null;
                List<Course> courseListData = _dbContext.Courses.Select(s => s).Where(s => s.CenterID == CenterID).ToList();
                if (courseListData.Count != 0)
                {
                    courseLD = new List<CourseListData>();
                    foreach (var item in courseListData)
                    {
                        courseLD.Add(new CourseListData
                        {
                            Address = center.address,
                            CenterName = center.CenterName,
                            CourseID = item.CourseID,
                            CourseName = item.CourseName,
                            InstructorName = null,
                            Logo = item.Logo,
                            Online = item.Online,
                            Options = item.Options,
                            Price = item.Price,
                            QA_Following = item.QA_Following,
                            RelasedDate = item.RelasedDate,
                            Stars = item.Stars
                        });
                    }
                }
                IdentityUser identity = await _usermanager.FindByIdAsync(center.AppUserId);
                if (identity == null)
                {
                    return new ReturnCenterProfile
                    {
                        IsSuccess = false,
                        Message = "This Center Don't Belong To Any AppUserID",
                    };
                }
                ReturnCenterProfile returnCP = new ReturnCenterProfile()
                {
                    address = center.address,
                    email = identity.Email,
                    logo = center.logo,
                    phone = identity.PhoneNumber,
                    userName = center.CenterName,
                    IsSuccess = true,
                    Message = "Data Returned Successfully",
                    myCourses = courseLD
                };
                return (returnCP);
            }
            catch (Exception ex)
            {
                return new ReturnCenterProfile
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


    }
}