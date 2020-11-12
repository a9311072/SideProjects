using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using UserRegistration.Models;
using UserRegistration.Repositories;
using UserRegistration.Services;

namespace UserRegistration.WebApi.Controllers
{
    public class CourseController : BaseCrudService<Course, CourseRepository> { }
}