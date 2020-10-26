using LibraryAPI.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    public class DemoController : ControllerBase
    {
        //GET /status
        [HttpGet("/status")]
        public ActionResult GetTheStatus()
        {
            return Ok(new { Message = "All is Good", CreatedAt = DateTime.Now});
        }
        //GET /employees/99
        [HttpGet("/employees/{employeeId:int}",Name ="demo-getemployee")]
        public ActionResult GetEmployee(int employeeId)
        {
            return Ok(new { EmployeeId = employeeId, Name = "Bob Smith" });
        }

        [HttpGet("/blogs/{year:int}/{month:int}/{day:int}")]
        public ActionResult GetBlogPosts(int year, int month, int day)
        { return Ok($"Geeting the blog posts for {month}/{day}/{year}"); }

        //Get /agents
        //Get /agents?state=CO
        [HttpGet("/agents")]
        public ActionResult GetAgents([FromQuery] string state="All",[FromQuery] string city="All")
        {
            return Ok($"Getting Agents from state { state} and from {city}");
        }

        [HttpGet("/whoami")] // User-Agent
        public ActionResult GetUserAgent([FromHeader(Name ="User-Agent")] string userAgent)
        {
            return Ok($"I see you are running {userAgent}");
        }

        [HttpPost("/employees")]
        public ActionResult Hire([FromBody] PostEmployeeRequest employeeToHire)
        {
            //Posting to a collection
            //1. 

            var response = new GetEmployeeDetailsResponse
            {
                Id = new Random().Next(40, 2000),
                Name = employeeToHire.Name,
                Department = employeeToHire.Department,
                Manager = "Sue Jones",
                Salary = employeeToHire.StartingSalary * 1.3M
            };

            return CreatedAtRoute("demo-getemployee", new { employeeId = response.Id }, response);

        }
    }
}
