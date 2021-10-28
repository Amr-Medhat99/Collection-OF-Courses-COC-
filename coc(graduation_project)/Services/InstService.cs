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
    public interface InstService
    {
        Task<ErrorResponse> EditInstructor(EditInstructor Model, int InstructorID);
        Task<ReturnInstructorProfile> ReturnInstructorProfile(int InstructorID);
    }
    public class iServices:InstService
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly DBcontext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailServices;
        private readonly IWebHostEnvironment webHostEnvironment;

        public iServices(UserManager<IdentityUser> usermanager, DBcontext dbContext, IConfiguration configuration, IMailService mailServices, IWebHostEnvironment hostEnvironment)
        {
            _usermanager = usermanager;
            _dbContext = dbContext;
            _configuration = configuration;
            _mailServices = mailServices;
            webHostEnvironment = hostEnvironment;

        }
        private string UploadedFile(EditInstructor Model)
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
        private string UploadedFile2(EditInstructor Model)
        {
            string uniqueFileName = null;

            if (Model.cv != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Model.cv.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Model.cv.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<ErrorResponse> EditInstructor(EditInstructor Model, int InstructorID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("AddProfileDtaAsync View Is Null");
                }
                Instructor inst=_dbContext.Instructors.Find(InstructorID);
                if (inst == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No Instructor Have This ID"
                    };
                }

                string uniqueFileName = UploadedFile(Model);
                inst.logo = uniqueFileName;
                inst.InstructorName= Model.username;
                inst.cv = UploadedFile2(Model);

                var x = _dbContext.Instructors.Update(inst);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    var userID = inst.AppUserId;
                    var Identity = await _usermanager.FindByIdAsync(userID);
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

        public async Task<ReturnInstructorProfile> ReturnInstructorProfile(int InstructorID)
        {
            try
            {
                Instructor instructor = _dbContext.Instructors.Find(InstructorID);
                if (instructor == null)
                {
                    return new ReturnInstructorProfile
                    {
                        IsSuccess = false,
                        Message = "No instructor Found With This InstructorID"
                    };
                }
                List<CourseListData> courseLD = null;
                List<Course> courseListData = _dbContext.Courses.Select(s => s).Where(s => s.InstructorID == InstructorID).ToList();
                if (courseListData.Count != 0)
                {
                    courseLD = new List<CourseListData>();
                    foreach (var item in courseListData)
                    {
                        courseLD.Add(new CourseListData
                        {
                            Address = null,
                            CenterName = null,
                            CourseID = item.CourseID,
                            CourseName = item.CourseName,
                            InstructorName = instructor.InstructorName,
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
                
                IdentityUser identity = await _usermanager.FindByIdAsync(instructor.AppUserId);
                if (identity == null)
                {
                    return new ReturnInstructorProfile
                    {
                        IsSuccess = false,
                        Message = "This instructor Don't Belong To Any AppUserID",
                    };
                }
                ReturnInstructorProfile returnIP = new ReturnInstructorProfile()
                {
                    email = identity.Email,
                    logo = instructor.logo,
                    phone = identity.PhoneNumber,
                    userName = instructor.InstructorName,
                    cv = instructor.cv,
                    IsSuccess = true,
                    Message = "Data Returned Successfully",
                    myCourses = courseLD
                };
                return (returnIP);
            }

            catch (Exception ex)
            {
                return new ReturnInstructorProfile
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
