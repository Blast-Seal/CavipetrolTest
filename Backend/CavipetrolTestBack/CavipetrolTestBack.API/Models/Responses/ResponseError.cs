namespace CavipetrolTestBack.API.Models.Responses
{
    public class ResponseError : BaseResponse
    {
        public ResponseError()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }
    }
}
