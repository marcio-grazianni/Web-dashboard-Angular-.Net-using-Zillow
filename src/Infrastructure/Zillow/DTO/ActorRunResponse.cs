using System;
using System.Collections.Generic;

namespace CDACommercial.PoC.Infrastructure.Zillow.DTO
{
    public class ActorRunResponse
    {
        public ActorRunData Data { get; set; }
    }
    public class ActorRunData
    {
        public long Total { get; set; }
        public long Count { get; set; }
        public long Offset { get; set; }
        public long Limit { get; set; }
        public List<ActorRun> Items { get; set; }

    }

    public class ActorRun
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public string BuildId { get; set; }
        public string BuildNumber { get; set; }
        public string DefaultKeyValueStoreId { get; set; }
        public string DefaultDatasetId { get; set; }
        public string DefaultRequestQueueId { get; set; }
    }
}