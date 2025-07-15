using Microsoft.AspNetCore.Mvc;
using src.Interface;
using src.Models;
using src.ViewModels;

namespace src.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        //Get all Courses...
        public async Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = await _courseRepository.GetAll();
            return View(courses);
        }

        //Get request for create course...
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Post request for creating courses...
        public async Task<IActionResult> Create(CreateCourseViewModel createVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var course = new Course
                    {
                        Name = createVM.Name,
                        CourseCode = createVM.CourseCode,
                        Unit = createVM.Unit,
                        CourseCategory = createVM.CourseCategory,
                        //Lecturer = new User
                        //{
                        //    FullName = createVM.Lecturer.FullName,
                        //    Gender = createVM.Lecturer.Gender,
                        //    Email = createVM.Lecturer.Email,
                        //    Department = createVM.Lecturer.Department,
                        //    DOB = createVM.Lecturer.DOB,
                        //    Nationality = createVM.Lecturer.Nationality,
                        //    StateOfOrigin = createVM.Lecturer.StateOfOrigin,
                        //    LGA = createVM.Lecturer.LGA,
                        //}
                    };
                    _courseRepository.Add(course);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not create course due to " + ex.Message);
                    return View(createVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Course creation failed");
            }
            return View(createVM);
        }

        //Get request for editing courses...
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseVM = new EditCourseViewModel
            {
                Name = course.Name,
                CourseCode = course.CourseCode,
                Unit = course.Unit,
                CourseCategory = course.CourseCategory,
                //Lecturer = course.Lecturer,
                //LecturerId = course.LecturerId,
            };
            return View(courseVM);
        }

        //Post request for editing courses...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCourseViewModel editVM)
        {
            if (ModelState.IsValid)
            {
                var course = await _courseRepository.GetByIdAsyncNoTracking(editVM.Id);

                if (course == null)
                {
                    return NotFound();
                }
                else
                {
                    course.Name = editVM.Name;
                    course.CourseCode = editVM.CourseCode;
                    course.Unit = editVM.Unit;
                    course.CourseCategory = editVM.CourseCategory;
                    course.LecturerId = editVM.LecturerId;
                    course.Lecturer = editVM.Lecturer;

                    _courseRepository.Update(course);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(editVM);
            }
        }

        //deleting courses...
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var courseDetails = await _courseRepository.GetByIdAsync(id);
            if (courseDetails == null)
            {
                return NotFound();
            }

            _courseRepository.Delete(courseDetails);
            return RedirectToAction("Index");
        }
    }
}
