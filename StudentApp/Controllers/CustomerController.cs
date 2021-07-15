using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using StudentApp.Model;

namespace StudentApp.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private const string tableName = "Student";
        private readonly Table table;
        public CustomerController(IAmazonDynamoDB dynamoDbClient)
        {
            this.table = Table.LoadTable(dynamoDbClient, tableName);
        }

        // GET api/customer
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            ScanOperationConfig config = new ScanOperationConfig();

            IEnumerable<Document> documents = await this.table.Scan(config).GetRemainingAsync();

            return ToStudentContract(documents);

        }
        // GET api/customer/1
        [HttpGet]
        [Route("{id}")]
        public async Task<Student> Get(string id)
        {
            Document document = await this.table.GetItemAsync(id);

            return ToStudentContract(document);

        }

        // POST api/customer
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Student student)
        {
            var id = Guid.NewGuid().ToString();
            student.Id = id;

            Document document = Document.FromJson(student.ToString());            
            await this.table.PutItemAsync(document);
            return Ok();
        }

        private IEnumerable<Student> ToStudentContract(IEnumerable<Document> documents)
        {
            return documents.Select(ToStudentContract);
        }

        private Student ToStudentContract(Document document)
        {
            return new Student
            {
                Id = document["id"],
                FirstName = document["firstName"],
                LastName = document["lastName"],
                Address = document["address"]
            };
        }
    }
}
