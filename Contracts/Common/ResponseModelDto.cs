namespace Contracts.Common
{
    public class ResponseModelDto
    {
        public string ResponseCode { get; set; }
        public long TotalCount {  get; set; }

        public dynamic Data { get; set; }

        public string Response { get; set; }

        public IList<ErrorModelDto>? Errors { get; set; }
    }
}
