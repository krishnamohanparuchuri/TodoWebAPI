﻿namespace TodoWebAPI.Dtos
{
    public class AuthResultDto
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
