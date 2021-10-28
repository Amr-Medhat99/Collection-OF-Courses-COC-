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
    public interface ICourseService
    {
        List<SubCategory> s();
        List<Component> c();
        Task<ReturnCourseDetails> ReturnCourseDetailsAdmin(int CourseID);
        List<CourseListData> SelectCourses();
        ListOfCourseForAdmin RejectCourse(int CourseID);
        ListOfCourseForAdmin AcceptCourse(int CourseID);
        ListOfCourseForAdmin RetrunListOfCources();
        ErrorResponse AddComment(AddCommentViewModel Model, int studentID, int videoID);
        ReturnListOfCoursesAndListNews RetrunListOfCourcesAndNews(int SubCategoryID, string choice);
        ReturnListChannel ReturnListChannel(int SubCategoryID);
        ErrorResponse AddNewChannel(AddNewChannel Model);
        ErrorResponse AddQAndAVideo(AddMediaViewModel model, int videoID);
        ErrorResponse AddMainCategory(AddCategoryViewModel Model);
        ReturnMainCategory ReturnMainCategory();
        ReturnSubCategory ReturnSubCategory(int MainCategoryID);
        ErrorResponse AddSubCategory(AddSubCategoryViewModel Model, int MainCategoryID);
        CourseErrorResponse AddNewCourse(int centerID, int instructorID, int subCategoryID, AddCourse model);
        ErrorResponse AddPackage(AddPackageViewModel Model, int CourseID);
        ErrorResponse AddComponent(AddComponent Model, int packageID);
        ErrorResponse AddVideo(AddVideo Model, int componetID);
        ErrorResponse AddFreeVideo( AddVideo Model, int componetID);
        ErrorResponse AddCourseToWatchLater(int CourseID, int WatchLaterID);
        ErrorResponse AddSubCategoryToFavorite(int subCategoryID, int favoriteID);
        ErrorResponse AddPackageToCart(int CartID,Root x);
        ErrorResponse AddPackageToCurrentCourse(int CurrentCoursesID,Root x,int cartID);
        ErrorResponse DeleteSubCategoryFromFavorite(int favoriteID, int subCategoryID);///android
        ErrorResponse DeleteCourseFromWatchLater(int watchLaterID, int courseID);///android
        ReturnPackageData ReturnPackage(int CourseID);
        ErrorResponse RemovePackageFromCart(int CartID, Root x);
        RootCurrentCourse ReturnCurrentCourses(int CurrentCoursesID);
        Task<ReturnCourseDetails> ReturnCourseDetails(int CourseID);
        ReturnCartData ReturnCartData(int CartID);
        ReturnWatchLaterData ReturnWatchLateCourses(int WatchLaterID);
        ErrorResponse AddPackageToCartWeb(int CartID, int packageID);//web
        ReturnCartData RemovePackageFromCartWeb(int CartID,int packageID); //web
        ErrorResponse AddPackageToCurrentCourseWeb(int CurrentCoursesID, int packageID, int cartID); //web
        SubCategoryDataList ReturnSubCategoryList(int favoriteID); //web
        SubCategoryDataList DeleteSubCategoryFromFavoriteWeb(int favoriteID, int subCategoryID);//web
        ReturnWatchLaterData DeleteCourseFromWatchLaterWeb(int watchLaterID, int courseID);//web

    }
    public class CourseService : ICourseService
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly DBcontext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailServices;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CourseService(UserManager<IdentityUser> usermanager, DBcontext dbContext, IConfiguration configuration, IMailService mailServices, IWebHostEnvironment hostEnvironment)
        {
            _usermanager = usermanager;
            _dbContext = dbContext;
            _configuration = configuration;
            _mailServices = mailServices;
            webHostEnvironment = hostEnvironment;
        }
        public List<Component> c()
        {
            List<Component> com= _dbContext.Components.Select(s => s).ToList();
            return (com);
        }
        public List<SubCategory> s()
        {
            List<SubCategory> su = _dbContext.SubCategorys.Select(s => s).ToList();
            return (su);
        }
        public List<CourseListData> SelectCourses()
        {
            List<Course> c = _dbContext.Courses.Select(s => s).Where(s=>s.status==true).ToList();
            List<CourseListData> CLD = new List<CourseListData>();
            foreach (var item in c)
            {
                if (item.CenterID==null)
                {
                    Instructor ins = _dbContext.Instructors.Find(item.InstructorID);
                    CLD.Add(new CourseListData 
                    {
                        Address=null,
                        CenterName=null,
                        CourseID=item.CourseID,
                        CourseName=item.CourseName,
                        InstructorName=ins.InstructorName,
                        Logo=item.Logo,
                        Online=item.Online,
                        Options=item.Options,
                        Price=item.Price,
                        QA_Following=item.QA_Following,
                        RelasedDate=item.RelasedDate,
                        Stars=item.Stars
                    });
                }
                if (item.InstructorID == null)
                {
                    Center cen= _dbContext.Centers.Find(item.CenterID);
                    if (item.Online==true)
                    {
                        CLD.Add(new CourseListData
                        {
                            Address = null,
                            CenterName = cen.CenterName,
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
                    if (item.Online == false)
                    {
                        CLD.Add(new CourseListData
                        {
                            Address = cen.address,
                            CenterName = cen.CenterName,
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
            }
            return (CLD);
        }
        private string UploadedFile(AddSubCategoryViewModel Model)
        {
            string uniqueFileName = null;

            if (Model.SubCategoryLogo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Model.SubCategoryLogo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Model.SubCategoryLogo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string UploadedFile(AddNewChannel Model)
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
        private string UploadedFile(AddCategoryViewModel Model)
        {
            string uniqueFileName = null;

            if (Model.MainCategoryLogo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Model.MainCategoryLogo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Model.MainCategoryLogo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string UploadedFile(AddCourse Model)
        {
            string uniqueFileName = null;

            if (Model.logoo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Model.logoo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Model.logoo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string UploadedFile(AddVideo Model)
        {
            string uniqueFileName = null;

            if (Model.videoURL != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Model.videoURL.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Model.videoURL.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string UploadedFile(AddMediaViewModel Model)
        {
            string uniqueFileName = null;

            if (Model.videoURL != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Model.videoURL.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Model.videoURL.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public ListOfCourseForAdmin RejectCourse(int CourseID)
        {
            try
            {
                Course course = _dbContext.Courses.Find(CourseID);
                if (course == null)
                {
                    return new ListOfCourseForAdmin
                    {
                        IsSuccess = false,
                        Message = "inValid CourseID"
                    };
                }
                var res = _dbContext.Courses.Remove(course);
                _dbContext.SaveChanges();
                if (res.IsKeySet)
                {
                    ListOfCourseForAdmin lcf = RetrunListOfCources();
                    if (lcf.IsSuccess)
                    {
                        return new ListOfCourseForAdmin
                        {
                            c_list = lcf.c_list,
                            Message = "Deleted Course Successfully",
                            IsSuccess = true
                        };
                    }
                }
                return new ListOfCourseForAdmin
                {
                    Message = "Ann Error Occer please try again",
                    IsSuccess = false
                };
            }
            catch (Exception ex)
            {
                return new ListOfCourseForAdmin
                {
                    Message=ex.Message,
                    IsSuccess=false
                };
            }
        }

        public ListOfCourseForAdmin AcceptCourse(int CourseID)
        {
            try
            {
                Course course = _dbContext.Courses.Find(CourseID);
                if (course == null)
                {
                    return new ListOfCourseForAdmin
                    {
                        IsSuccess = false,
                        Message = "inValid CourseID"
                    };
                }
                course.status = true;
                var res = _dbContext.Courses.Update(course);
                _dbContext.SaveChanges();
                if (res.IsKeySet)
                {
                    ListOfCourseForAdmin lcf = RetrunListOfCources();
                    if (lcf.IsSuccess)
                    {
                        return new ListOfCourseForAdmin
                        {
                            c_list = lcf.c_list,
                            Message = "Updated Successfully",
                            IsSuccess = true
                        };
                    }
                }
                return new ListOfCourseForAdmin
                {
                    Message = "Ann Error Occer please try again",
                    IsSuccess = false
                };
            }
            catch (Exception ex)
            {
                return new ListOfCourseForAdmin
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }


        public ListOfCourseForAdmin RetrunListOfCources()
        {
            try
            {
                List<Course> lcourse = _dbContext.Courses.Select(s => s).Where(s => s.status == false).ToList();
                if (lcourse.Count == 0)
                {
                    return new ListOfCourseForAdmin
                    {
                        c_list = null,
                        Message = "No Courses Require To Accepted For Uploaded",
                        IsSuccess = true
                    };
                }
                ListOfCourseForAdmin lca = new ListOfCourseForAdmin();
                lca.c_list = new List<CourseListData>();
                foreach (var item in lcourse)
                {
                    if (item.InstructorID == null)
                    {
                        Center cen = _dbContext.Centers.Find(item.CenterID);
                        if (item.Online == true)
                        {
                            lca.c_list.Add(new CourseListData
                            {
                                Stars = item.Stars,
                                RelasedDate = item.RelasedDate,
                                QA_Following = item.QA_Following,
                                Price = item.Price,
                                Options = item.Options,
                                Online = item.Online,
                                Logo = item.Logo,
                                CourseID = item.CourseID,
                                CourseName = item.CourseName,
                                InstructorName = null,
                                Address = null,
                                CenterName = cen.CenterName
                            });
                        }

                        if (item.Online == false)
                        {
                            lca.c_list.Add(new CourseListData
                            {
                                Stars = item.Stars,
                                RelasedDate = item.RelasedDate,
                                QA_Following = item.QA_Following,
                                Price = item.Price,
                                Options = item.Options,
                                Online = item.Online,
                                Logo = item.Logo,
                                CourseID = item.CourseID,
                                CourseName = item.CourseName,
                                InstructorName = null,
                                Address = cen.address,
                                CenterName = cen.CenterName
                            });
                        }
                    }

                    if (item.CenterID == null)
                    {
                        Instructor ins = _dbContext.Instructors.Find(item.InstructorID);
                        lca.c_list.Add(new CourseListData
                        {
                            Stars = item.Stars,
                            RelasedDate = item.RelasedDate,
                            QA_Following = item.QA_Following,
                            Price = item.Price,
                            Options = item.Options,
                            Online = item.Online,
                            Logo = item.Logo,
                            CourseID = item.CourseID,
                            CourseName = item.CourseName,
                            InstructorName = ins.InstructorName,
                            Address = null,
                            CenterName = null
                        });
                    }
                }
                lca.Message = "Data Returned Successfully";
                lca.IsSuccess = true;
                return (lca);
            }
            catch (Exception ex)
            {
                return new ListOfCourseForAdmin
                {
                    Message=ex.Message,
                    IsSuccess=false
                };
            }
        }
        public CourseErrorResponse AddNewCourse(int centerID, int instructorID, int subCategoryID, AddCourse model)
        {
            try
            {
                if (model == null)
                {
                    throw new NullReferenceException();
                }
                Instructor ins = null;
                Center cen = null;
                Course course = null;
                if (instructorID == 0)
                {
                    cen = _dbContext.Centers.Find(centerID);
                    if (model.choice == "online")
                    {
                        course = new Course
                        {
                            Logo = "515bae15-93a9-4600-ba99-85f9b96f65b1_DeepLearning.jpeg",
                            choice = model.choice,
                            CourseName = model.courseName,
                            Description = model.description,
                            Online = true,
                            Options = model.options,
                            Price = model.price,
                            RelasedDate = DateTime.Now.ToString(),
                            Requirements = model.requirements,
                            SubCategoryID =subCategoryID,
                            CenterID =centerID,
                            InstructorID = null,
                            Stars=0,
                            status=false
                        };
                    }
                    if (model.choice == "offline")
                    {
                        course = new Course
                        {
                            Logo = "515bae15-93a9-4600-ba99-85f9b96f65b1_DeepLearning.jpeg",
                            choice = model.choice,
                            CourseName = model.courseName,
                            Description = model.description,
                            Online = false,
                            Options = model.options,
                            Price = model.price,
                            RelasedDate = DateTime.Now.ToString(),
                            Requirements = model.requirements,
                            SubCategoryID =subCategoryID,
                            CenterID =centerID,
                            InstructorID = null,
                            Stars = 0,
                            status = false
                        };
                    }

                }
                if (centerID == 0)
                {
                    ins = _dbContext.Instructors.Find(instructorID);
                    course = new Course
                    {
                        Logo = "515bae15-93a9-4600-ba99-85f9b96f65b1_DeepLearning.jpeg",
                        choice = "online",
                        CourseName = model.courseName,
                        Description = model.description,
                        Online = true,
                        Options = model.options,
                        Price = model.price,
                        RelasedDate = DateTime.Now.ToString(),
                        Requirements = model.requirements,
                        SubCategoryID = subCategoryID,
                        CenterID = null,
                        InstructorID =instructorID,
                        Stars=0,
                        status = false
                    };
                }
                var result = _dbContext.Courses.Add(course);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    var cou = _dbContext.Courses.Find(course.CourseID);
                    var news = new News
                    {
                        logo=UploadedFile(model),
                        modified_date=Convert.ToString(DateTime.Now),
                        what_is_new="Add New Course",
                        CourseID=cou.CourseID
                    };

                    var res = _dbContext.News.Add(news);
                    _dbContext.SaveChanges();
                    if (res.IsKeySet)
                    {
                        return new CourseErrorResponse
                        {
                            courseID=course.CourseID,
                            IsSuccess = true,
                            Message = "course and News added successfully"
                        };
                    }
                    return new CourseErrorResponse
                    {
                        courseID = course.CourseID,
                        IsSuccess = false,
                        Message = "News Of this Course Not added Successfully Please Try again"
                    };
                }
                return new CourseErrorResponse
                {
                    courseID = course.CourseID,
                    IsSuccess = false,
                    Message = "some error occuer please try again"
                };
            }
            catch (Exception ex)
            {
                return new CourseErrorResponse
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            } 
        }
        public ReturnListChannel ReturnListChannel(int SubCategoryID)
        {
            try
            {
                List<HelpFullChannel> channel = _dbContext.HelpFullChannels.Select(s => s).Where(s => s.SubCategoryID == SubCategoryID).ToList();
                ReturnListChannel rlc = new ReturnListChannel();
                rlc.lista = new List<ChannelData>();
                foreach (var item in channel)
                {
                    rlc.lista.Add(new ChannelData
                    {
                        ChannelID = item.ID,
                        ChannelName = item.channelName,
                        CourseName = item.courseName,
                        Link = item.Link,
                        Logo = item.logo
                    });
                }
                rlc.IsSuccess = true;
                rlc.Message = "successfully";
                return (rlc);
            }
            catch (Exception ex)
            {
                return new ReturnListChannel
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
                
            }
        }
        public ErrorResponse AddComment(AddCommentViewModel Model, int studentID,int videoID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Comment View Is Null");
                }
                var Comment = new Comment
                {
                    StudentID = studentID,
                    VideoID = videoID,
                    understand_rate = Model.understand_Rate,
                    missing_explain = Model.missing_Explain,
                    missing_answers = Model.missing_Answer
                };
                var x = _dbContext.Comments.Add(Comment);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Comment Add Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Comment Not Added Some Error Occuer Please Try Again"
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

        public ReturnListOfCoursesAndListNews RetrunListOfCourcesAndNews(int SubCategoryID, string choice)
        {
            try
            {
                SubCategory sub = _dbContext.SubCategorys.Find(SubCategoryID);
                if (sub==null)
                {
                    return new ReturnListOfCoursesAndListNews
                    {
                        IsSuccess = false,
                        Message = "invalid SubCategory"
                    };
                }
                if (choice == "")
                {
                    return new ReturnListOfCoursesAndListNews
                    {
                        IsSuccess = false,
                        Message = "invalid Choice"
                    };
                }
                int numberOfUser=0;
                float commentRate=0;
                float average = 0;
                List<CourseListData> s = new List<CourseListData>();
                List<Course> courseList=null;
                ReturnListOfCoursesAndListNews cna = new ReturnListOfCoursesAndListNews();
                if (choice=="online")
                {
                    courseList = _dbContext.Courses.Select(s => s).Where(s => s.SubCategoryID == SubCategoryID && s.choice == "online"&&s.status==true).ToList();
                    if (courseList.Count == 0)
                    {
                        return new ReturnListOfCoursesAndListNews
                        {
                            IsSuccess = false,
                            Message= "you enter invalid parameters(SubCategoryID,choice)"
                        };
                    }
                    foreach (var item in courseList)
                    {
                        numberOfUser =0;
                        commentRate =0;
                        List<Package> pac = _dbContext.Package.Select(s => s).Where(s => s.CourseID == item.CourseID).ToList();
                        foreach (var item2 in pac)
                        {
                            List<Component> com = _dbContext.Components.Select(s => s).Where(s => s.PackageID == item2.PackageID).ToList();
                            foreach (var item3 in com)
                            {
                                List<media> med = _dbContext.Medias.Select(s => s).Where(s => s.ComponentID == item3.ComponentID).ToList();
                                foreach (var item4 in med)
                                {
                                    List<Comment> commData = _dbContext.Comments.Select(s => s).Where(s => s.VideoID == item4.MediaID).ToList();
                                    if (commData.Count==0)
                                    {
                                        average = 0;
                                        break;
                                    }
                                    numberOfUser += commData.Count;
                                    foreach (var item5 in commData)
                                    {
                                        commentRate += item5.understand_rate;
                                    }
                                }
                            }
                        }
                        if (commentRate == 0 && numberOfUser == 0)
                        {
                            average = 0;
                        }
                        else
                        {
                            average = commentRate / numberOfUser;
                        }
                        if (item.CenterID == null)
                        {
                            Instructor i = _dbContext.Instructors.Find(item.InstructorID);
                            s.Add(new CourseListData
                            {
                                CourseID = item.CourseID,
                                CourseName = item.CourseName,
                                InstructorName = i.InstructorName,
                                CenterName = null,
                                Logo = item.Logo,
                                Online = item.Online,
                                Options = item.Options,
                                Price = item.Price,
                                QA_Following = item.QA_Following,
                                RelasedDate = item.RelasedDate,
                                Stars = average
                            });
                        }
                        if (item.InstructorID == null)
                        {
                            Center i = _dbContext.Centers.Find(item.CenterID);
                            s.Add(new CourseListData
                            {
                                CourseID = item.CourseID,
                                Address = i.address,
                                CourseName = item.CourseName,
                                CenterName = i.CenterName,
                                InstructorName = null,
                                Logo = item.Logo,
                                Online = item.Online,
                                Options = item.Options,
                                Price = item.Price,
                                QA_Following = item.QA_Following,
                                RelasedDate = item.RelasedDate,
                                Stars = average
                            });
                        }

                        Course c = item;
                        c.Stars= average;
                        var x= _dbContext.Courses.Update(c);
                        _dbContext.SaveChanges();
                    }

                    //cna.c_list = s;
                    //cna.IsSuccess = true;
                    //cna.Message = "data returned successfully";
                    //return (cna);
                }
                if (choice=="offline")
                {
                    courseList = _dbContext.Courses.Select(s => s).Where(s => s.SubCategoryID == SubCategoryID && s.choice == "offline" && s.status == true).ToList();
                    //ReturnListOfCoursesAndListNews cna = new ReturnListOfCoursesAndListNews();
                    if (courseList.Count == 0)
                    {
                        return new ReturnListOfCoursesAndListNews
                        {
                            IsSuccess = false,
                            Message = "you enter invalid parameters(SubCategoryID,choice)"
                        };
                    }
                    foreach (var item in courseList)
                    {
                        numberOfUser = 0;
                        commentRate = 0;
                        List<Package> pac = _dbContext.Package.Select(s => s).Where(s => s.CourseID == item.CourseID).ToList();
                        foreach (var item2 in pac)
                        {
                            List<Component> com = _dbContext.Components.Select(s => s).Where(s => s.PackageID == item2.PackageID).ToList();
                            foreach (var item3 in com)
                            {
                                List<media> med = _dbContext.Medias.Select(s => s).Where(s => s.ComponentID == item3.ComponentID).ToList();
                                foreach (var item4 in med)
                                {
                                    List<Comment> commData = _dbContext.Comments.Select(s => s).Where(s => s.VideoID == item4.MediaID).ToList();
                                    if (commData.Count == 0)
                                    {
                                        average = 0;
                                        break;
                                    }
                                    numberOfUser += commData.Count;
                                    foreach (var item5 in commData)
                                    {
                                        commentRate += item5.understand_rate;
                                    }
                                }
                            }
                        }
                        if (commentRate == 0 && numberOfUser == 0)
                        {
                            average = 0;
                        }
                        else
                        {
                            average = commentRate / numberOfUser;
                        }
                        if (item.CenterID!=null)
                        {
                            Center i = _dbContext.Centers.Find(item.CenterID);
                            s.Add(new CourseListData
                            {
                                CourseID = item.CourseID,
                                Address = i.address,
                                CourseName = item.CourseName,
                                CenterName = i.CenterName,
                                InstructorName = null,
                                Logo = item.Logo,
                                Online = item.Online,
                                Options = item.Options,
                                Price = item.Price,
                                QA_Following = item.QA_Following,
                                RelasedDate = item.RelasedDate,
                                Stars = average
                            });
                        }
                        Course c = item;
                        c.Stars = average;
                        var x = _dbContext.Courses.Update(c);
                        _dbContext.SaveChanges();
                    }
                    //cna.c_list = s;
                    //cna.IsSuccess = true;
                    //cna.Message = "data returned successfully";
                    //return (cna);
                }
                if (choice == "all")
                {
                    courseList = _dbContext.Courses.Select(s => s).Where(s => s.SubCategoryID == SubCategoryID && s.status == true).ToList();
                    //ReturnListOfCoursesAndListNews cna = new ReturnListOfCoursesAndListNews();
                    if (courseList.Count == 0)
                    {
                        return new ReturnListOfCoursesAndListNews
                        {
                            IsSuccess = false,
                            Message = "you enter invalid parameters(SubCategoryID,choice)"
                        };
                    }
                    foreach (var item in courseList)
                    {
                        numberOfUser = 0;
                        commentRate = 0;
                        List<Package> pac = _dbContext.Package.Select(s => s).Where(s => s.CourseID == item.CourseID).ToList();
                        foreach (var item2 in pac)
                        {
                            List<Component> com = _dbContext.Components.Select(s => s).Where(s => s.PackageID == item2.PackageID).ToList();
                            foreach (var item3 in com)
                            {
                                List<media> med = _dbContext.Medias.Select(s => s).Where(s => s.ComponentID == item3.ComponentID).ToList();
                                foreach (var item4 in med)
                                {
                                    List<Comment> commData = _dbContext.Comments.Select(s => s).Where(s => s.VideoID == item4.MediaID).ToList();
                                    if (commData.Count == 0)
                                    {
                                        average = 0;
                                        break;
                                    }
                                    numberOfUser += commData.Count;
                                    foreach (var item5 in commData)
                                    {
                                        commentRate += item5.understand_rate;
                                    }
                                }
                            }
                        }
                        if (commentRate == 0 && numberOfUser == 0)
                        {
                            average = 0;
                        }
                        else
                        { average = commentRate / numberOfUser; }
                        if (item.CenterID == null)
                        {
                            Instructor i = _dbContext.Instructors.Find(item.InstructorID);
                            s.Add(new CourseListData
                            {
                                CourseID = item.CourseID,
                                //Address = item.Address,
                                CourseName = item.CourseName,
                                InstructorName = i.InstructorName,
                                CenterName = null,
                                Logo = item.Logo,
                                Online = item.Online,
                                Options = item.Options,
                                Price = item.Price,
                                QA_Following = item.QA_Following,
                                RelasedDate = item.RelasedDate,
                                Stars = average
                            });
                        }
                        if (item.InstructorID == null)
                        {
                            Center i = _dbContext.Centers.Find(item.CenterID);
                            s.Add(new CourseListData
                            {
                                CourseID = item.CourseID,
                                Address = i.address,
                                CourseName = item.CourseName,
                                CenterName = i.CenterName,
                                InstructorName = null,
                                Logo = item.Logo,
                                Online = item.Online,
                                Options = item.Options,
                                Price = item.Price,
                                QA_Following = item.QA_Following,
                                RelasedDate = item.RelasedDate,
                                Stars = average

                            });
                        }
                        Course c = item;
                        c.Stars = average;
                        var x = _dbContext.Courses.Update(c);
                        _dbContext.SaveChanges();
                    }

                    //cna.c_list = s;
                    //cna.IsSuccess = true;
                    //cna.Message = "data returned successfully";
                    //return (cna);
                }

                List<NewsListData> n = new List<NewsListData>();
                List<News> news = new List<News>();
                foreach (var item in courseList)
                {
                    news=_dbContext.News.Select(s=>s).Where(s => s.CourseID == item.CourseID).ToList();
                    foreach (var item2 in news)
                    {
                        if (item.InstructorID==null)
                        {
                            Center c = _dbContext.Centers.Find(item.CenterID);
                            n.Add(new NewsListData
                            {
                                id = item2.id,
                                logo = item2.logo,
                                modified_date = item2.modified_date,
                                what_is_new = item2.what_is_new,
                                center_name =c.CenterName,
                                course_name = item.CourseName,
                                instructor_name =null
                            });
                        }

                        if (item.CenterID == null)
                        {
                            Instructor i = _dbContext.Instructors.Find(item.InstructorID);
                            n.Add(new NewsListData
                            {
                                id = item2.id,
                                logo = item2.logo,
                                modified_date = item2.modified_date,
                                what_is_new = item2.what_is_new,
                                center_name = null,
                                course_name = item.CourseName,
                                instructor_name = i.InstructorName
                            });
                        }

                    }

                }

                cna.n_list = n;
                cna.c_list = s;
                cna.IsSuccess = true;
                cna.Message = "data returned successfully";
                return (cna);

                //return new ReturnListOfCoursesAndListNews
                //{
                //    IsSuccess = false,
                //    Message = "You enter invalid choice"
                //};
            }
            catch (Exception ex)
            {
                return new ReturnListOfCoursesAndListNews
                {
                    Message = ex.Message,
                    IsSuccess = false
                };

            }
        }
        public ErrorResponse AddNewChannel(AddNewChannel Model)
        {
            try
            {
                if (Model==null)
                {
                    throw new NullReferenceException("this model is null");
                }
                string logo = UploadedFile(Model);
                var channel = new HelpFullChannel
                {
                    channelName=Model.ChannelName,
                    courseName=Model.CourseName,
                    Link=Model.Link,
                    logo=logo,
                    SubCategoryID=Model.SubCategoryID
                };
                var result = _dbContext.HelpFullChannels.Add(channel);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Channel Added Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message ="an error occuer please try again"
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

        public ErrorResponse AddQAndAVideo(AddMediaViewModel model, int videoID)
        {
            try
            {
                if (model == null)
                {
                    throw new NullReferenceException("QAndA Video View Is Null");
                }
                var QAndAVideo = new QAndAVideo
                {
                    VideoName = model.videoName,
                    VideoURL = UploadedFile(model),
                    VideoID = videoID
                };
                var x = _dbContext.QandAVideos.Add(QAndAVideo);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "QAndA Video Add Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "QAndA Video Not Added Some Error Occuer Please Try Again"
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
        //add main category
        public ErrorResponse AddMainCategory(AddCategoryViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Category View Is Null");
                }
                string uniqueFileName = UploadedFile(Model);
                var MainCategory = new MainCategory
                {
                    MainCategoryName = Model.MainCategoryName,
                    MainCategoryLogo = uniqueFileName
                };
                var x = _dbContext.MainCategorys.Add(MainCategory);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Main Category Video Add Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Main Category Not Added Some Error Occuer Please Try Again"
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

        //return all mainCategory
        public ReturnMainCategory ReturnMainCategory()
        {
            try
            {
                List<MainCategory> mainCategory = _dbContext.MainCategorys.Select(s => s).ToList();
                List<News> news = _dbContext.News.Select(s => s).ToList();
                ReturnMainCategory rmc = new ReturnMainCategory();
                rmc.lista = new List<MainCategory>();
                rmc.lista = mainCategory;
                List<NewsListData> nld = new List<NewsListData>();
                foreach (var item in news)
                {
                    Course course = _dbContext.Courses.Find(item.CourseID);
                    if (course.CenterID == null)
                    {
                        Instructor inst = _dbContext.Instructors.Find(course.InstructorID);
                        nld.Add(new NewsListData
                        {
                            id = item.id,
                            course_name = course.CourseName,
                            instructor_name = inst.InstructorName,
                            center_name = null,
                            logo = item.logo,
                            modified_date = item.modified_date,
                            what_is_new = item.what_is_new
                        });
                    }
                    if (course.InstructorID == null)
                    {
                        Center cen = _dbContext.Centers.Find(course.CenterID);
                        nld.Add(new NewsListData
                        {
                            id = item.id,
                            course_name = course.CourseName,
                            center_name = cen.CenterName,
                            instructor_name = null,
                            logo = item.logo,
                            modified_date = item.modified_date,
                            what_is_new = item.what_is_new
                        });
                    }
                }
                rmc.n_list = nld;
                rmc.Message = "succesfully";
                rmc.IsSuccess = true;
                return (rmc);
            }

            catch (Exception ex)
            {
                return new ReturnMainCategory
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        } 

        //return SubCategory
        public ReturnSubCategory ReturnSubCategory(int MainCategoryID)
        {
            try
            {
                List<SubCategory> subCategory = _dbContext.SubCategorys.Select(s => s).Where(s => s.MainCategoryID == MainCategoryID).ToList();
                List<Course> course = new List<Course>();
                ReturnSubCategory rmc=new ReturnSubCategory();

                if (subCategory.Count>0)
                {
                    List<SubCategoryData> ccd = new List<SubCategoryData>();
                    List<NewsListData> n = new List<NewsListData>();
                    foreach (var item in subCategory)
                    {
                        //rmc = new ReturnSubCategory();
                        course = _dbContext.Courses.Select(s => s).Where(s => s.SubCategoryID == item.SubCategoryID).ToList();
                        List<News> news = new List<News>();
                        foreach (var item2 in course)
                        {
                            news = _dbContext.News.Select(s => s).Where(s => s.CourseID == item2.CourseID).ToList();
                            foreach (var item3 in news)
                            {
                                
                                Course co = _dbContext.Courses.Find(item3.CourseID);
                                Center cen;
                                Instructor ins;
                                if (co.InstructorID == null)
                                {
                                    cen = _dbContext.Centers.Find(co.CenterID);
                                    n.Add(new NewsListData
                                    {
                                        id = item3.id,
                                        logo = item3.logo,
                                        what_is_new = item3.what_is_new,
                                        modified_date = item3.modified_date,
                                        center_name = cen.CenterName,
                                        instructor_name = null,
                                        course_name = co.CourseName
                                    });
                                }
                                if (co.CenterID == null)
                                {
                                    ins = _dbContext.Instructors.Find(co.InstructorID);
                                    n.Add(new NewsListData
                                    {
                                        id = item3.id,
                                        logo = item3.logo,
                                        what_is_new = item3.what_is_new,
                                        modified_date = item3.modified_date,
                                        center_name = null,
                                        instructor_name = ins.InstructorName,
                                        course_name = co.CourseName
                                    });
                                }
                            }
                        }
                        ccd.Add(new SubCategoryData 
                        {
                            SubCategoryID=item.SubCategoryID,
                            SubCategoryLogo=item.SubCategoryLogo,
                            SubCategoryName=item.SubCategoryName
                        });
                    }
                    rmc.n_list = n;
                    rmc.lista = new List<SubCategoryData>();
                    rmc.lista = ccd;
                    rmc.Message = "succesfully";
                    rmc.IsSuccess = true;
                    return (rmc);
                }
                return new ReturnSubCategory
                {
                    IsSuccess = false,
                    Message = "you enter invalid mainCategoryID"
                };

            }
            catch (Exception ex)
            {
                return new ReturnSubCategory
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public ErrorResponse AddSubCategory(AddSubCategoryViewModel Model,int MainCategoryID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Category View Is Null");
                }

                var SubCategory = new SubCategory
                {
                    SubCategoryName = Model.SubCategoryName,
                    SubCategoryLogo = UploadedFile(Model),
                    MainCategoryID = MainCategoryID
                };
                var x = _dbContext.SubCategorys.Add(SubCategory);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Sub Category Added Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Sub Category Not Added Some Error Occuer Please Try Again"
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
        public ErrorResponse AddPackage(AddPackageViewModel Model, int CourseID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Package View Is Null");
                }
                var Package = new Package
                {
                    PackageCost = Model.packageCost,
                    PackageNumber = Model.packageNumber,
                    CourseID = CourseID
                };
                var x = _dbContext.Package.Add(Package);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Package Add Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Package Not Added"
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
        public ErrorResponse AddComponent(AddComponent Model, int packageID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Package View Is Null");
                }
                var component = new Component
                {
                    ComponentName=Model.componentName,
                    PackageID = packageID
                };
                var x = _dbContext.Components.Add(component);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Component Add Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Component Not Added"
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
        public ErrorResponse AddVideo(AddVideo Model, int componetID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Package View Is Null");
                }
                var video = new media
                {
                    VideoName=Model.videoName,
                    VideoURL= "001aa140-ba8b-4042-b4df-a40350a3d0ff_smallVideo.mp4",
                    ComponentID=componetID
                };
                var x = _dbContext.Medias.Add(video);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Video Add Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Video Not Added"
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
        public ErrorResponse AddFreeVideo(AddVideo Model, int componetID)
        {
            try
            {
                if (Model == null)
                {
                    throw new NullReferenceException("Package View Is Null");
                }
                var freevideo = new FreeVideo
                {
                    FreeVideoName = Model.videoName,
                    FreeVideoURL = "001aa140-ba8b-4042-b4df-a40350a3d0ff_smallVideo.mp4",
                    ComponentID = componetID
                };
                var x = _dbContext.FreeVideos.Add(freevideo);
                _dbContext.SaveChanges();
                if (x.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "FreeVideo Add Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "FreeVideo Not Added"
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

        public ErrorResponse AddCourseToWatchLater(int CourseID, int WatchLaterID)
        {
            try
            {
                Course course = _dbContext.Courses.Find(CourseID);
                if (course==null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "no course with this CourseID"
                    };
                }
                WatchLater watchlater = _dbContext.watchLaters.Find(WatchLaterID);
                if (watchlater == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "no watchlater with this watchlaterID"
                    };
                }
                List<WatchLaterCourse> wl = _dbContext.WatchLaterCourses.Select(s=>s).Where(s=>s.CourseID==CourseID&&s.WatchLaterID==WatchLaterID).ToList();
                if (wl.Count!=0)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "This Course Already In Watch Later List"
                    };
                }
                WatchLaterCourse wlc = new WatchLaterCourse
                {
                    WatchLaterID=WatchLaterID,
                    CourseID=CourseID
                };
                var result = _dbContext.WatchLaterCourses.Add(wlc);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess=true,
                        Message="Course Added Successfully To Watch Later"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Course Not Added Successfully To Watch Later Please Try Again"
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

        public ErrorResponse AddSubCategoryToFavorite(int subCategoryID,int favoriteID)
        {
            try
            {
                SubCategory subCategory = _dbContext.SubCategorys.Find(subCategoryID);
                if (subCategory == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "no subCategory with this SubCategoryID"
                    };
                }
                Favorite favorite = _dbContext.Favorites.Find(favoriteID);
                if (favorite == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "no favorite with this FavoriteID"
                    };
                }
                List<FavoriteSubCategory> fSub = _dbContext.FavoriteSubCategory.Select(s => s).Where(s => s.SubCategoryID==subCategoryID&&s.FavoriteID==favoriteID).ToList();
                if (fSub.Count != 0)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "This SubCategory Already In Favorite List"
                    };
                }
                FavoriteSubCategory fsc = new FavoriteSubCategory
                {
                    SubCategoryID = subCategoryID,
                    FavoriteID = favoriteID
                };
                var result = _dbContext.FavoriteSubCategory.Add(fsc);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "subCategory Added Successfully To Favorite List"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "subCategory Not Added Successfully To Favorite List Please Try Again"
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
        //android
        public ErrorResponse AddPackageToCart(int CartID,Root x)
        {
            try
            {
                Cart cart = _dbContext.Carts.FirstOrDefault(s=>s.CartId==CartID);
                if (cart == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No Cart With This CartID"
                    };
                }
                List<int> neg = new List<int>();
                bool flag=false;
                CurrentCourse cc = _dbContext.CurrentCourses.FirstOrDefault(s => s.studentID == cart.StudentId);
                List<CurrentCoursePackage> ccp = _dbContext.CurrentCoursePackage.Select(s => s).Where(s => s.CurrentCourseID == cc.CurrentCourseID).ToList();
                foreach (var item in x.PackageIDs)
                {
                    flag = false;
                    var pac = _dbContext.Package.Find(item.PackageID);
                    if (pac == null)
                    {
                        return new ErrorResponse
                        {
                            IsSuccess = false,
                            Message = "no Package With This PackageID"
                        };
                    }
                    if (ccp.Count!=0)
                    {
                        foreach (var item2 in ccp)
                        {
                            if (item2.PackageID == item.PackageID)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag==false)
                        {
                            var coursePack2 = _dbContext.CoursePackage.FirstOrDefault(s => s.PackageID == item.PackageID && s.CartId == CartID);
                            if (coursePack2 != null)
                            {
                                //return new ErrorResponse
                                //{
                                //    IsSuccess = false,
                                //    Message = "This Package Alredy in Cart"
                                //};
                            }
                            var cartPackage2 = new CoursePackage
                            {
                                CartId = CartID,
                                PackageID = item.PackageID
                            };
                            var result2 = _dbContext.CoursePackage.Add(cartPackage2);
                            _dbContext.SaveChanges();
                            continue;
                        }
                    }
                    if (flag==false)
                    {
                        var coursePack = _dbContext.CoursePackage.FirstOrDefault(s => s.PackageID == item.PackageID && s.CartId == CartID);
                        if (coursePack == null)
                        {
                            var cartPackage = new CoursePackage
                            {
                                CartId = CartID,
                                PackageID = item.PackageID
                            };
                            var result = _dbContext.CoursePackage.Add(cartPackage);
                            _dbContext.SaveChanges();
                        }
                    }
                }
                if (flag==true)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Yor Already Buy This Package"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = true,
                    Message = "Package Added  Successfully To Cart"
                };

            }
            catch (Exception ex)
            {
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message =ex.Message
                };
            }
        }

        public ErrorResponse RemovePackageFromCart(int CartID, Root x)
        {
            try
            {
                var cart = _dbContext.Carts.Find(CartID);
                if (cart == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No Cart With This CartID"
                    };
                }

                foreach (var item in x.PackageIDs)
                {
                    var pac = _dbContext.Package.Find(item.PackageID);
                    if (pac == null)
                    {
                        return new ErrorResponse
                        {
                            IsSuccess = false,
                            Message = "no Package With This PackageID"
                        };
                    }
                    var coursePack = _dbContext.CoursePackage.FirstOrDefault(s=>s.PackageID==pac.PackageID&&s.CartId==CartID);
                    if (coursePack==null)
                    {
                        return new ErrorResponse
                        {
                            IsSuccess = false,
                            Message = "no This Package in Cart"
                        };
                    }
                    var result = _dbContext.CoursePackage.Remove(coursePack);
                    _dbContext.SaveChanges();
                }
                return new ErrorResponse
                {
                    IsSuccess = true,
                    Message = "Package Deleted  Successfully From Cart"
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

        public ErrorResponse AddPackageToCurrentCourse(int CurrentCoursesID, Root x,int cartID)
        {
            try
            {
                var currentCoursesID = _dbContext.CurrentCourses.Find(CurrentCoursesID);
                if (currentCoursesID == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No currentCourses With This currentCoursesID"
                    };
                }
                foreach (var item in x.PackageIDs)
                {
                    var pac = _dbContext.Package.Find(item.PackageID);
                    if (pac == null)
                    {
                        return new ErrorResponse
                        {
                            IsSuccess = false,
                            Message = "no Package With This PackageID"
                        };
                    }
                }

                foreach (var item in x.PackageIDs)
                {
                    var currentCoursePackage = new CurrentCoursePackage
                    {
                        CurrentCourseID = CurrentCoursesID,
                        PackageID = item.PackageID
                    };
                    var result = _dbContext.CurrentCoursePackage.Add(currentCoursePackage);
                    _dbContext.SaveChanges();
                }
                ErrorResponse er = RemovePackageFromCart(cartID, x);
                if (er.IsSuccess == false)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "error occuer please try again"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = true,
                    Message = "Package Added Successfully To CurrentCourse"
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
        public ErrorResponse DeleteSubCategoryFromFavorite(int favoriteID, int subCategoryID)
        {
            try
            {
                var fav = _dbContext.Favorites.Find(favoriteID);
                if (fav == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "the FavoriteID is invalid"
                    };
                }
                var subCat = _dbContext.SubCategorys.Find(subCategoryID);
                if (subCat == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "the subCategoryID is invalid"
                    };
                }
                var item = _dbContext.FavoriteSubCategory.FirstOrDefault(s => s.SubCategoryID == subCategoryID && s.FavoriteID == favoriteID);
                if (item == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No Item With This FavoriteID And Sub CategoryID"
                    };
                }
                var result = _dbContext.FavoriteSubCategory.Remove(item);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Item Remove Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Item Not Remove Please Try Again"
                };
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

        public ErrorResponse DeleteCourseFromWatchLater(int watchLaterID, int courseID)
        {
            try
            {
                var watchLater = _dbContext.watchLaters.Find(watchLaterID);
                if (watchLater == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "the watchLaterID is invalid"
                    };
                }
                var course = _dbContext.Courses.Find(courseID);
                if (course == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "the courseID is invalid"
                    };
                }
                var item = _dbContext.WatchLaterCourses.FirstOrDefault(s => s.WatchLaterID == watchLaterID&& s.CourseID== courseID);
                if (item == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "No Item With This watchLaterID And courseID"
                    };
                }
                var result = _dbContext.WatchLaterCourses.Remove(item);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Item Remove Successfully"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Item Not Remove Please Try Again"
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
        public ReturnPackageData ReturnPackage(int CourseID)
        {
            try
            {
                var course = _dbContext.Courses.Find(CourseID);
                if (course == null)
                {
                    return new ReturnPackageData
                    {
                        IsSuccess = false,
                        Message = "No course With This CourseID"
                    };
                }
                List<Package> pacData = _dbContext.Package.Select(s => s).Where(s => s.CourseID == CourseID).ToList();
                if (pacData.Count == 0)
                {
                    return new ReturnPackageData
                    {
                        IsSuccess = false,
                        Message = "No Package With This CourseID"
                    };
                }
                ReturnPackageData rpd = new ReturnPackageData();
                List<Components> comName =null;
                List<Lista> data=new List<Lista>();
                foreach (var item in pacData)
                {
                    comName = new List<Components>();
                    List<Component> componentData = _dbContext.Components.Select(s => s).Where(s => s.PackageID == item.PackageID).ToList();
                    foreach (var item2 in componentData)
                    {
                        comName.Add(new Components { ComponentName = item2.ComponentName });
                    }
                    data.Add(new Lista
                    {
                        PackageID = item.PackageID,
                        PackageCost = item.PackageCost,
                        PackageNumber = item.PackageNumber,
                        Components = comName
                    });
                }
                rpd.lista = data;
                rpd.IsSuccess = true;
                return (rpd);
            }
            catch (Exception ex)
            {
                return new ReturnPackageData
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public RootCurrentCourse ReturnCurrentCourses(int CurrentCoursesID)
        {
            try
            {
                List<CurrentCoursePackage>currentCourse = _dbContext.CurrentCoursePackage.Select(s=>s).Where(s=>s.CurrentCourseID==CurrentCoursesID).ToList();
                if (currentCourse.Count == 0)
                {
                    return new RootCurrentCourse
                    {
                        Issucess = false,
                        Message = "no currentCourse With This ID"
                    };
                }
                RootCurrentCourse rcc = new RootCurrentCourse();
                rcc.Lista = new List<ListaData>();
                List<ComponentData> cd = null;
                List<Video> v = null;
                List<CurrentCoursePackage> pacData = _dbContext.CurrentCoursePackage.Select(s => s).Where(s => s.CurrentCourseID == CurrentCoursesID).ToList();
                if (pacData.Count == 0)
                {
                    return new RootCurrentCourse
                    {
                        Issucess = false,
                        Message = "This CurrentCourse is Empty"
                    };
                }
                foreach (var item in pacData)
                {
                    Package pLData = _dbContext.Package.Find(item.PackageID);
                    if (pLData == null)
                    {
                        return new RootCurrentCourse
                        {
                            Issucess = false,
                            Message = "this PackageID is no Found"
                        };
                    }
                    Course courseData = _dbContext.Courses.Find(pLData.CourseID);
                    if (courseData == null)
                    {
                        return new RootCurrentCourse
                        {
                            Issucess = false,
                            Message = "this courseID is no Found"
                        };
                    }
                    List<Component> com = _dbContext.Components.Select(s => s).Where(s => s.PackageID == pLData.PackageID).ToList();
                    if (com.Count == 0)
                    {
                        return new RootCurrentCourse
                        {
                            Issucess = false,
                            Message = "this PackageID is no have any component"
                        };
                    }
                    //v = new List<Video>();
                    cd = new List<ComponentData>();
                    foreach (var item2 in com)
                    {
                        v = new List<Video>();
                        List<media> med = _dbContext.Medias.Select(s => s).Where(s => s.ComponentID == item2.ComponentID).ToList();
                        foreach (var item3 in med)
                        {
                            v.Add(new Video
                            {
                                VideoID = item3.MediaID,
                                VideoName = item3.VideoName,
                                VideoURL = item3.VideoURL
                            });
                        }
                        //cd = new List<ComponentData>();
                        cd.Add(new ComponentData
                        {
                            ComponentID = item2.ComponentID,
                            ComponentName = item2.ComponentName,
                            Video = v
                        });
                    }
                    rcc.Lista.Add(new ListaData
                    {
                        CourseName = courseData.CourseName,
                        Components = cd
                    });
                }
                rcc.Issucess = true;
                rcc.Message = "Data Returned Successfully";
                return (rcc);

            }
            catch (Exception ex)
            {
                return new RootCurrentCourse
                {
                    Issucess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ReturnCourseDetails> ReturnCourseDetails(int CourseID)
        {
            try
            {
                Course courseDetails = _dbContext.Courses.FirstOrDefault(s=> s.CourseID==CourseID&&s.status==true);
                if (courseDetails==null)
                {
                    return new ReturnCourseDetails
                    {
                        IsSuccess = false,
                        Message = "no found this CourseID"
                    };
                }
                IdentityUser identity=null;
                Instructor inst = null;
                if (courseDetails.CenterID==null)
                {
                    inst = _dbContext.Instructors.Find(courseDetails.InstructorID);
                    identity = await _usermanager.FindByIdAsync(inst.AppUserId);
                    if (identity == null)
                    {
                        return new ReturnCourseDetails
                        {
                            IsSuccess = false,
                            Message = "the instructorID Or CenterID not Connected To Any AppUserID"
                        };
                    }
                }
                Center cen= null;
                if (courseDetails.InstructorID== null)
                {
                    cen = _dbContext.Centers.Find(courseDetails.CenterID);
                    identity = await _usermanager.FindByIdAsync(cen.AppUserId);
                    if (identity == null)
                    {
                        return new ReturnCourseDetails
                        {
                            IsSuccess = false,
                            Message = "the instructorID Or CenterID not Connected To Any AppUserID"
                        };
                    }
                }
                if (cen==null&&inst==null)
                {
                    return new ReturnCourseDetails
                    {
                        IsSuccess = false,
                        Message = "This course No Have CenterID And InstructorID Please Add Instructor Or CenterID tO This Course"
                    };
                }
                ReturnCourseDetails rcd = new ReturnCourseDetails();
                List<FreeVideosCourseDetails> fv = new List<FreeVideosCourseDetails>();
                List<ComponentCourseDetails> ccd = new List<ComponentCourseDetails>(); ;
                List<VideoCourseDetails> vcd = null;
                int x = 0;
                List<Package> pack = _dbContext.Package.Select(s => s).Where(s=>s.CourseID==courseDetails.CourseID).ToList();
                if (pack.Count==0)
                {
                    return new ReturnCourseDetails
                    {
                        IsSuccess = false,
                        Message = "This course No Have Packages"
                    };
                }
                foreach (var item in pack)
                {
                    List<Component> com = _dbContext.Components.Select(s => s).Where(s=>s.PackageID==item.PackageID).ToList();
                    if (com.Count == 0)
                    {
                        return new ReturnCourseDetails
                        {
                            IsSuccess = false,
                            Message = "some Package No Have Component"
                        };
                    }

                    List<FreeVideo> fVideo=null;

                    foreach (var item2 in com)
                    {
                        fVideo = _dbContext.FreeVideos.Select(s => s).Where(s => s.ComponentID == item2.ComponentID).ToList();
                        if (fVideo.Count != 0)
                        {
                            x = item2.ComponentID;
                            fv = new List<FreeVideosCourseDetails>();
                            foreach (var item3 in fVideo)
                            {
                                fv.Add(new FreeVideosCourseDetails
                                {
                                    VideoID = item3.FreeVideoID,
                                    VideoName = item3.FreeVideoName,
                                    VideoURL = item3.FreeVideoURL,
                                });
                            }
                        }
                    }
                    List<Component> com2 = _dbContext.Components.Select(s => s).Where(s => s.PackageID == item.PackageID).ToList();
                    foreach (var item4 in com2)
                    {
                        if (item4.ComponentID!=x)
                        {
                            List<media> mVideo = _dbContext.Medias.Select(s => s).Where(s => s.ComponentID == item4.ComponentID).ToList();
                            if (mVideo.Count == 0)
                            {
                                return new ReturnCourseDetails
                                {
                                    IsSuccess = false,
                                    Message = "some Component No Have Video"
                                };
                            }
                            vcd = new List<VideoCourseDetails>();
                            foreach (var item5 in mVideo)
                            {
                                vcd.Add(new VideoCourseDetails
                                {
                                    VideoID = item5.MediaID,
                                    VideoName = item5.VideoName,
                                    VideoURL = item5.VideoURL
                                });
                            }
                            
                            ccd.Add(new ComponentCourseDetails
                            {
                                ComponentID = item4.ComponentID,
                                ComponentName = item4.ComponentName,
                                Video = vcd
                            });
                        }
                    }
                       
                       
                    
                }
                if (cen == null)
                {
                    rcd.Address = null;
                    rcd.CourseID = courseDetails.CourseID;
                    rcd.CourseName = courseDetails.CourseName;
                    rcd.CV = inst.cv;
                    rcd.Description = courseDetails.Description;
                    rcd.Email = identity.Email;
                    rcd.InstructorName = inst.InstructorName;
                    rcd.Logo = courseDetails.Logo;
                    rcd.Online = courseDetails.Online;
                    rcd.Options = courseDetails.Options;
                    rcd.Phone = identity.PhoneNumber;
                    rcd.Price = courseDetails.Price;
                    rcd.QA_FollowingWN = courseDetails.QA_FollowingWN;
                    rcd.RelasedDate = courseDetails.RelasedDate;
                    rcd.Requirements = courseDetails.Requirements;
                    rcd.Stars = courseDetails.Stars;
                    rcd.FreeVideos = fv;
                    rcd.Components = ccd;
                }
                if (inst == null&&courseDetails.choice=="online")
                {
                    rcd.Address =null;
                    rcd.CourseID = courseDetails.CourseID;
                    rcd.CourseName = courseDetails.CourseName;
                    rcd.CV = null;
                    rcd.Description = courseDetails.Description;
                    rcd.Email = identity.Email;
                    rcd.InstructorName = cen.CenterName;
                    rcd.Logo = courseDetails.Logo;
                    rcd.Online = courseDetails.Online;
                    rcd.Options = courseDetails.Options;
                    rcd.Phone = identity.PhoneNumber;
                    rcd.Price = courseDetails.Price;
                    rcd.QA_FollowingWN = courseDetails.QA_FollowingWN;
                    rcd.RelasedDate = courseDetails.RelasedDate;
                    rcd.Requirements = courseDetails.Requirements;
                    rcd.Stars = courseDetails.Stars;
                    rcd.FreeVideos = fv;
                    rcd.Components = ccd;
                }
                if (inst == null && courseDetails.choice == "offline")
                {
                    rcd.Address = cen.address;
                    rcd.CourseID = courseDetails.CourseID;
                    rcd.CourseName = courseDetails.CourseName;
                    rcd.CV = null;
                    rcd.Description = courseDetails.Description;
                    rcd.Email = identity.Email;
                    rcd.InstructorName = cen.CenterName;
                    rcd.Logo = courseDetails.Logo;
                    rcd.Online = courseDetails.Online;
                    rcd.Options = courseDetails.Options;
                    rcd.Phone = identity.PhoneNumber;
                    rcd.Price = courseDetails.Price;
                    rcd.QA_FollowingWN = courseDetails.QA_FollowingWN;
                    rcd.RelasedDate = courseDetails.RelasedDate;
                    rcd.Requirements = courseDetails.Requirements;
                    rcd.Stars = courseDetails.Stars;
                    rcd.FreeVideos = fv;
                    rcd.Components = ccd;
                }
                rcd.IsSuccess = true;
                rcd.Message = "Data Returned Successfully";
                return (rcd);

            }
            catch (Exception ex)
            {
                return new ReturnCourseDetails
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ReturnCourseDetails> ReturnCourseDetailsAdmin(int CourseID)
        {
            try
            {
                Course courseDetails = _dbContext.Courses.FirstOrDefault(s => s.CourseID == CourseID);
                if (courseDetails == null)
                {
                    return new ReturnCourseDetails
                    {
                        IsSuccess = false,
                        Message = "no found this CourseID"
                    };
                }
                IdentityUser identity = null;
                Instructor inst = null;
                if (courseDetails.CenterID == null)
                {
                    inst = _dbContext.Instructors.Find(courseDetails.InstructorID);
                    identity = await _usermanager.FindByIdAsync(inst.AppUserId);
                    if (identity == null)
                    {
                        return new ReturnCourseDetails
                        {
                            IsSuccess = false,
                            Message = "the instructorID Or CenterID not Connected To Any AppUserID"
                        };
                    }
                }
                Center cen = null;
                if (courseDetails.InstructorID == null)
                {
                    cen = _dbContext.Centers.Find(courseDetails.CenterID);
                    identity = await _usermanager.FindByIdAsync(cen.AppUserId);
                    if (identity == null)
                    {
                        return new ReturnCourseDetails
                        {
                            IsSuccess = false,
                            Message = "the instructorID Or CenterID not Connected To Any AppUserID"
                        };
                    }
                }
                if (cen == null && inst == null)
                {
                    return new ReturnCourseDetails
                    {
                        IsSuccess = false,
                        Message = "This course No Have CenterID And InstructorID Please Add Instructor Or CenterID tO This Course"
                    };
                }
                ReturnCourseDetails rcd = new ReturnCourseDetails();
                List<FreeVideosCourseDetails> fv = new List<FreeVideosCourseDetails>();
                List<ComponentCourseDetails> ccd = new List<ComponentCourseDetails>(); ;
                List<VideoCourseDetails> vcd = null;
                int x = 0;
                List<Package> pack = _dbContext.Package.Select(s => s).Where(s => s.CourseID == courseDetails.CourseID).ToList();
                if (pack.Count == 0)
                {
                    return new ReturnCourseDetails
                    {
                        IsSuccess = false,
                        Message = "This course No Have Packages"
                    };
                }
                foreach (var item in pack)
                {
                    List<Component> com = _dbContext.Components.Select(s => s).Where(s => s.PackageID == item.PackageID).ToList();
                    if (com.Count == 0)
                    {
                        return new ReturnCourseDetails
                        {
                            IsSuccess = false,
                            Message = "some Package No Have Component"
                        };
                    }

                    List<FreeVideo> fVideo = null;

                    foreach (var item2 in com)
                    {
                        fVideo = _dbContext.FreeVideos.Select(s => s).Where(s => s.ComponentID == item2.ComponentID).ToList();
                        if (fVideo.Count != 0)
                        {
                            x = item2.ComponentID;
                            fv = new List<FreeVideosCourseDetails>();
                            foreach (var item3 in fVideo)
                            {
                                fv.Add(new FreeVideosCourseDetails
                                {
                                    VideoID = item3.FreeVideoID,
                                    VideoName = item3.FreeVideoName,
                                    VideoURL = item3.FreeVideoURL,
                                });
                            }
                        }
                    }
                    List<Component> com2 = _dbContext.Components.Select(s => s).Where(s => s.PackageID == item.PackageID).ToList();
                    foreach (var item4 in com2)
                    {
                        if (item4.ComponentID != x)
                        {
                            List<media> mVideo = _dbContext.Medias.Select(s => s).Where(s => s.ComponentID == item4.ComponentID).ToList();
                            if (mVideo.Count == 0)
                            {
                                return new ReturnCourseDetails
                                {
                                    IsSuccess = false,
                                    Message = "some Component No Have Video"
                                };
                            }
                            vcd = new List<VideoCourseDetails>();
                            foreach (var item5 in mVideo)
                            {
                                vcd.Add(new VideoCourseDetails
                                {
                                    VideoID = item5.MediaID,
                                    VideoName = item5.VideoName,
                                    VideoURL = item5.VideoURL
                                });
                            }

                            ccd.Add(new ComponentCourseDetails
                            {
                                ComponentID = item4.ComponentID,
                                ComponentName = item4.ComponentName,
                                Video = vcd
                            });
                        }
                    }



                }
                if (cen == null)
                {
                    rcd.Address = null;
                    rcd.CourseID = courseDetails.CourseID;
                    rcd.CourseName = courseDetails.CourseName;
                    rcd.CV = inst.cv;
                    rcd.Description = courseDetails.Description;
                    rcd.Email = identity.Email;
                    rcd.InstructorName = inst.InstructorName;
                    rcd.Logo = courseDetails.Logo;
                    rcd.Online = courseDetails.Online;
                    rcd.Options = courseDetails.Options;
                    rcd.Phone = identity.PhoneNumber;
                    rcd.Price = courseDetails.Price;
                    rcd.QA_FollowingWN = courseDetails.QA_FollowingWN;
                    rcd.RelasedDate = courseDetails.RelasedDate;
                    rcd.Requirements = courseDetails.Requirements;
                    rcd.Stars = courseDetails.Stars;
                    rcd.FreeVideos = fv;
                    rcd.Components = ccd;
                }
                if (inst == null && courseDetails.choice == "online")
                {
                    rcd.Address = null;
                    rcd.CourseID = courseDetails.CourseID;
                    rcd.CourseName = courseDetails.CourseName;
                    rcd.CV = null;
                    rcd.Description = courseDetails.Description;
                    rcd.Email = identity.Email;
                    rcd.InstructorName = cen.CenterName;
                    rcd.Logo = courseDetails.Logo;
                    rcd.Online = courseDetails.Online;
                    rcd.Options = courseDetails.Options;
                    rcd.Phone = identity.PhoneNumber;
                    rcd.Price = courseDetails.Price;
                    rcd.QA_FollowingWN = courseDetails.QA_FollowingWN;
                    rcd.RelasedDate = courseDetails.RelasedDate;
                    rcd.Requirements = courseDetails.Requirements;
                    rcd.Stars = courseDetails.Stars;
                    rcd.FreeVideos = fv;
                    rcd.Components = ccd;
                }
                if (inst == null && courseDetails.choice == "offline")
                {
                    rcd.Address = cen.address;
                    rcd.CourseID = courseDetails.CourseID;
                    rcd.CourseName = courseDetails.CourseName;
                    rcd.CV = null;
                    rcd.Description = courseDetails.Description;
                    rcd.Email = identity.Email;
                    rcd.InstructorName = cen.CenterName;
                    rcd.Logo = courseDetails.Logo;
                    rcd.Online = courseDetails.Online;
                    rcd.Options = courseDetails.Options;
                    rcd.Phone = identity.PhoneNumber;
                    rcd.Price = courseDetails.Price;
                    rcd.QA_FollowingWN = courseDetails.QA_FollowingWN;
                    rcd.RelasedDate = courseDetails.RelasedDate;
                    rcd.Requirements = courseDetails.Requirements;
                    rcd.Stars = courseDetails.Stars;
                    rcd.FreeVideos = fv;
                    rcd.Components = ccd;
                }
                rcd.IsSuccess = true;
                rcd.Message = "Data Returned Successfully";
                return (rcd);

            }
            catch (Exception ex)
            {
                return new ReturnCourseDetails
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public ReturnCartData ReturnCartData(int CartID)
        {
            try
            {
                var cartData = _dbContext.Carts.Find(CartID);
                if (cartData == null)
                {
                    return new ReturnCartData
                    {
                        IsSuccess = false,
                        Message = "no cart With This CartID"
                    };
                }
                List<CoursePackage> coursePackage = _dbContext.CoursePackage.Select(s => s).Where(s => s.CartId == CartID).ToList();
                if (coursePackage.Count == 0)
                {
                    return new ReturnCartData
                    {
                        IsSuccess = true,
                        Message = "This Cart Is Empty"
                    };
                }
                List<PackageCartData> packageCD = new List<PackageCartData>();
                ReturnCartData rcd = new ReturnCartData();
                List<ComponentCartData> componentCD = null;
                foreach (var item in coursePackage)
                {
                    Package packageData = _dbContext.Package.Find(item.PackageID);
                    if (packageData == null)
                    {
                        return new ReturnCartData
                        {
                            IsSuccess = false,
                            Message = "Some PackageID in Cart Not Belong to Pacckage Table"
                        };
                    }
                    componentCD = new List<ComponentCartData>();
                    List<Component> com = _dbContext.Components.Select(s => s).Where(s => s.PackageID == packageData.PackageID).ToList();
                    foreach (var item4 in com)
                    {
                        componentCD.Add(new ComponentCartData
                        {
                            ComponentName = item4.ComponentName
                        });
                    }
                    packageCD.Add(new PackageCartData
                    {
                        PackageID = packageData.PackageID,
                        PackageCost = packageData.PackageCost,
                        PackageNumber = packageData.PackageNumber,
                        Components = componentCD
                    });

                }
                rcd.lista = packageCD;
                rcd.IsSuccess = true;
                rcd.Message = "Data Returned Successfully";
                return (rcd);

            }
            catch (Exception ex)
            {
                return new ReturnCartData
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public ReturnWatchLaterData ReturnWatchLateCourses(int WatchLaterID)
        {
            try
            {
                WatchLater watchLater = _dbContext.watchLaters.Find(WatchLaterID);
                if (watchLater == null)
                {
                    return new ReturnWatchLaterData
                    {
                        IsSuccess = false,
                        Message = "You Enter Invalid WatchLaterID"
                    };
                }
                List<WatchLaterCourse> watchLaterCourse = _dbContext.WatchLaterCourses.Select(s => s).Where(s => s.WatchLaterID == WatchLaterID).ToList();
                if (watchLaterCourse == null)
                {
                    return new ReturnWatchLaterData
                    {
                        IsSuccess = true,
                        Message = "This WatchLater Is Empty"
                    };
                }
                ReturnWatchLaterData returnWLD = new ReturnWatchLaterData();
                List<CourseListData> courseLD = new List<CourseListData>();
                foreach (var item in watchLaterCourse)
                {
                    Course courseData = _dbContext.Courses.Find(item.CourseID);
                    if (courseData==null)
                    {
                        return new ReturnWatchLaterData
                        {
                            IsSuccess = false,
                            Message = "the CourseID Where store in WatchLater Not Belong To Any Coure Which Store In Course Table"
                        };
                    }
                    if (courseData.CenterID==null)
                    {
                        Instructor ins = _dbContext.Instructors.Find(courseData.InstructorID);
                        courseLD.Add(new CourseListData
                        {
                            Address =null,
                            Online=courseData.Online,
                            CourseName=courseData.CourseName,
                            CourseID=courseData.CourseID,
                            Logo=courseData.Logo,
                            Options=courseData.Options,
                            Price=courseData.Price,
                            QA_Following=courseData.QA_Following,
                            RelasedDate=courseData.RelasedDate,
                            Stars=courseData.Stars,
                            CenterName=null,
                            InstructorName=ins.InstructorName
                        });
                    }
                    if (courseData.InstructorID == null)
                    {
                        Center cen = _dbContext.Centers.Find(courseData.CenterID);
                        if (courseData.choice=="online")
                        {
                            courseLD.Add(new CourseListData
                            {
                                Address = null,
                                Online = courseData.Online,
                                CourseName = courseData.CourseName,
                                CourseID = courseData.CourseID,
                                Logo = courseData.Logo,
                                Options = courseData.Options,
                                Price = courseData.Price,
                                QA_Following = courseData.QA_Following,
                                RelasedDate = courseData.RelasedDate,
                                Stars = courseData.Stars,
                                CenterName = cen.CenterName,
                                InstructorName = null
                            });
                        }
                        else if (courseData.choice == "offline")
                        {
                            courseLD.Add(new CourseListData
                            {
                                Address =cen.address,
                                Online = courseData.Online,
                                CourseName = courseData.CourseName,
                                CourseID = courseData.CourseID,
                                Logo = courseData.Logo,
                                Options = courseData.Options,
                                Price = courseData.Price,
                                QA_Following = courseData.QA_Following,
                                RelasedDate = courseData.RelasedDate,
                                Stars = courseData.Stars,
                                CenterName = cen.CenterName,
                                InstructorName =null
                            });
                        }       
                    }
                }
                returnWLD.myCourses = courseLD;
                returnWLD.IsSuccess = true;
                returnWLD.Message = "Data Returned Successfully";
                return (returnWLD);
            }
            catch (Exception ex)
            {
                return new ReturnWatchLaterData
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        //web
        public ErrorResponse AddPackageToCartWeb(int CartID, int packageID)
        {
            try
            {
                if (CartID==0||packageID==0)
                {
                    return new ErrorResponse
                    {
                        IsSuccess=false,
                        Message="Invalid CartID OR PackageID"
                    };
                }
                Cart cart = _dbContext.Carts.Find(CartID);
                if (cart==null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid CartID"
                    };
                }
                Package package= _dbContext.Package.Find(packageID);
                if (package == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid PackageID"
                    };
                }
                CoursePackage CP = _dbContext.CoursePackage.FirstOrDefault(s=>s.PackageID==packageID&&s.CartId==CartID);
                if (CP!=null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "This Package is Already In Cart"
                    };
                }
                var stu = _dbContext.Carts.FirstOrDefault(s=>s.CartId==CartID);
                CurrentCourse cc = _dbContext.CurrentCourses.FirstOrDefault(s=>s.studentID==stu.StudentId);
                CurrentCoursePackage ccp = _dbContext.CurrentCoursePackage.FirstOrDefault(s=>s.PackageID==packageID);
                if (ccp!=null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Yor Already Buy This Package"
                    };
                }
                var cartPackage = new CoursePackage
                {
                    CartId=CartID,
                    PackageID=packageID
                };
                var result = _dbContext.CoursePackage.Add(cartPackage);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = true,
                        Message = "Package Added Successfully To Cart"
                    };
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Package Not Added To Package An Error Occer Please Try Again"
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
        }//web

        public ReturnCartData RemovePackageFromCartWeb(int CartID, int packageID)
        {
            try
            {
                if (CartID == 0 || packageID == 0)
                {
                    return new ReturnCartData
                    {
                        IsSuccess = false,
                        Message = "Invalid CartID OR PackageID"
                    };
                }
                Cart cart = _dbContext.Carts.Find(CartID);
                if (cart == null)
                {
                    return new ReturnCartData
                    {
                        IsSuccess = false,
                        Message = "Invalid CartID"
                    };
                }
                Package package = _dbContext.Package.Find(packageID);
                if (package == null)
                {
                    return new ReturnCartData
                    {
                        IsSuccess = false,
                        Message = "Invalid PackageID"
                    };
                }
                CoursePackage CP = _dbContext.CoursePackage.FirstOrDefault(s => s.PackageID == packageID && s.CartId == CartID);
                if (CP == null)
                {
                    return new ReturnCartData
                    {
                        IsSuccess = false,
                        Message = "This Package is not in cart"
                    };
                }
                var result = _dbContext.CoursePackage.Remove(CP);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    ReturnCartData RCD = ReturnCartData(CartID);
                    return (RCD);
                }
                return new ReturnCartData
                {
                    IsSuccess = false,
                    Message = "Package not Deleted An Error Occer Please Try Again"
                };
            }
            catch (Exception ex)
            {
                return new ReturnCartData
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }//web

        public ErrorResponse AddPackageToCurrentCourseWeb(int CurrentCoursesID, int packageID, int cartID)
        {
            try
            {
                if (CurrentCoursesID == 0 || packageID == 0|| cartID==0)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid CartID OR PackageID OR CurrentCoursesID"
                    };
                }
                Cart cart = _dbContext.Carts.Find(cartID);
                if (cart == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid CartID"
                    };
                }
                Package package = _dbContext.Package.Find(packageID);
                if (package == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid PackageID"
                    };
                }
                CurrentCourse currentCourseID= _dbContext.CurrentCourses.Find(CurrentCoursesID);
                if (currentCourseID == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid CurrentCourseID"
                    };
                }
                CoursePackage CP = _dbContext.CoursePackage.FirstOrDefault(s => s.PackageID == packageID && s.CartId == cartID);
                if (CP == null)
                {
                    return new ErrorResponse
                    {
                        IsSuccess = false,
                        Message = "This Package is not in cart You Mast Add it To Cart"
                    };
                }

                var currentCoursePack = new CurrentCoursePackage
                {
                    CurrentCourseID= CurrentCoursesID,
                    PackageID=packageID
                };
                var result = _dbContext.CurrentCoursePackage.Add(currentCoursePack);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    ReturnCartData RCD= RemovePackageFromCartWeb(cartID, packageID);
                    if (RCD.IsSuccess==true)
                    {
                        return new ErrorResponse
                        {
                            IsSuccess = true,
                            Message = "Add Package To Current Course Successfully"
                        };
                    }
                }
                return new ErrorResponse
                {
                    IsSuccess = false,
                    Message = "Package Not Added to Current Course Successfully"
                };
            }
            catch (Exception ex)
            {
                return new ErrorResponse
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }//web

        public ReturnWatchLaterData DeleteCourseFromWatchLaterWeb(int watchLaterID, int courseID)//web
        {
            try
            {
                var watchLater = _dbContext.watchLaters.Find(watchLaterID);
                if (watchLater == null)
                {
                    return new ReturnWatchLaterData
                    {
                        IsSuccess = false,
                        Message = "the watchLaterID is invalid"
                    };
                }
                var course = _dbContext.Courses.Find(courseID);
                if (course == null)
                {
                    return new ReturnWatchLaterData
                    {
                        IsSuccess = false,
                        Message = "the courseID is invalid"
                    };
                }
                var item = _dbContext.WatchLaterCourses.FirstOrDefault(s => s.WatchLaterID == watchLaterID && s.CourseID == courseID);
                if (item == null)
                {
                    return new ReturnWatchLaterData
                    {
                        IsSuccess = false,
                        Message = "No Item With This watchLaterID And courseID"
                    };
                }
                var result = _dbContext.WatchLaterCourses.Remove(item);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    var watchLaterList = ReturnWatchLateCourses(watchLaterID);
                    if (watchLaterList.IsSuccess)
                    {
                        return (watchLaterList);
                    }
                    return new ReturnWatchLaterData
                    {
                        IsSuccess = false,
                        Message = "Item is Remove but can't to reload the Watch Later List "
                    };
                }
                return new ReturnWatchLaterData
                {
                    IsSuccess = false,
                    Message = "Item Not Remove Please Try Again"
                };
            }
            catch (Exception ex)
            {
                return new ReturnWatchLaterData
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public SubCategoryDataList ReturnSubCategoryList(int favoriteID)
        {
            try
            {
                if (favoriteID==0)
                {
                    return new SubCategoryDataList
                    {
                        Message="FavoriteID Required",
                        IsSuccess=false
                    };
                }
                Favorite favorite = _dbContext.Favorites.Find(favoriteID);
                if (favorite==null)
                {
                    return new SubCategoryDataList
                    {
                        IsSuccess = false,
                        Message = "Invalid FavoriteID"
                    };
                }
                List<FavoriteSubCategory> fSubCat = _dbContext.FavoriteSubCategory.Select(s => s).Where(s => s.FavoriteID == favoriteID).ToList();
                if (fSubCat.Count==0)
                {
                    return new SubCategoryDataList
                    {
                        IsSuccess=true,
                        Message="Your Favorite List Is Empty"
                    };
                }
                SubCategoryDataList SCDL = new SubCategoryDataList();
                SCDL.SubCatObject = new List<SubCategoryData>();
                foreach (var item in fSubCat)
                {
                    var subCatData = _dbContext.SubCategorys.Find(item.SubCategoryID);
                    SCDL.SubCatObject.Add(new SubCategoryData 
                    {
                        SubCategoryID=subCatData.SubCategoryID,
                        SubCategoryLogo=subCatData.SubCategoryLogo,
                        SubCategoryName=subCatData.SubCategoryName
                    });
                }
                SCDL.IsSuccess = true;
                SCDL.Message = "Favorite List Returned Successfully";
                return (SCDL);
            }
            catch (Exception ex)
            {
                return new SubCategoryDataList
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }//web
        public SubCategoryDataList DeleteSubCategoryFromFavoriteWeb(int favoriteID, int subCategoryID)//web
        {
            try
            {
                var favorite = _dbContext.Favorites.Find(favoriteID);
                if (favorite == null)
                {
                    return new SubCategoryDataList
                    {
                        IsSuccess = false,
                        Message = "invalid FavoriteID"
                    };
                }
                var subCat = _dbContext.SubCategorys.Find(subCategoryID);
                if (subCat == null)
                {
                    return new SubCategoryDataList
                    {
                        IsSuccess = false,
                        Message = "invalid subCategoryID"
                    };
                }
                var item = _dbContext.FavoriteSubCategory.FirstOrDefault(s => s.FavoriteID == favoriteID&& s.SubCategoryID== subCategoryID);
                if (item == null)
                {
                    return new SubCategoryDataList
                    {
                        IsSuccess = false,
                        Message = "No Item With This FavoriteID And SubCategoryID"
                    };
                }
                var result = _dbContext.FavoriteSubCategory.Remove(item);
                _dbContext.SaveChanges();
                if (result.IsKeySet)
                {
                    var favoriteList = ReturnSubCategoryList(favoriteID);
                    if (favoriteList.IsSuccess)
                    {
                        return (favoriteList);
                    }
                    return new SubCategoryDataList
                    {
                        IsSuccess = false,
                        Message = "Item is Remove but can't to reload the Favorite List "
                    };
                }
                return new SubCategoryDataList
                {
                    IsSuccess = false,
                    Message = "Item Not Remove Please Try Again"
                };
            }
            catch (Exception ex)
            {
                return new SubCategoryDataList
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

    }
}