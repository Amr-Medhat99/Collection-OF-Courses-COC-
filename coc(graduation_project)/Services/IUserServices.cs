using coc_graduation_project_.Enums;
using coc_graduation_project_.Models;
using coc_graduation_project_.ViewModel;
using coc_graduation_project_.ViewModelRecieveData;
using coc_graduation_project_.ViewModelReturnData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace coc_graduation_project_.Services
{
    public interface IUserServices
    {
        Task<ErrorResponse> RegisterUserAsync(RegisterViewModel Model);
        Task<ErrorResponse> RegisterAdminAsync(RegisterAdmin Model);
        Task<ReturnOverLogin> LoginUserAsync(LoginViewModel Model);
        Task<ErrorResponse> ConfirmEmailAsync(string id, string token);
        Task<ErrorResponse> ForgetPasswordAsync(string email);
        Task<ErrorResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<ErrorResponse> AddProfileDtaAsync(EditStudent Model,int StudentID);
        Task<ReturnStudentProfile> ReturnStudentProfile(int StudentID);
    }
    public class UserServices : IUserServices
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly DBcontext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailServices;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserServices(UserManager<IdentityUser> usermanager, DBcontext dbContext, IConfiguration configuration, IMailService mailServices, IWebHostEnvironment hostEnvironment)
        {
            _usermanager = usermanager;
            _dbContext = dbContext;
            _configuration = configuration;
            _mailServices = mailServices;
            webHostEnvironment = hostEnvironment;
        }
        private string UploadedFile(EditStudent Model)
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
        public async Task<ErrorResponse> AddProfileDtaAsync(EditStudent Model, int StudentID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("AddProfileDtaAsync View Is Null");
                }
                var student = await _dbContext.Students.FindAsync(StudentID);
                if (student==null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No Student Have This ID"
                    };
                }
                string uniqueFileName =UploadedFile(Model);
                student.Picture=uniqueFileName;
                student.StudentName = Model.username;

                var x = _dbContext.Students.Update(student);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    var userID = student.AppUserId;
                    var Identity =await _usermanager.FindByIdAsync(userID);
                    Identity.PhoneNumber = Model.phone;
                    Identity.Email= Model.email;

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
                            Error = result.Errors.Select(s=>s.Description)
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

        //Register New User
        public async Task<ErrorResponse> RegisterUserAsync(RegisterViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Register View Is Null");
                }
                var user = await _usermanager.FindByEmailAsync(Model.Email);
                if (user != null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "This Email Is Already Have Account"
                    };
                }
                var IdentityUser = new IdentityUser
                {
                    Email = Model.Email,
                    UserName = Model.UserName,
                };
                var result = await _usermanager.CreateAsync(IdentityUser, Model.Password);
                if (result.Succeeded)
                {
                    var StudentModel = new Student
                    {
                        AppUserId = IdentityUser.Id
                    };
                    var x = _dbContext.Students.Add(StudentModel);
                    _dbContext.SaveChanges();
                    if (x.IsKeySet)
                    {
                        var InstructorModel = new Instructor
                        {
                            AppUserId = IdentityUser.Id
                        };
                        var I = _dbContext.Instructors.Add(InstructorModel);
                        _dbContext.SaveChanges();

                        if (I.IsKeySet)
                        {
                            var CenterModel = new Center
                            {
                                AppUserId = IdentityUser.Id
                            };
                            var C = _dbContext.Centers.Add(CenterModel);
                            _dbContext.SaveChanges();

                            if (C.IsKeySet)
                            {
                                await _usermanager.AddToRoleAsync(IdentityUser, "NormalRole");
                                var Cart = new Cart
                                {
                                    StudentId = StudentModel.StudentId
                                };
                                var CA = _dbContext.Carts.Add(Cart);
                                _dbContext.SaveChanges();

                                if (CA.IsKeySet)
                                {
                                    var WatchLaterModel = new WatchLater
                                    {
                                        studentID = StudentModel.StudentId
                                    };
                                    var W = _dbContext.watchLaters.Add(WatchLaterModel);
                                    _dbContext.SaveChanges();

                                    if (W.IsKeySet)
                                    {
                                        var CurrentCourseModel = new CurrentCourse
                                        {
                                            studentID = StudentModel.StudentId
                                        };
                                        var CC = _dbContext.CurrentCourses.Add(CurrentCourseModel);
                                        _dbContext.SaveChanges();
                                        if (CC.IsKeySet)
                                        {
                                            var FavoriteModel = new Favorite
                                            {
                                                studentID = StudentModel.StudentId
                                            };
                                            var F = _dbContext.Favorites.Add(FavoriteModel);
                                            _dbContext.SaveChanges();

                                            if (F.IsKeySet)
                                            {
                                                var confirmEmailToken = await _usermanager.GenerateEmailConfirmationTokenAsync(IdentityUser);
                                                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                                                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                                                string url = $"{_configuration["URL"]}/api/Auth/confirmemail?userid={IdentityUser.Id}&token={validEmailToken}";

                                                await _mailServices.SendEmailAsync(IdentityUser.Email, "Confirm Your Email", $"<h1>Welcome To Auth Demo</h1>" +
                                                    $"<p>Please Confirm Your Email By <a href='{url}'>Click Here</a></p>");

                                                return new ErrorResponse
                                                {
                                                    Message = "Created User Successfully",
                                                    IsSuccess = true
                                                };
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return new ErrorResponse
                    {
                        Message = "User Not Created Some Error Occer",
                        IsSuccess = false
                    };
                }
                else
                {
                    return new ErrorResponse
                    {
                        Message = "User Did Not Created",
                        IsSuccess = false,
                        Error = result.Errors.Select(s => s.Description)

                    };
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }

        public async Task<ErrorResponse> RegisterAdminAsync(RegisterAdmin Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Register View Is Null");
                }
                var user = await _usermanager.FindByEmailAsync(Model.Email);
                if (user != null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "This Email Is Already Have Account"
                    };
                }
                var IdentityUser = new IdentityUser
                {
                    Email = Model.Email,
                    UserName = Model.UserName,
                    PhoneNumber=Model.Phone
                };
                var result = await _usermanager.CreateAsync(IdentityUser, Model.Password);
                if (result.Succeeded)
                {
                    var admin = new Admin
                    {
                        AppUserId = IdentityUser.Id
                    };
                    var res = _dbContext.Admins.Add(admin);
                    _dbContext.SaveChanges();
                    if (res.IsKeySet)
                    {
                        await _usermanager.AddToRoleAsync(IdentityUser, "AdminRole");
                        var confirmEmailToken = await _usermanager.GenerateEmailConfirmationTokenAsync(IdentityUser);
                        var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                        var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                        string url = $"{_configuration["URL"]}/api/Auth/confirmemail?userid={IdentityUser.Id}&token={validEmailToken}";

                        await _mailServices.SendEmailAsync(IdentityUser.Email, "Confirm Your Email", $"<h1>Welcome To Auth Demo</h1>" +
                            $"<p>Please Confirm Your Email By <a href='{url}'>Click Here</a></p>");

                        return new ErrorResponse
                        {
                            Message = "Created User Successfully",
                            IsSuccess = true
                        };
                    }
                    return new ErrorResponse
                    {
                        Message = "Admin Email Not Created Please Try Again",
                        IsSuccess = false
                    };

                }         
                else
                {
                    return new ErrorResponse
                    {
                        Message = "Admin Did Not Created",
                        IsSuccess = false,
                        Error = result.Errors.Select(s => s.Description)

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


        public async Task<ReturnOverLogin> LoginUserAsync(LoginViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Login View Is Null");
                }
                //check email address
                var user = await _usermanager.FindByEmailAsync(Model.Email);
                if (user == null)
                {
                    return new ReturnOverLogin
                    {
                        IsSuccess = false,
                        Message = "There Is No User With That Email Address"
                    };
                }
                //check user password
                var result = await _usermanager.CheckPasswordAsync(user, Model.Password);
                if (!result)
                {
                    return new ReturnOverLogin
                    {
                        IsSuccess = false,
                        Message = "Invalid Password"
                    };
                }
                var userRole = await _usermanager.GetRolesAsync(user);
                //generate access token (JWT)
                bool adm = false;
                Claim[]claim = null;
                foreach (var item in userRole)
                {
                    claim = new[]
                    {
                    new Claim("Email",Model.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Role,item)
                    };
                    if (item=="AdminRole")
                    {
                        adm = true;
                    }
                }
                

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSetting:Key"]));

                var Token = new JwtSecurityToken(
                    issuer: _configuration["AuthSetting:Issuer"],
                    audience: _configuration["AuthSetting:Audience"],
                    claims: claim,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                string TokenAsString = new JwtSecurityTokenHandler().WriteToken(Token);

                //Student student = _dbContext.Students.FirstOrDefault(s => s.AppUserId == user.Id);
                
                if (adm==true)
                {
                    ReturnOverLogin rolAd = new ReturnOverLogin()
                    {
                        Message = TokenAsString,
                        IsSuccess = true,
                        ExpireDate = Token.ValidTo,
                        StudentID = 0,
                        CenterID = 0,
                        InstructorID = 0,
                        FavoriteID = 0,
                        CurrentCoursesID = 0,
                        WatchLaterID = 0,
                        CartID = 0,
                        admin=true
                    };
                    return (rolAd);
                }
                var student = _dbContext.Students.Where(x => x.AppUserId == user.Id).FirstOrDefault();
                var center = _dbContext.Centers.Where(x => x.AppUserId == user.Id).FirstOrDefault();
                var instructor = _dbContext.Instructors.Where(x => x.AppUserId == user.Id).FirstOrDefault();
                var favorite = _dbContext.Favorites.Where(x => x.studentID == student.StudentId).FirstOrDefault();
                var CurrentCourses = _dbContext.CurrentCourses.Where(x => x.studentID == student.StudentId).FirstOrDefault();
                var WatchLater = _dbContext.watchLaters.Where(x => x.studentID == student.StudentId).FirstOrDefault();
                var Cart = _dbContext.Carts.Where(x => x.StudentId == student.StudentId).FirstOrDefault();
                
                ReturnOverLogin rol = new ReturnOverLogin()
                {
                    Message = TokenAsString,
                    IsSuccess = true,
                    ExpireDate = Token.ValidTo,
                    StudentID = student.StudentId,
                    CenterID = center.CenterId,
                    InstructorID = instructor.InstructorId,
                    FavoriteID = favorite.FavoriteID,
                    CurrentCoursesID = CurrentCourses.CurrentCourseID,
                    WatchLaterID = WatchLater.WatchLaterId,
                    CartID = Cart.CartId,
                    admin=false
                };

                return (rol);
            }
            catch (Exception ex)
            {
                return new ReturnOverLogin
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
           
        }

        public async Task<ErrorResponse> ConfirmEmailAsync(string id, string token)
        {
            try
            {
                var user = await _usermanager.FindByIdAsync(id);
                if (user == null)
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "User not found"
                    };

                var decodedToken = WebEncoders.Base64UrlDecode(token);
                string normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _usermanager.ConfirmEmailAsync(user, normalToken);

                if (result.Succeeded)
                    return new ErrorResponse
                    {
                        Message = "Email confirmed successfully!",
                        IsSuccess = true,
                    };

                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Email did not confirm",
                    Error = result.Errors.Select(e => e.Description)
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

        public async Task<ErrorResponse> ForgetPasswordAsync(string email)
        {
            try
            {
                var user = await _usermanager.FindByEmailAsync(email);
                if (user == null)
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No user associated with email",
                    };

                var token = await _usermanager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);

                string url = $"{_configuration["URL"]}/ResetPassword?email={email}&token={validToken}";

                await _mailServices.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                    $"<p>To reset your password <a href='{url}'>Click here</a></p>");

                return new ErrorResponse
                {
                    IsSuccess = true,
                    Message = "Reset password URL has been sent to the email successfully!"
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

        public async Task<ErrorResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            try
            {
                var user = await _usermanager.FindByEmailAsync(model.Email);
                if (user == null)
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No user associated with email",
                    };

                if (model.NewPassword != model.ConfirmPassword)
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Password doesn't match its confirmation",
                    };

                var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
                string normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _usermanager.ResetPasswordAsync(user, normalToken, model.NewPassword);

                if (result.Succeeded)
                {
                    return new ErrorResponse
                    {
                        Message = "Password has been reset successfully!",
                        IsSuccess = true,
                    };
                }

                return new ErrorResponse
                {
                    Message = "Something went wrong",
                    IsSuccess = false,
                    Error = result.Errors.Select(e => e.Description),
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

        public async Task<ReturnStudentProfile> ReturnStudentProfile(int StudentID)
        {
            try
            {
                Student studentData = _dbContext.Students.Find(StudentID);
                if (studentData == null)
                {
                    return new ReturnStudentProfile
                    {
                        IsSuccess = false,
                        Message = "invalid StudentID"
                    };
                }
                IdentityUser identity = await _usermanager.FindByIdAsync(studentData.AppUserId);
                if (identity == null)
                {
                    return new ReturnStudentProfile
                    {
                        IsSuccess = false,
                        Message = "invalid StudentID"
                    };
                }
                Favorite favoriteData = _dbContext.Favorites.FirstOrDefault(s => s.studentID == StudentID);
                if (favoriteData == null)
                {
                    return new ReturnStudentProfile
                    {
                        IsSuccess = false,
                        Message = "invalid StudentID"
                    };
                }
                List<FavoriteSubCategory> favoriteSubCategoryData = _dbContext.FavoriteSubCategory.Select(s => s).Where(s => s.FavoriteID == favoriteData.FavoriteID).ToList();
                List<SubCategoryData> subCD=null;
                List<NewsListData> newsLD = null;
                List<CourseListData> CLD = null;
                if (favoriteSubCategoryData.Count != 0)
                {
                    subCD = new List<SubCategoryData>();
                    foreach (var item in favoriteSubCategoryData)
                    {
                        List<SubCategory> sc = _dbContext.SubCategorys.Select(s => s).Where(s => s.SubCategoryID == item.SubCategoryID).ToList();
                        if (sc == null)
                        {
                            return new ReturnStudentProfile
                            {
                                IsSuccess = false,
                                Message = "invalid SubCategoryID"
                            };
                        }
                        foreach (var item2 in sc)
                        {
                            subCD.Add(new SubCategoryData
                            {
                                SubCategoryID = item2.SubCategoryID,
                                SubCategoryName = item2.SubCategoryName,
                                SubCategoryLogo = item2.SubCategoryLogo
                            });
                        }
                    }


                    newsLD = new List<NewsListData>();
                    List<Course> cour = null;
                    foreach (var item in subCD)
                    {
                        cour = _dbContext.Courses.Select(s => s).Where(s => s.SubCategoryID == item.SubCategoryID).ToList();
                        if (cour.Count != 0)
                        {
                            foreach (var item2 in cour)
                            {
                                List<News> news = _dbContext.News.Select(s => s).Where(s => s.CourseID == item2.CourseID).ToList();
                                foreach (var item3 in news)
                                {
                                    if (item2.InstructorID == null)
                                    {
                                        Center c = _dbContext.Centers.Find(item2.CenterID);
                                        newsLD.Add(new NewsListData
                                        {
                                            id = item3.id,
                                            what_is_new = item3.what_is_new,
                                            modified_date = item3.modified_date,
                                            logo = item3.logo,
                                            instructor_name = null,
                                            center_name = c.CenterName,
                                            course_name = item2.CourseName,

                                        });
                                    }
                                    if (item2.CenterID == null)
                                    {
                                        Instructor i = _dbContext.Instructors.Find(item2.InstructorID);
                                        newsLD.Add(new NewsListData
                                        {
                                            id = item3.id,
                                            what_is_new = item3.what_is_new,
                                            modified_date = item3.modified_date,
                                            logo = item3.logo,
                                            instructor_name = i.InstructorName,
                                            center_name = null,
                                            course_name = item2.CourseName,
                                        });
                                    }

                                }

                            }
                        }
                    }
                }
                WatchLater wl = _dbContext.watchLaters.FirstOrDefault(s => s.studentID == StudentID);
                if (wl == null)
                {
                    return new ReturnStudentProfile
                    {
                        IsSuccess = false,
                        Message = "no found watchLaterID Belong TO This StudentID"
                    };
                }
                List<WatchLaterCourse> wLC = _dbContext.WatchLaterCourses.Select(s => s).Where(s => s.WatchLaterID == wl.WatchLaterId).ToList();
                if (wLC.Count != 0)
                {
                    CLD = new List<CourseListData>();
                    foreach (var item in wLC)
                    {
                        List<Course> course = _dbContext.Courses.Select(s => s).Where(s => s.CourseID == item.CourseID).ToList();
                        foreach (var item2 in course)
                        {
                            if (item2.CenterID == null)
                            {
                                Instructor i = _dbContext.Instructors.Find(item2.InstructorID);
                                CLD.Add(new CourseListData
                                {
                                    Address = null,
                                    CenterName = null,
                                    CourseID = item2.CourseID,
                                    CourseName = item2.CourseName,
                                    InstructorName = i.InstructorName,
                                    Logo = item2.Logo,
                                    Online = item2.Online,
                                    Options = item2.Options,
                                    Price = item2.Price,
                                    QA_Following = item2.QA_Following,
                                    RelasedDate = item2.RelasedDate,
                                    Stars = item2.Stars
                                });
                            }
                            if (item2.InstructorID == null)
                            {
                                Center c = _dbContext.Centers.Find(item2.CenterID);
                                if (item2.choice == "online")
                                {
                                    CLD.Add(new CourseListData
                                    {
                                        Address = null,
                                        CenterName = c.CenterName,
                                        CourseID = item2.CourseID,
                                        CourseName = item2.CourseName,
                                        InstructorName = null,
                                        Logo = item2.Logo,
                                        Online = item2.Online,
                                        Options = item2.Options,
                                        Price = item2.Price,
                                        QA_Following = item2.QA_Following,
                                        RelasedDate = item2.RelasedDate,
                                        Stars = item2.Stars
                                    });
                                }
                                if (item2.choice == "offline")
                                {
                                    CLD.Add(new CourseListData
                                    {
                                        Address = c.address,
                                        CenterName = c.CenterName,
                                        CourseID = item2.CourseID,
                                        CourseName = item2.CourseName,
                                        InstructorName = null,
                                        Logo = item2.Logo,
                                        Online = item2.Online,
                                        Options = item2.Options,
                                        Price = item2.Price,
                                        QA_Following = item2.QA_Following,
                                        RelasedDate = item2.RelasedDate,
                                        Stars = item2.Stars
                                    });
                                }

                            }

                        }
                    }
                }
                
                ReturnStudentProfile RSP = new ReturnStudentProfile
                {
                    email = identity.Email,
                    phone = identity.PhoneNumber,
                    Logo = studentData.Picture,
                    userName = studentData.StudentName,
                    favorites = subCD,
                    notifications = newsLD,
                    watchLater = CLD,
                    IsSuccess = true,
                    Message = "Data Returned Successfully"
                };
                return (RSP);
            }
            catch (Exception ex)
            {
                return new ReturnStudentProfile
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

    }
}