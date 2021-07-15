using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StudentApp.Model
{
    [DynamoDBTable("Student")]
    public class Student
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("id")]
        public string Id { get; set; }

        [DynamoDBProperty("firstName")]
        public string FirstName { get; set; }

        [DynamoDBProperty("lastName")]
        public string LastName { get; set; }

        [DynamoDBProperty("address")]
        public string Address { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            
        }
    }
}
