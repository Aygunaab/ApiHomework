using AutoMapper;
using Data.DAL;
using Data.Services;
using DomainModels.Dtos;
using DomainModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRepositoryService<Student> _repo;

        public StudentController(IMapper mapper, IRepositoryService<Student> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            List<Student> students = await _repo.GetAll( s=>s.Name.Contains("A"));
            List<Student> studentsa = await _repo.GetAll();

            List<StudentDto> studentDtos = _mapper.Map<List<StudentDto>>(students);

            return Ok(studentDtos);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]int Id)
        {
            var student = await _repo.GetOne(Id);
            if (student == null) return NotFound();
            return Ok(_mapper.Map<StudentDto>(student));
        }
        [HttpPost]
        public async Task<IActionResult>Create( [FromBody]StudentPostDto model)
        {
            var student = _mapper.Map<Student>(model);
            await _repo.Create(student);
            return StatusCode(StatusCodes.Status201Created);
          
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute]int Id,[FromBody] StudentPostDto model)
        {
            var student = await _repo.GetOne(Id);
            if (student == null) return NotFound();
            student.Name = model.Name;
            student.Surname = model.Surname;
            student.Age = model.Age;
            await _repo.Update(student);
            return Ok();

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]int Id)
        {
            var student = _repo.GetOne(Id);
            if (student == null) return NotFound();
            await _repo.Delete(Id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        //[HttpGet("list")]
        //public async Task<IActionResult> GetStudents()
        //{
        //    List<Student> student = await _context.Students.ToListAsync();

        //    List<StudentDto> studentDtos = _mapper.Map<List<StudentDto>>(student);
        //    //List<Student> students = new List<Student>();
        //    //students.Add(new Student { Id = 1, Name = "Ricat", Surname = "Alizade", Age = 17 });
        //    //students.Add(new Student { Id = 1, Name = "Elbrus", Surname = "Suleymanli", Age = 37 });
        //    //students.Add(new Student { Id = 1, Name = "Aqil", Surname = "Bashirov", Age = 28 });
        //    //students.Add(new Student { Id = 1, Name = "Sema", Surname = "Mayilova", Age = 17 });
        //    //students.Add(new Student { Id = 1, Name = "Madina", Surname = "Agazade", Age = 19 });
        //    return Ok(studentDtos);
        //}
        //[HttpGet("{Id}")]
        //public async Task<IActionResult>Get(int Id)
        //{
        //    var student = await _context.Students.FindAsync(Id);
        //    if (student == null) return NotFound();
        //    return Ok(_mapper.Map<StudentDto>(student));
        //}
    }
}
