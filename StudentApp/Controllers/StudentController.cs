using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using StudentApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace StudentApp.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IDynamoDBContext context;
        public StudentController(IAmazonDynamoDB dynamoDbClient)
        {
            this.context = new DynamoDBContext(dynamoDbClient);
        }

        // GET api/student
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await this.context.ScanAsync<Student>(new List<ScanCondition>()).GetRemainingAsync();
        }

        // POST api/student
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Student student)
        {
            var id = Guid.NewGuid().ToString();
            student.Id = id;
            await context.SaveAsync(student);
            return Ok();
        }

    }
}
