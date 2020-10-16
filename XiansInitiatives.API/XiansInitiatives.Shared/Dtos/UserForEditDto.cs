using Microsoft.AspNetCore.Http;
using System;

namespace XiansInitiatives.Shared.Dtos
{
    public class UserForEditDto
    {
        
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile File { get; set; }
    }
}