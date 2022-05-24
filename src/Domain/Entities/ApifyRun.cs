namespace CDACommercial.PoC.Domain.Entities
{
    public class ApifyRun : Entity
    {
        public string RunId { get; private set; }
        public string Data { get; private set; }
        public string Message { get; private set; }


        public ApifyRun(string runId, string data)
        {
            RunId = runId;
            Data = data;
        }

        public void Fail(string stackTrace)
        {
            Message = stackTrace;
        }

    }
}