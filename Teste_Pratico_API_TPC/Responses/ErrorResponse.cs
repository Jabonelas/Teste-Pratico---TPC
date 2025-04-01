﻿namespace Teste_Pratico_API_TPC.Responses
{
    public class ErrorResponse
    {
        public string message { get; set; }
    }

    public class ErrorResponse400
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string[] errors { get; set; }
        public string traceId { get; set; }
    }
}