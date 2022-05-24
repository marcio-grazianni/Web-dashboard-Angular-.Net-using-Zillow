using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CDACommercial.PoC.Domain.Entities
{
    public class Job : Entity
    {

        public string Title { get; private set; }
        public string Parameters {get; private set;}
        public JobStatus Status { get; private set; }
        public JobType Type { get; private set; }
        public DateTime RunAt { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public string Message { get; private set; }


        public Job(string title, JobType type, DateTime runAt)
        {
            Title = title;
            Status = JobStatus.Scheduled;
            Type = type;
            RunAt = runAt;
            Message = "";
            Parameters = null;
        }

        public void Start()
        {
            Status = JobStatus.Started;
            StartTime = DateTime.UtcNow;
        }

        public void End(string message = "")
        {
            Status = JobStatus.Completed;
            EndTime = DateTime.UtcNow;
            Message = message;
        }

        public void Fail(string exception)
        {
            Status = JobStatus.Failed;
            EndTime = DateTime.UtcNow;
            Message = exception;
        }

        public void AddParameters(Dictionary<string, dynamic> parameters)
        {
            Parameters = JsonConvert.SerializeObject(parameters);
        }

        public Dictionary<string, dynamic> GetParameters()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(Parameters);
        }

    }


    public enum JobType
    {
        ListingDiscovery,
        MarketHistoryDiscovery,
        Calculation,
        MarketSummaryDiscovery,
        ListingPrediction
    }

    public enum JobStatus
    {
        Scheduled,
        Started,
        Completed,
        Failed
    }
}