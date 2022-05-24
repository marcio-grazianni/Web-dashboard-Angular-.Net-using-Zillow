using System;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Application.Api.DTO
{
    public class JobRequest
    {
        public JobType Type { get; set; }
        public string Title { get; set; }
        public DateTime? RunAt { get; set; }
    }
}