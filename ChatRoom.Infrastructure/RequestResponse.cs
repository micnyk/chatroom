namespace ChatRoom.Infrastructure
{
    public class RequestResponse
    {
        public ResponseResult ResponseResult { get; set; }

        public object Data { get; set; }

        public string[] Messages { get; set; }
    }
}